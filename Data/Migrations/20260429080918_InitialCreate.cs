using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CostPrice = table.Column<double>(type: "double precision", nullable: false),
                    IsAlcoholic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CostPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    MaxStockQuantity = table.Column<int>(type: "integer", nullable: false),
                    MinStockQuantity = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    VolumeCl = table.Column<int>(type: "integer", nullable: true),
                    Pant = table.Column<int>(type: "integer", nullable: true),
                    AlcoholPercentage = table.Column<double>(type: "double precision", nullable: true),
                    SugarFree = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrinkLiquid",
                columns: table => new
                {
                    DrinkId = table.Column<int>(type: "integer", nullable: false),
                    IngredientsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkLiquid", x => new { x.DrinkId, x.IngredientsId });
                    table.ForeignKey(
                        name: "FK_DrinkLiquid_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkLiquid_Products_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PriceAtSale = table.Column<decimal>(type: "numeric", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    DrinkId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sales_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "CostPrice", "IsAlcoholic", "Name" },
                values: new object[,]
                {
                    { 10, 85.0, true, "Sex on the beach" },
                    { 11, 95.0, true, "Long Island Iced Tea" },
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
                    { 70, 20.0, true, "Southern & Sprite" },
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
                    { 3, 4.5999999999999996, 5.50m, "Liquid", 0, 0, "Ceres Top", 0, 100, false, 33 },
                    { 4, 4.5999999999999996, 6.00m, "Liquid", 0, 0, "Albani øl", 0, 150, false, 33 },
                    { 5, 4.5, 12.00m, "Liquid", 0, 0, "Shaker Sport", 0, 80, false, 33 }
                });

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
                    { 20, 37.5, 150.00m, "Liquid", 0, 0, "Vodka", 0, 10, false, 70 },
                    { 25, 0.0, 15.00m, "Liquid", 0, 0, "Appelsinjuice", 0, 20, false, 100 },
                    { 26, 0.0, 18.00m, "Liquid", 0, 0, "Tranebærjuice", 0, 20, false, 100 },
                    { 27, 18.0, 120.00m, "Liquid", 0, 0, "Peach Schnapps", 0, 5, false, 70 },
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
                    { 57, 0.0, 25.00m, "Liquid", 0, 0, "Lime Juice", 0, 15, false, 50 },
                    { 80, 0.0, 40.00m, "Liquid", 0, 0, "Grenadine", 0, 15, false, 50 },
                    { 81, 0.0, 15.00m, "Liquid", 0, 0, "Saftevand (Rød)", 0, 50, false, 100 },
                    { 82, 0.0, 18.00m, "Liquid", 0, 0, "Ananasjuice", 0, 20, false, 100 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "1234", 1, "Admin" },
                    { 2, "1234", 0, "bar" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkLiquid_IngredientsId",
                table: "DrinkLiquid",
                column: "IngredientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_DrinkId",
                table: "Sales",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkLiquid");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Drinks");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
