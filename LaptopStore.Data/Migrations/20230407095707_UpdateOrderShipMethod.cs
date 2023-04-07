using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaptopStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderShipMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Img",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ShipMethod",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("597c8190-753d-4bb4-9253-c23bfe7d192c"),
                column: "Password",
                value: "$2b$10$KF3/gFF3xa3bPBBp7ceQkOJmoDTtXkYc.BJT2HXwBGuk7iilI2gHa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipMethod",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Img",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("597c8190-753d-4bb4-9253-c23bfe7d192c"),
                column: "Password",
                value: "$2b$10$zTEkF52x2/wMXsfRoNlC2eiV53l3UCf5W6AtYrePhRoT/hMWl4fLy");
        }
    }
}
