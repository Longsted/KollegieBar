using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalSeedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "CostPrice", "IsAlcoholic", "Name" },
                values: new object[,]
                {
                    { 10, 85.0, true, "Sex on the beach" },
                    { 11, 95.0, true, "Long Island Iced Tea" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AlcoholPercentage", "CostPrice", "Discriminator", "MaxStockQuantity", "MinStockQuantity", "Name", "Pant", "StockQuantity", "SugarFree", "VolumeCl" },
                values: new object[,]
                {
                    { 20, 37.5, 150.00m, "Liquid", 0, 0, "Vodka", 0, 10, false, 70 },
                    { 25, 0.0, 15.00m, "Liquid", 0, 0, "Appelsinjuice", 0, 20, false, 100 },
                    { 26, 0.0, 18.00m, "Liquid", 0, 0, "Tranebærjuice", 0, 20, false, 100 },
                    { 27, 18.0, 120.00m, "Liquid", 0, 0, "Peach Schnapps", 0, 5, false, 70 }
                });

            migrationBuilder.InsertData(
                table: "DrinkIngredients",
                columns: new[] { "Id", "DrinkId", "LiquidId", "LiquidProductId", "VolumeCl" },
                values: new object[,]
                {
                    { 1, 10, 20, 20, 4 },
                    { 2, 10, 27, 27, 2 },
                    { 3, 10, 25, 25, 6 },
                    { 4, 10, 26, 26, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);
        }
    }
}
