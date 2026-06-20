using ECommerceApp.InventoryService.Data;
using ECommerceApp.Shared;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.InventoryService.Consumer;

public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    private readonly InventoryDbContext _db;
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(
        InventoryDbContext db,
        ILogger<OrderCreatedConsumer> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var message = context.Message;

        _logger.LogInformation("Processing OrderCreated: {OrderId}", message.OrderId);

        var reservation = new InventoryReservation
        {
            Id = Guid.NewGuid(),
            OrderId = message.OrderId,
            OrderNumber = message.OrderNumber,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
            Items = new List<InventoryReservationItem>()
        };

        bool allSuccess = true;

        foreach (var item in message.Items)
        {
            var inventory = await _db.InventoryItems
                .FirstOrDefaultAsync(x => x.ProductId == item.ProductId);

            bool success =
                inventory != null &&
                inventory.AvailableQuantity >= item.Quantity;

            if (success)
            {
                inventory!.AvailableQuantity -= item.Quantity;
                inventory.ReservedQuantity += item.Quantity;
                inventory.LastUpdated = DateTime.UtcNow;
            }
            else
            {
                allSuccess = false;
            }

            reservation.Items.Add(new InventoryReservationItem
            {
                Id = Guid.NewGuid(),
                ProductId = item.ProductId,
                RequestedQuantity = item.Quantity,
                ReservedQuantity = success ? item.Quantity : 0,
                Status = success ? "Reserved" : "Failed"
            });

            _db.InventoryTransactions.Add(new InventoryTransaction
            {
                Id = Guid.NewGuid(),
                ProductId = item.ProductId,
                OrderId = message.OrderId,
                Type = success ? "Reserved" : "Failed",
                QuantityChanged = success ? item.Quantity : 0,
                CreatedAt = DateTime.UtcNow
            });
        }

        reservation.Status = allSuccess ? "Reserved" : "Failed";

        _db.InventoryReservations.Add(reservation);

        _logger.LogInformation(
            "Inventory processed. OrderId: {OrderId}, Success: {Success}",
            message.OrderId,
            allSuccess);

        // Publish next event
        await context.Publish(new InventoryReserved(
            message.OrderId,
            message.OrderNumber,
            allSuccess,
            DateTime.UtcNow,
            allSuccess ? null : "One or more items failed",
            reservation.Items.Select(i => new InventoryReservationItemContract(
                i.ProductId,
                i.RequestedQuantity,
                i.ReservedQuantity
            )).ToList()
        ));

        await _db.SaveChangesAsync();
    }
}