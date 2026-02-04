using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolDbContext.Migrations
{
    /// <inheritdoc />
    public partial class notmapped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderRow_Order_OrderId",
                table: "OrderRow");

            migrationBuilder.DropIndex(
                name: "IX_OrderRow_OrderId",
                table: "OrderRow");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
