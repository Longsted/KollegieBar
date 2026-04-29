using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMocktails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "CostPrice", "IsAlcoholic", "Name" },
                values: new object[,]
                {
                    { 90, 20.0, false, "Børnebrandbil" },
                    { 91, 20.0, false, "Børnechampagnebrus" },
                    { 92, 20.0, false, "Børnefilur" },
                    { 93, 20.0, false, "Børneastronaut" },
                    { 94, 20.0, false, "Safe Sex On The Beach" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AlcoholPercentage", "CostPrice", "Discriminator", "MaxStockQuantity", "MinStockQuantity", "Name", "Pant", "StockQuantity", "SugarFree", "VolumeCl" },
                values: new object[,]
                {
                    { 80, 0.0, 40.00m, "Liquid", 0, 0, "Grenadine", 0, 15, false, 50 },
                    { 81, 0.0, 15.00m, "Liquid", 0, 0, "Saftevand (Rød)", 0, 50, false, 100 },
                    { 82, 0.0, 18.00m, "Liquid", 0, 0, "Ananasjuice", 0, 20, false, 100 }
                });

            migrationBuilder.InsertData(
                table: "DrinkIngredients",
                columns: new[] { "Id", "DrinkId", "LiquidId", "LiquidProductId" },
                values: new object[,]
                {
                    { 100, 90, 81, 81 },
                    { 101, 90, 48, 48 },
                    { 102, 91, 81, 81 },
                    { 103, 91, 49, 49 },
                    { 104, 92, 25, 25 },
                    { 105, 92, 48, 48 },
                    { 106, 93, 81, 81 },
                    { 107, 93, 38, 38 },
                    { 108, 94, 25, 25 },
                    { 109, 94, 26, 26 },
                    { 110, 94, 82, 82 },
                    { 111, 94, 80, 80 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 82);
        }
    }
}
