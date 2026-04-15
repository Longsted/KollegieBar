using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "AlcoholPercentage",
                table: "Products",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxStockQuantity",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinStockQuantity",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxStockQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MinStockQuantity",
                table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "AlcoholPercentage",
                table: "Products",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);
        }
    }
}
