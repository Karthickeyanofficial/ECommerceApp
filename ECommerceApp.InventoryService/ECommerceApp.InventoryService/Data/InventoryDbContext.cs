using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.InventoryService.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options) { }

        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
        public DbSet<InventoryReservation> InventoryReservations => Set<InventoryReservation>();
        public DbSet<InventoryReservationItem> InventoryReservationItems => Set<InventoryReservationItem>();
        public DbSet<InventoryTransaction> InventoryTransactions => Set<InventoryTransaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // InventoryItem config
            // =========================
            modelBuilder.Entity<InventoryItem>()
                .HasIndex(x => x.ProductId)
                .IsUnique();

            modelBuilder.Entity<InventoryItem>()
                .Property(x => x.AvailableQuantity)
                .HasDefaultValue(0);

            modelBuilder.Entity<InventoryItem>()
                .Property(x => x.ReservedQuantity)
                .HasDefaultValue(0);

            // =========================
            // InventoryReservation config
            // =========================
            modelBuilder.Entity<InventoryReservation>()
                .HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey(x => x.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // InventoryReservationItem config
            // =========================
            modelBuilder.Entity<InventoryReservationItem>()
                .Property(x => x.RequestedQuantity)
                .HasDefaultValue(0);

            modelBuilder.Entity<InventoryReservationItem>()
                .Property(x => x.ReservedQuantity)
                .HasDefaultValue(0);

            modelBuilder.Entity<InventoryReservationItem>()
                .HasIndex(x => x.ProductId);

            // =========================
            // InventoryTransaction config
            // =========================
            modelBuilder.Entity<InventoryTransaction>()
                .HasIndex(x => x.ProductId);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}