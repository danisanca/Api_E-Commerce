using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEstoque.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Discount");

            migrationBuilder.AddColumn<Guid>(
                name: "shopId",
                table: "Stock",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Stock_shopId",
                table: "Stock",
                column: "shopId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_productId",
                table: "Image",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Product_productId",
                table: "Image",
                column: "productId",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Shop_shopId",
                table: "Stock",
                column: "shopId",
                principalTable: "Shop",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Product_productId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Shop_shopId",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Stock_shopId",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Image_productId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "shopId",
                table: "Stock");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Discount",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
