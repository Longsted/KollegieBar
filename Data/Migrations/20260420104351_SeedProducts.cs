using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AlcoholPercentage", "CostPrice", "Discriminator", "MaxStockQuantity", "MinStockQuantity", "Name", "Pant", "SalesPrice", "StockQuantity", "VolumeCl" },
                values: new object[,]
                {
                    { 3, 4.5999999999999996, 5.50m, "LiquidWithAlcohol", 0, 0, "Ceres Top", 0m, 20.00m, 100, 33 },
                    { 4, 4.5999999999999996, 6.00m, "LiquidWithAlcohol", 0, 0, "Albani øl", 0m, 22.00m, 150, 33 },
                    { 5, 4.5, 12.00m, "LiquidWithAlcohol", 0, 0, "Shaker Sport", 0m, 35.00m, 80, 33 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
