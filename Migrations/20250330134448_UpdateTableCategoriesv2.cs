using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEstoque.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableCategoriesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Shop_shopId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_shopId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "shopId",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "imageUrl",
                table: "Categories",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "imageUrl",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "shopId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_shopId",
                table: "Categories",
                column: "shopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Shop_shopId",
                table: "Categories",
                column: "shopId",
                principalTable: "Shop",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
