using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEstoque.Migrations
{
    /// <inheritdoc />
    public partial class insertAddressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adress",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    complement = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    neighborhood = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    city = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    state = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    zipcode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    cellPhone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adress", x => x.id);
                    table.ForeignKey(
                        name: "FK_Adress_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adress_userId",
                table: "Adress",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adress");
        }
    }
}
