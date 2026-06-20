using ECommerceApp.OrderService.Data;
using ECommerceApp.OrderService.Models;
using ECommerceApp.Shared;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.OrderService;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderDbContext _db;
    private readonly IPublishEndpoint _publish;

    public OrdersController(OrderDbContext db, IPublishEndpoint publish)
    {
        _db = db;
        _publish = publish;
    }

    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var orderId = Guid.NewGuid();
        var orderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}";

        // Validate against seeded inventory products (important)
        var validProducts = new[] { "P1", "P2", "P3", "P4" };

        foreach (var item in request.Items)
        {
            if (!validProducts.Contains(item.ProductId))
            {
                return BadRequest($"Invalid ProductId: {item.ProductId}. Allowed: P1-P4");
            }
        }

        var order = new Order
        {
            Id = orderId,
            CustomerId = request.CustomerId,
            CreatedAt = DateTime.UtcNow,
            Status = "Created"
        };

        order.Items = request.Items.Select(i => new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            ProductId = i.ProductId,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice
        }).ToList();

        order.TotalAmount = order.Items.Sum(x => x.Quantity * x.UnitPrice);

        _db.Orders.Add(order);

        _db.OrderStatusHistory.Add(new OrderStatusHistory
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            Status = "Created",
            ChangedAt = DateTime.UtcNow
        });

        // Publish event (Inventory will consume)
        await _publish.Publish(new OrderCreated(
            OrderId: order.Id,
            OrderNumber: orderNumber,
            CustomerName: request.CustomerId,
            TotalAmount: order.TotalAmount,
            CreatedAt: order.CreatedAt,
            Items: order.Items.Select(i => new OrderItemContract(
                ProductId: i.ProductId,
                Quantity: i.Quantity,
                UnitPrice: i.UnitPrice
            )).ToList()
        ));

        await _db.SaveChangesAsync();

        return Ok(new
        {
            order.Id,
            OrderNumber = orderNumber,
            TotalAmount = order.TotalAmount
        });
    }
}