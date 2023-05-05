using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaptopStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_Transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPay",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("597c8190-753d-4bb4-9253-c23bfe7d192c"),
                column: "Password",
                value: "$2b$10$Ed3a1lsDYt2bC4fm0WhPru1YslYDmp0huVjIu9cRuB2HEkfOVZ3aq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPay",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("597c8190-753d-4bb4-9253-c23bfe7d192c"),
                column: "Password",
                value: "$2b$10$gjqKUCsl.eYLjcMDIC6xW.DbPtH2nlHemrFbyvmM6n2WDko5.qY42");
        }
    }
}
