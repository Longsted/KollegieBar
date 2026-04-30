using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DropColumn(
                name: "PriceAtSale",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "Drinks");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Drinks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "DrinkLiquid",
                columns: new[] { "DrinkId", "IngredientsId" },
                values: new object[,]
                {
                    { 60, 38 },
                    { 60, 41 },
                    { 61, 26 },
                    { 61, 31 },
                    { 61, 38 },
                    { 62, 42 },
                    { 63, 43 },
                    { 64, 39 },
                    { 64, 44 },
                    { 65, 43 },
                    { 65, 56 },
                    { 66, 20 },
                    { 66, 25 },
                    { 67, 41 },
                    { 67, 45 },
                    { 68, 30 },
                    { 68, 38 },
                    { 68, 57 },
                    { 69, 30 },
                    { 70, 20 },
                    { 70, 39 },
                    { 90, 26 },
                    { 90, 38 },
                    { 91, 25 },
                    { 92, 38 },
                    { 92, 57 },
                    { 93, 39 },
                    { 94, 39 },
                    { 94, 80 }
                });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 60,
                column: "Description",
                value: "Råstoff Strawberry/Rhubarb, Lemon Soda");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 61,
                column: "Description",
                value: "Pink Gin, Lime Juice, Lemon Soda, Cranberry Juice");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 62,
                column: "Description",
                value: "Jägermeister, Red Soda");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 63,
                column: "Description",
                value: "Cuba Caramel, Green Soda");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 64,
                column: "Description",
                value: "Cuba Kurant, Faxe Kondi, Lime Juice");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 65,
                column: "Description",
                value: "Cuba Caramel, Chocolate Milk");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 66,
                column: "Description",
                value: "Vodka, Red Soda, Orange Juice");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 67,
                column: "Description",
                value: "Råstoff Liquorice, Råstoff Strawberry/Rhubarb, Orange Soda");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 68,
                column: "Description",
                value: "Gin, Mango Syrup, Lemon Soda");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Gin/Pink Gin, Tonic Water/Lemon Soda", "Gin & Tonic/Lemon" });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Vodka, Blue Curacao, Faxe Kondi", "Isbjørn" });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Non-Alcoholic Gin, Lemon Soda, Cranberry Juice, Lime Juice", "Boring Bitch" });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Non-Alcoholic Gin, Red Soda, Orange Juice", "Filur Free" });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Non-Alcoholic Gin, Mango Syrup, Lemon Soda", "Gin Love" });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Non-Alcoholic Gin, Blue Curacao, Faxe Kondi/Free", "Panda" });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Non-Alcoholic Gin, Blue Curacao, Faxe Kondi/Free (+ Grenadine)", "(Levende) Panda" });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "Description", "IsAlcoholic", "Name" },
                values: new object[,]
                {
                    { 71, "Vodka, Mango Syrup, Cranberry Juice, Orange Juice", true, "Sex On The Beach" },
                    { 72, "Pisang Ambon, Chocolate Milk", true, "Skumbanan" },
                    { 73, "Southern Comfort, Lime Syrup, Faxe Kondi", true, "Southern & Sprite" },
                    { 74, "Rum, Pepsi/Max", true, "Rum & Coke" },
                    { 75, "Tequila, Grenadine, Orange Juice", true, "Tequila Sunrise" },
                    { 76, "Kahlua, Vodka, Milk/Chocolate Milk", true, "White Russian" },
                    { 77, "Bailey, Chocolate Milk", true, "Chocolate & Bailey" },
                    { 78, "Pisang Ambon, Orange Juice", true, "Green Goblin" },
                    { 79, "Cointreau, Vodka, Cranberry Juice, Lime Juice", true, "Cosmopolitan" },
                    { 80, "Jägermeister, Shaker", true, "Shaker Jäger" },
                    { 81, "Vodka, Energy Drink", true, "Vodka Energy" },
                    { 82, "Southern Comfort, Vodka, Lemon, Passion Syrup, Faxe Kondi", true, "3-Meter-Vippen" },
                    { 83, "Cointreau, Vodka, Passion, Mango Syrup, Grenadine, Faxe Kondi", true, "Exotic" },
                    { 84, "Cointreau, Gin, Rum, Tequila, Vodka, Lemon Juice, Pepsi/Max", true, "Long Island Iced Tea" },
                    { 85, "Gin, Pisang Ambon, Rum, Vodka, Lemon Juice, Lemon Syrup, Faxe Kondi", true, "Lille Fugl Fald Død Om" },
                    { 95, "Non-Alcoholic Gin, Mango Syrup, Cranberry Juice, Orange Juice", false, "Virgin Sex" },
                    { 96, "Non-Alcoholic Gin, Grenadine, Orange Juice", false, "Gin Sunset" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CostPrice",
                value: 7.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CostPrice", "Name", "StockQuantity" },
                values: new object[] { 7.00m, "Albani Odense Classic", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AlcoholPercentage", "CostPrice", "Name", "StockQuantity" },
                values: new object[] { 0.0, 7.00m, "Royal Pilsner (0%)", 10 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                column: "CostPrice",
                value: 90.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CostPrice", "Name", "StockQuantity" },
                values: new object[] { 10.00m, "Orange Juice", 6 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CostPrice", "Name", "StockQuantity" },
                values: new object[] { 10.00m, "Cranberry Juice", 5 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30,
                column: "CostPrice",
                value: 110.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                column: "CostPrice",
                value: 110.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Name", "SugarFree" },
                values: new object[] { "Faxe Kondi Free", true });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43,
                column: "CostPrice",
                value: 90.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44,
                column: "CostPrice",
                value: 90.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "AlcoholPercentage", "CostPrice", "Name", "StockQuantity", "VolumeCl" },
                values: new object[] { 31.0, 120.00m, "Licor 43", 5, 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "AlcoholPercentage", "CostPrice", "Name", "StockQuantity", "VolumeCl" },
                values: new object[] { 16.399999999999999, 130.00m, "Råstoff Pineapple/Vanilla", 5, 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "CostPrice", "Name", "StockQuantity", "SugarFree", "VolumeCl" },
                values: new object[] { 12.00m, "Energy Drink", 20, true, 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "CostPrice", "StockQuantity" },
                values: new object[] { 10.00m, 5 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "CostPrice", "Name", "StockQuantity", "VolumeCl" },
                values: new object[] { 45.00m, "Mango Syrup", 1, 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "StockQuantity", "VolumeCl" },
                values: new object[] { 1, 70 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "CostPrice", "Name", "StockQuantity", "VolumeCl" },
                values: new object[] { 30.00m, "Lemon Syrup", 1, 70 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CostPrice", "Discriminator", "MaxStockQuantity", "MinStockQuantity", "Name", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 7.00m, "Snack", 0, 0, "Popcorn", 4 },
                    { 2, 10.00m, "Snack", 0, 0, "Chips", 4 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AlcoholPercentage", "CostPrice", "Discriminator", "MaxStockQuantity", "MinStockQuantity", "Name", "Pant", "StockQuantity", "SugarFree", "VolumeCl" },
                values: new object[,]
                {
                    { 6, 4.5, 10.00m, "Liquid", 0, 0, "Shaker", 0, 10, false, 33 },
                    { 32, 37.5, 150.00m, "Liquid", 0, 0, "Rum", 0, 10, false, 70 },
                    { 33, 38.0, 160.00m, "Liquid", 0, 0, "Tequila", 0, 5, false, 70 },
                    { 34, 40.0, 180.00m, "Liquid", 0, 0, "Cointreau", 0, 5, false, 70 },
                    { 35, 20.0, 110.00m, "Liquid", 0, 0, "Kahlua", 0, 5, false, 70 },
                    { 50, 0.0, 10.00m, "Liquid", 0, 0, "Pepsi", 0, 100, false, 33 },
                    { 51, 0.0, 10.00m, "Liquid", 0, 0, "Pepsi Max", 0, 100, true, 33 },
                    { 52, 0.0, 10.00m, "Liquid", 0, 0, "Red Soda", 0, 100, false, 33 },
                    { 53, 0.0, 10.00m, "Liquid", 0, 0, "Green Soda", 0, 100, false, 33 },
                    { 54, 0.0, 10.00m, "Liquid", 0, 0, "Orange Soda", 0, 100, false, 33 },
                    { 58, 0.0, 45.00m, "Liquid", 0, 0, "Passion Syrup", 0, 1, false, 70 },
                    { 59, 0.0, 6.00m, "Liquid", 0, 0, "Lime Juice", 0, 7, false, 50 },
                    { 75, 0.0, 120.00m, "Liquid", 0, 0, "Blue Curacao", 0, 5, false, 70 },
                    { 76, 0.0, 10.00m, "Liquid", 0, 0, "Tonic Water", 0, 10, false, 33 },
                    { 83, 0.0, 6.00m, "Liquid", 0, 0, "Lemon Juice", 0, 2, false, 50 },
                    { 100, 0.0, 120.00m, "Liquid", 0, 0, "Non-Alcoholic Gin", 0, 5, false, 70 }
                });

            migrationBuilder.InsertData(
                table: "DrinkLiquid",
                columns: new[] { "DrinkId", "IngredientsId" },
                values: new object[,]
                {
                    { 61, 59 },
                    { 62, 52 },
                    { 63, 53 },
                    { 64, 59 },
                    { 66, 52 },
                    { 67, 54 },
                    { 69, 76 },
                    { 70, 75 },
                    { 72, 47 },
                    { 72, 56 },
                    { 73, 39 },
                    { 73, 46 },
                    { 73, 81 },
                    { 74, 32 },
                    { 74, 50 },
                    { 80, 42 },
                    { 80, 55 },
                    { 81, 6 },
                    { 81, 20 },
                    { 82, 20 },
                    { 82, 39 },
                    { 82, 46 },
                    { 82, 58 },
                    { 82, 81 },
                    { 83, 20 },
                    { 83, 34 },
                    { 83, 39 },
                    { 83, 57 },
                    { 83, 58 },
                    { 83, 80 },
                    { 84, 20 },
                    { 84, 30 },
                    { 84, 32 },
                    { 84, 33 },
                    { 84, 34 },
                    { 84, 50 },
                    { 84, 59 },
                    { 85, 20 },
                    { 85, 30 },
                    { 85, 32 },
                    { 85, 39 },
                    { 85, 47 },
                    { 85, 59 },
                    { 90, 59 },
                    { 90, 100 },
                    { 91, 52 },
                    { 91, 100 },
                    { 92, 100 },
                    { 93, 75 },
                    { 93, 100 },
                    { 94, 75 },
                    { 94, 100 },
                    { 95, 25 },
                    { 95, 26 },
                    { 95, 57 },
                    { 95, 100 },
                    { 96, 25 },
                    { 96, 80 },
                    { 96, 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 60, 38 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 60, 41 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 61, 26 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 61, 31 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 61, 38 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 61, 59 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 62, 42 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 62, 52 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 63, 43 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 63, 53 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 64, 39 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 64, 44 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 64, 59 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 65, 43 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 65, 56 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 66, 20 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 66, 25 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 66, 52 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 67, 41 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 67, 45 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 67, 54 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 68, 30 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 68, 38 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 68, 57 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 69, 30 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 69, 76 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 70, 20 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 70, 39 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 70, 75 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 72, 47 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 72, 56 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 73, 39 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 73, 46 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 73, 81 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 74, 32 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 74, 50 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 80, 42 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 80, 55 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 81, 6 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 81, 20 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 82, 20 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 82, 39 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 82, 46 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 82, 58 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 82, 81 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 83, 20 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 83, 34 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 83, 39 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 83, 57 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 83, 58 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 83, 80 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 84, 20 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 84, 30 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 84, 32 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 84, 33 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 84, 34 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 84, 50 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 84, 59 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 85, 20 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 85, 30 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 85, 32 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 85, 39 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 85, 47 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 85, 59 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 90, 26 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 90, 38 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 90, 59 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 90, 100 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 91, 25 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 91, 52 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 91, 100 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 92, 38 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 92, 57 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 92, 100 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 93, 39 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 93, 75 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 93, 100 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 94, 39 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 94, 75 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 94, 80 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 94, 100 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 95, 25 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 95, 26 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 95, 57 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 95, 100 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 96, 25 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 96, 80 });

            migrationBuilder.DeleteData(
                table: "DrinkLiquid",
                keyColumns: new[] { "DrinkId", "IngredientsId" },
                keyValues: new object[] { 96, 100 });

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Drinks");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAtSale",
                table: "Sales",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "CostPrice",
                table: "Drinks",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 60,
                column: "CostPrice",
                value: 20.0);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 61,
                column: "CostPrice",
                value: 20.0);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 62,
                column: "CostPrice",
                value: 20.0);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 63,
                column: "CostPrice",
                value: 20.0);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 64,
                column: "CostPrice",
                value: 20.0);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 65,
                column: "CostPrice",
                value: 20.0);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 66,
                column: "CostPrice",
                value: 20.0);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 67,
                column: "CostPrice",
                value: 20.0);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 68,
                column: "CostPrice",
                value: 20.0);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "CostPrice", "Name" },
                values: new object[] { 20.0, "Skumbanan" });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "CostPrice", "Name" },
                values: new object[] { 20.0, "Southern & Sprite" });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "CostPrice", "Name" },
                values: new object[] { 20.0, "Børnebrandbil" });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "CostPrice", "Name" },
                values: new object[] { 20.0, "Børnechampagnebrus" });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "CostPrice", "Name" },
                values: new object[] { 20.0, "Børnefilur" });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "CostPrice", "Name" },
                values: new object[] { 20.0, "Børneastronaut" });

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "CostPrice", "Name" },
                values: new object[] { 20.0, "Safe Sex On The Beach" });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "CostPrice", "IsAlcoholic", "Name" },
                values: new object[,]
                {
                    { 10, 85.0, true, "Sex on the beach" },
                    { 11, 95.0, true, "Long Island Iced Tea" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CostPrice",
                value: 5.50m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CostPrice", "Name", "StockQuantity" },
                values: new object[] { 6.00m, "Albani øl", 150 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AlcoholPercentage", "CostPrice", "Name", "StockQuantity" },
                values: new object[] { 4.5, 12.00m, "Shaker Sport", 80 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                column: "CostPrice",
                value: 150.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CostPrice", "Name", "StockQuantity" },
                values: new object[] { 15.00m, "Appelsinjuice", 20 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CostPrice", "Name", "StockQuantity" },
                values: new object[] { 18.00m, "Tranebærjuice", 20 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30,
                column: "CostPrice",
                value: 140.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                column: "CostPrice",
                value: 145.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Name", "SugarFree" },
                values: new object[] { "Tonic Water", false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43,
                column: "CostPrice",
                value: 125.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44,
                column: "CostPrice",
                value: 125.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "AlcoholPercentage", "CostPrice", "Name", "StockQuantity", "VolumeCl" },
                values: new object[] { 0.0, 10.00m, "Red Soda", 100, 33 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "AlcoholPercentage", "CostPrice", "Name", "StockQuantity", "VolumeCl" },
                values: new object[] { 0.0, 10.00m, "Green Soda", 100, 33 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "CostPrice", "Name", "StockQuantity", "SugarFree", "VolumeCl" },
                values: new object[] { 10.00m, "Orange Soda", 100, false, 33 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "CostPrice", "StockQuantity" },
                values: new object[] { 12.00m, 40 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "CostPrice", "Name", "StockQuantity", "VolumeCl" },
                values: new object[] { 25.00m, "Lime Juice", 15, 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "StockQuantity", "VolumeCl" },
                values: new object[] { 15, 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "CostPrice", "Name", "StockQuantity", "VolumeCl" },
                values: new object[] { 15.00m, "Saftevand (Rød)", 50, 100 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CostPrice", "Discriminator", "MaxStockQuantity", "MinStockQuantity", "Name", "StockQuantity" },
                values: new object[,]
                {
                    { 6, 10.00m, "Snack", 0, 0, "Popcorn", 50 },
                    { 7, 15.00m, "Snack", 0, 0, "Chips", 40 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AlcoholPercentage", "CostPrice", "Discriminator", "MaxStockQuantity", "MinStockQuantity", "Name", "Pant", "StockQuantity", "SugarFree", "VolumeCl" },
                values: new object[,]
                {
                    { 27, 18.0, 120.00m, "Liquid", 0, 0, "Peach Schnapps", 0, 5, false, 70 },
                    { 36, 0.0, 45.00m, "Liquid", 0, 0, "Mango Syrup", 0, 10, false, 70 },
                    { 82, 0.0, 18.00m, "Liquid", 0, 0, "Ananasjuice", 0, 20, false, 100 }
                });
        }
    }
}
