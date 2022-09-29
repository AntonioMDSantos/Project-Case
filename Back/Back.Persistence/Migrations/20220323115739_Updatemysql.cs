using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back.Persistence.Migrations
{
    public partial class Updatemysql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sale",
                table: "Products",
                newName: "Sale");

            migrationBuilder.RenameColumn(
                name: "revenue",
                table: "Products",
                newName: "Revenue");

            migrationBuilder.RenameColumn(
                name: "purchase",
                table: "Products",
                newName: "Purchase");

            migrationBuilder.RenameColumn(
                name: "profit",
                table: "Products",
                newName: "Profit");

            migrationBuilder.RenameColumn(
                name: "cost",
                table: "Products",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Products",
                newName: "ProductQtd");

            migrationBuilder.RenameColumn(
                name: "Produto",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "ProductOrders",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "pOrs",
                table: "ProductOrders",
                newName: "OrderType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sale",
                table: "Products",
                newName: "sale");

            migrationBuilder.RenameColumn(
                name: "Revenue",
                table: "Products",
                newName: "revenue");

            migrationBuilder.RenameColumn(
                name: "Purchase",
                table: "Products",
                newName: "purchase");

            migrationBuilder.RenameColumn(
                name: "Profit",
                table: "Products",
                newName: "profit");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Products",
                newName: "cost");

            migrationBuilder.RenameColumn(
                name: "ProductQtd",
                table: "Products",
                newName: "Quantidade");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Produto");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ProductOrders",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "OrderType",
                table: "ProductOrders",
                newName: "pOrs");
        }
    }
}
