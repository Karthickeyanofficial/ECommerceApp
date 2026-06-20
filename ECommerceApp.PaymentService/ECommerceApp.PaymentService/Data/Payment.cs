namespace ECommerceApp.PaymentService.Data
{
    public class Payment
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string OrderNumber { get; set; } = default!;

        public decimal Amount { get; set; }

        public string Status { get; set; } = default!;
        // Pending, Paid, Failed

        public DateTime CreatedAt { get; set; }
    }
}
