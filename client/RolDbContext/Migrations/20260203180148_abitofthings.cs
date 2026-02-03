using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolDbContext.Migrations
{
    /// <inheritdoc />
    public partial class abitofthings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Customer",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRow_OrderId",
                table: "OrderRow",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRow_Order_OrderId",
                table: "OrderRow",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderRow_Order_OrderId",
                table: "OrderRow");

            migrationBuilder.DropIndex(
                name: "IX_OrderRow_OrderId",
                table: "OrderRow");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Customer");
        }
    }
}
