namespace ECommerceApp.OrderService.Data
{
    public class OrderStatusHistory
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string Status { get; set; } = default!;

        public DateTime ChangedAt { get; set; }
    }
}
