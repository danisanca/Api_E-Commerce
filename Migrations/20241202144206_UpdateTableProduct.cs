using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEstoque.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "shopId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_shopId",
                table: "Product",
                column: "shopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Shop_shopId",
                table: "Product",
                column: "shopId",
                principalTable: "Shop",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Shop_shopId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_shopId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "shopId",
                table: "Product");
        }
    }
}
