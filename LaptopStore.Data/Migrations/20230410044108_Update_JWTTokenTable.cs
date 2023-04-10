using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaptopStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_JWTTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JWTToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JWTToken", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_JWTToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("597c8190-753d-4bb4-9253-c23bfe7d192c"),
                column: "Password",
                value: "$2b$10$9kE1dzrS/yXX3QdBFQvcxew9.cPAQVUyDBQeY0Ag0MxJAos9meiGi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JWTToken");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("597c8190-753d-4bb4-9253-c23bfe7d192c"),
                column: "Password",
                value: "$2b$10$I8mHTQso/6WZ7v4wtoyI3.0bx3r6SheIkMC0FkdzCyNx/xIB0X2lW");
        }
    }
}
