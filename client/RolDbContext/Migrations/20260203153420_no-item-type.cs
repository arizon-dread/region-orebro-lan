using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolDbContext.Migrations
{
    /// <inheritdoc />
    public partial class noitemtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "OrderRow");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "ItemInventory");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "Info");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "Customer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "OrderRow",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "Order",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "ItemInventory",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "Item",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "Info",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "Customer",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
