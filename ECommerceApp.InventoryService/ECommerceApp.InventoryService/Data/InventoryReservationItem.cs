namespace ECommerceApp.InventoryService.Data
{
    public class InventoryReservationItem
    {
        public Guid Id { get; set; }

        public Guid ReservationId { get; set; }

        public string ProductId { get; set; } = default!;

        public int RequestedQuantity { get; set; }

        public int ReservedQuantity { get; set; }

        public string Status { get; set; } = default!;
        // Reserved, Failed
    }
}
