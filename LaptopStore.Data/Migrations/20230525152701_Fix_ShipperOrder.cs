using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaptopStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_ShipperOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShipperOrders",
                table: "ShipperOrders");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ShipperOrders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShipperOrders",
                table: "ShipperOrders",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("597c8190-753d-4bb4-9253-c23bfe7d192c"),
                column: "Password",
                value: "$2b$10$qiX5aJ6Z3Piuzo1vvYL31ePh66Em7I9O0qAohpUAk3kZ8HqJN/j6u");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShipperOrders",
                table: "ShipperOrders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShipperOrders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShipperOrders",
                table: "ShipperOrders",
                columns: new[] { "OrderId", "UserId" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("597c8190-753d-4bb4-9253-c23bfe7d192c"),
                column: "Password",
                value: "$2b$10$NMCfA1qR.x1qX9goRb3WIO6vIiIGbqgrqvniwQwt77ZUWsSUf83Mm");
        }
    }
}
