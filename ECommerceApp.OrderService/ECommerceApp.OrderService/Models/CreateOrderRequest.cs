namespace ECommerceApp.OrderService.Models
{
    public class CreateOrderRequest
    {
        public string CustomerId { get; set; } = default!;
        public List<CreateOrderItemRequest> Items { get; set; } = new();
    }

    public class CreateOrderItemRequest
    {
        public string ProductId { get; set; } = default!; // must be P1-P4
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
