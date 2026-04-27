using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class KevinDevRemoveDrinkIngredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DrinkId",
                table: "Products",
                type: "integer",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Drinks_DrinkId",
                table: "Products",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Drinks_DrinkId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DrinkId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DrinkId",
                table: "Products");
        }
    }
}
