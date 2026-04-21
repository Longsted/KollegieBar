using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class RestructuredAllModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkIngredients_Products_LiquidProductId",
                table: "DrinkIngredients");

            migrationBuilder.DropIndex(
                name: "IX_DrinkIngredients_LiquidProductId",
                table: "DrinkIngredients");

            migrationBuilder.DropColumn(
                name: "SalesPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Snack_SalesPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalesPrice",
                table: "Drinks");

            migrationBuilder.AlterColumn<double>(
                name: "Pant",
                table: "Products",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Products",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(21)",
                oldMaxLength: 21);

            migrationBuilder.AddColumn<int>(
                name: "LiquidId",
                table: "DrinkIngredients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DrinkIngredients_LiquidId",
                table: "DrinkIngredients",
                column: "LiquidId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkIngredients_Products_LiquidId",
                table: "DrinkIngredients",
                column: "LiquidId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkIngredients_Products_LiquidId",
                table: "DrinkIngredients");

            migrationBuilder.DropIndex(
                name: "IX_DrinkIngredients_LiquidId",
                table: "DrinkIngredients");

            migrationBuilder.DropColumn(
                name: "LiquidId",
                table: "DrinkIngredients");

            migrationBuilder.AlterColumn<decimal>(
                name: "Pant",
                table: "Products",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Products",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<decimal>(
                name: "SalesPrice",
                table: "Products",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Snack_SalesPrice",
                table: "Products",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SalesPrice",
                table: "Drinks",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_DrinkIngredients_LiquidProductId",
                table: "DrinkIngredients",
                column: "LiquidProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkIngredients_Products_LiquidProductId",
                table: "DrinkIngredients",
                column: "LiquidProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
