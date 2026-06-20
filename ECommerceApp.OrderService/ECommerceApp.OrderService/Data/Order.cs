namespace ECommerceApp.OrderService.Data
{
    public class Order
    {
        public Guid Id { get; set; }

        public string CustomerId { get; set; } = default!;

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Created";

        public DateTime CreatedAt { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }
}
