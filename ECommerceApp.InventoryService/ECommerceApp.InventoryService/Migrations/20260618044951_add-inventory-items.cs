using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApp.InventoryService.Migrations
{
    /// <inheritdoc />
    public partial class addinventoryitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "InventoryReservations");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "InventoryReservations",
                newName: "OrderNumber");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "InventoryTransactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InventoryReservationItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: false),
                    RequestedQuantity = table.Column<int>(type: "integer", nullable: false),
                    ReservedQuantity = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    InventoryReservationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryReservationItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryReservationItem_InventoryReservations_InventoryRes~",
                        column: x => x.InventoryReservationId,
                        principalTable: "InventoryReservations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReservationItem_InventoryReservationId",
                table: "InventoryReservationItem",
                column: "InventoryReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryReservationItem");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "InventoryTransactions");

            migrationBuilder.RenameColumn(
                name: "OrderNumber",
                table: "InventoryReservations",
                newName: "ProductId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "InventoryReservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
