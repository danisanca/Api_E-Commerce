using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBaseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "CartHeader",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CartHeader",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CartDetail",
                newName: "id");

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "CartDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "updatedAt",
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
                name: "id",
                table: "CartDetail",
                newName: "Id");
        }
    }
}
