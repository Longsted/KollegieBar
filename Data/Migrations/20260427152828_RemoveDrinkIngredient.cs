using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDrinkIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Drinks_DrinkId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "DrinkIngredients");

            migrationBuilder.DropIndex(
                name: "IX_Products_DrinkId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DrinkId",
                table: "Products");

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

            migrationBuilder.CreateIndex(
                name: "IX_DrinkLiquid_IngredientsId",
                table: "DrinkLiquid",
                column: "IngredientsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkLiquid");

            migrationBuilder.AddColumn<int>(
                name: "DrinkId",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DrinkIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DrinkId = table.Column<int>(type: "integer", nullable: false),
                    LiquidId = table.Column<int>(type: "integer", nullable: false),
                    LiquidProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrinkIngredients_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkIngredients_Products_LiquidId",
                        column: x => x.LiquidId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DrinkIngredients",
                columns: new[] { "Id", "DrinkId", "LiquidId", "LiquidProductId" },
                values: new object[,]
                {
                    { 1, 10, 20, 20 },
                    { 2, 10, 27, 27 },
                    { 3, 10, 25, 25 },
                    { 4, 10, 26, 26 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DrinkId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "DrinkId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "DrinkId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                column: "DrinkId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                column: "DrinkId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                column: "DrinkId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                column: "DrinkId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Products_DrinkId",
                table: "Products",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkIngredients_DrinkId",
                table: "DrinkIngredients",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkIngredients_LiquidId",
                table: "DrinkIngredients",
                column: "LiquidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Drinks_DrinkId",
                table: "Products",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id");
        }
    }
}
