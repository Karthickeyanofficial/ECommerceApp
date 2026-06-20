using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApp.InventoryService.Migrations
{
    /// <inheritdoc />
    public partial class addinventoryitemsdbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryReservationItem_InventoryReservations_InventoryRes~",
                table: "InventoryReservationItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryReservationItem",
                table: "InventoryReservationItem");

            migrationBuilder.DropIndex(
                name: "IX_InventoryReservationItem_InventoryReservationId",
                table: "InventoryReservationItem");

            migrationBuilder.DropColumn(
                name: "InventoryReservationId",
                table: "InventoryReservationItem");

            migrationBuilder.RenameTable(
                name: "InventoryReservationItem",
                newName: "InventoryReservationItems");

            migrationBuilder.AlterColumn<int>(
                name: "ReservedQuantity",
                table: "InventoryReservationItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RequestedQuantity",
                table: "InventoryReservationItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryReservationItems",
                table: "InventoryReservationItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_ProductId",
                table: "InventoryTransactions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReservationItems_ProductId",
                table: "InventoryReservationItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReservationItems_ReservationId",
                table: "InventoryReservationItems",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryReservationItems_InventoryReservations_Reservation~",
                table: "InventoryReservationItems",
                column: "ReservationId",
                principalTable: "InventoryReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryReservationItems_InventoryReservations_Reservation~",
                table: "InventoryReservationItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransactions_ProductId",
                table: "InventoryTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryReservationItems",
                table: "InventoryReservationItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryReservationItems_ProductId",
                table: "InventoryReservationItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryReservationItems_ReservationId",
                table: "InventoryReservationItems");

            migrationBuilder.RenameTable(
                name: "InventoryReservationItems",
                newName: "InventoryReservationItem");

            migrationBuilder.AlterColumn<int>(
                name: "ReservedQuantity",
                table: "InventoryReservationItem",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RequestedQuantity",
                table: "InventoryReservationItem",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "InventoryReservationId",
                table: "InventoryReservationItem",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryReservationItem",
                table: "InventoryReservationItem",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryReservationItem_InventoryReservationId",
                table: "InventoryReservationItem",
                column: "InventoryReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryReservationItem_InventoryReservations_InventoryRes~",
                table: "InventoryReservationItem",
                column: "InventoryReservationId",
                principalTable: "InventoryReservations",
                principalColumn: "Id");
        }
    }
}
