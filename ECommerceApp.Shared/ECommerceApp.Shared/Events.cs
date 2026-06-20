namespace ECommerceApp.Shared
{
    public record OrderCreated
  (
      Guid OrderId,
      string OrderNumber,      
      string CustomerName,
      decimal TotalAmount,    
      DateTime CreatedAt,

      List<OrderItemContract> Items
  );

    public record OrderItemContract
   (
       string ProductId,
       int Quantity,
       decimal UnitPrice
   );

    public record InventoryReserved(
        Guid OrderId,
        string OrderNumber,
        bool Success,
        DateTime ReservedAt,
        string? FailureReason,
        List<InventoryReservationItemContract> Items
    );

    public record InventoryReservationItemContract
    (
        string ProductId,
        int RequestedQuantity,
        int ReservedQuantity
    );
}
