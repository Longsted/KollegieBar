using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedVolumeCl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolumeCl",
                table: "DrinkIngredients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VolumeCl",
                table: "DrinkIngredients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 1,
                column: "VolumeCl",
                value: 4);

            migrationBuilder.UpdateData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 2,
                column: "VolumeCl",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 3,
                column: "VolumeCl",
                value: 6);

            migrationBuilder.UpdateData(
                table: "DrinkIngredients",
                keyColumn: "Id",
                keyValue: 4,
                column: "VolumeCl",
                value: 6);
        }
    }
}
