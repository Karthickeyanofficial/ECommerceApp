namespace ECommerceApp.InventoryService.Data
{
    public class InventoryItem
    {
        public Guid Id { get; set; }

        public string ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;

        public int AvailableQuantity { get; set; }
        public int ReservedQuantity { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
