using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelashionship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartDetail_CartHeaderId",
                table: "CartDetail");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "CartHeader",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CartHeader",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "CartDetail",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CartDetail",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_CartHeaderId",
                table: "CartDetail",
                column: "CartHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartDetail_CartHeaderId",
                table: "CartDetail");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "CartHeader",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CartHeader",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "CartDetail",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CartDetail",
                newName: "id");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_CartHeaderId",
                table: "CartDetail",
                column: "CartHeaderId",
                unique: true);
        }
    }
}
