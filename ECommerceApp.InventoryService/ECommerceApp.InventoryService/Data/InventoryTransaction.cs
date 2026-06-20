namespace ECommerceApp.InventoryService.Data
{
    public class InventoryTransaction
    {
        public Guid Id { get; set; }

        public string ProductId { get; set; } = default!;

        public string Type { get; set; } = default!;
        // Reserved, Released, Adjusted

        public int QuantityChanged { get; set; }

        public Guid? OrderId { get; set; }

        public Guid? ReservationId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
