using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class CockTails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "CostPrice", "IsAlcoholic", "Name" },
                values: new object[,]
                {
                    { 60, 20.0, true, "Astronaut" },
                    { 61, 20.0, true, "Basic Bitch" },
                    { 62, 20.0, true, "Brandbil" },
                    { 63, 20.0, true, "Champagnebrus" },
                    { 64, 20.0, true, "Purple Rain" },
                    { 65, 20.0, true, "Dumle" },
                    { 66, 20.0, true, "Filur" },
                    { 67, 20.0, true, "Flagermus" },
                    { 68, 20.0, true, "Gin Hass" },
                    { 69, 20.0, true, "Skumbanan" },
                    { 70, 20.0, true, "Southern & Sprite" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AlcoholPercentage", "CostPrice", "Discriminator", "MaxStockQuantity", "MinStockQuantity", "Name", "Pant", "StockQuantity", "SugarFree", "VolumeCl" },
                values: new object[,]
                {
                    { 30, 37.5, 140.00m, "Liquid", 0, 0, "Gin", 0, 8, false, 70 },
                    { 31, 37.5, 145.00m, "Liquid", 0, 0, "Pink Gin", 0, 5, false, 70 },
                    { 36, 0.0, 45.00m, "Liquid", 0, 0, "Mango Syrup", 0, 10, false, 70 },
                    { 38, 0.0, 10.00m, "Liquid", 0, 0, "Lemon Soda", 0, 100, false, 33 },
                    { 39, 0.0, 10.00m, "Liquid", 0, 0, "Faxe Kondi", 0, 100, false, 33 },
                    { 40, 0.0, 10.00m, "Liquid", 0, 0, "Tonic Water", 0, 100, false, 33 },
                    { 41, 16.399999999999999, 130.00m, "Liquid", 0, 0, "Råstoff Strawberry/Rhubarb", 0, 10, false, 70 },
                    { 42, 35.0, 155.00m, "Liquid", 0, 0, "Jägermeister", 0, 5, false, 70 },
                    { 43, 30.0, 125.00m, "Liquid", 0, 0, "Cuba Caramel", 0, 8, false, 70 },
                    { 44, 30.0, 125.00m, "Liquid", 0, 0, "Cuba Kurant", 0, 8, false, 70 },
                    { 45, 16.399999999999999, 130.00m, "Liquid", 0, 0, "Råstoff Liquorice", 0, 10, false, 70 },
                    { 46, 35.0, 165.00m, "Liquid", 0, 0, "Southern Comfort", 0, 4, false, 70 },
                    { 47, 20.0, 135.00m, "Liquid", 0, 0, "Pisang Ambon", 0, 6, false, 70 },
                    { 48, 0.0, 10.00m, "Liquid", 0, 0, "Red Soda", 0, 100, false, 33 },
                    { 49, 0.0, 10.00m, "Liquid", 0, 0, "Green Soda", 0, 100, false, 33 },
                    { 55, 0.0, 10.00m, "Liquid", 0, 0, "Orange Soda", 0, 100, false, 33 },
                    { 56, 0.0, 12.00m, "Liquid", 0, 0, "Chocolate Milk", 0, 40, false, 100 },
                    { 57, 0.0, 25.00m, "Liquid", 0, 0, "Lime Juice", 0, 15, false, 50 }
                });

            migrationBuilder.InsertData(
                table: "DrinkIngredients",
                columns: new[] { "Id", "DrinkId", "LiquidId", "LiquidProductId" },
                values: new object[,]
                {
                    { 30, 60, 41, 41 },
                    { 31, 60, 38, 38 },
                    { 32, 61, 31, 31 },
                    { 33, 61, 57, 57 },
                    { 34, 61, 38, 38 },
                    { 35, 61, 26, 26 },
                    { 36, 62, 42, 42 },
                    { 37, 62, 48, 48 },
                    { 38, 63, 43, 43 },
                    { 39, 63, 49, 49 },
                    { 40, 64, 44, 44 },
                    { 41, 64, 39, 39 },
                    { 42, 64, 57, 57 },
                    { 43, 65, 43, 43 },
                    { 44, 65, 56, 56 },
                    { 45, 66, 20, 20 },
                    { 46, 66, 48, 48 },
                    { 47, 66, 25, 25 },
                    { 48, 67, 45, 45 },
                    { 49, 67, 41, 41 },
                    { 50, 67, 55, 55 },
                    { 51, 68, 30, 30 },
                    { 52, 68, 36, 36 },
                    { 53, 68, 38, 38 },
                    { 54, 69, 47, 47 },
                    { 55, 69, 56, 56 },
                    { 56, 70, 46, 46 },
                    { 57, 70, 39, 39 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 57);
        }
    }
}
