    namespace ECommerceApp.InventoryService.Data
    {
        public class InventoryReservation
        {
            public Guid Id { get; set; }

            public Guid OrderId { get; set; }
            public string OrderNumber { get; set; } = default!;

            public string Status { get; set; } = default!;
            // Reserved, Failed, PartiallyReserved, Released

            public DateTime CreatedAt { get; set; }

            public List<InventoryReservationItem> Items { get; set; } = new();
        }
    }
