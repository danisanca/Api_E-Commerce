using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEstoque.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHistPurchDBv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryPurchase_Product_productId",
                table: "HistoryPurchase");

            migrationBuilder.DropIndex(
                name: "IX_HistoryPurchase_productId",
                table: "HistoryPurchase");

            migrationBuilder.DropColumn(
                name: "amount",
                table: "HistoryPurchase");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "HistoryPurchase");

            migrationBuilder.AlterColumn<string>(
                name: "externalReference",
                table: "HistoryPurchase",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "cartProducts",
                table: "HistoryPurchase",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cartProducts",
                table: "HistoryPurchase");

            migrationBuilder.AlterColumn<Guid>(
                name: "externalReference",
                table: "HistoryPurchase",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<float>(
                name: "amount",
                table: "HistoryPurchase",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "HistoryPurchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryPurchase_productId",
                table: "HistoryPurchase",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryPurchase_Product_productId",
                table: "HistoryPurchase",
                column: "productId",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
