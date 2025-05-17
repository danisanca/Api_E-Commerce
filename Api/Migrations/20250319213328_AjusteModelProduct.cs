using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEstoque.Migrations
{
    /// <inheritdoc />
    public partial class AjusteModelProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Image_imageId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_imageId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "imageId",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "imageId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_imageId",
                table: "Product",
                column: "imageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Image_imageId",
                table: "Product",
                column: "imageId",
                principalTable: "Image",
                principalColumn: "id");
        }
    }
}
