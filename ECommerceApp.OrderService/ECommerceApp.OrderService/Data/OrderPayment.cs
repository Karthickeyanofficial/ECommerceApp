namespace ECommerceApp.OrderService.Data
{
    public class OrderPayment
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public decimal Amount { get; set; }

        public string PaymentStatus { get; set; } = "Pending";

        public string? TransactionId { get; set; }

        public DateTime? PaidAt { get; set; }
    }
}
