using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCartDetailsDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "CartDetail",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CartDetail",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "CartDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "CartDetail");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartDetail");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "CartDetail");
        }
    }
}
