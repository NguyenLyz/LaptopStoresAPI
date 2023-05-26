using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaptopStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddShipperOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShipperOrders",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipperOrders", x => new { x.OrderId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ShipperOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShipperOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("597c8190-753d-4bb4-9253-c23bfe7d192c"),
                column: "Password",
                value: "$2b$10$NMCfA1qR.x1qX9goRb3WIO6vIiIGbqgrqvniwQwt77ZUWsSUf83Mm");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperOrders_OrderId",
                table: "ShipperOrders",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShipperOrders_UserId",
                table: "ShipperOrders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipperOrders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("597c8190-753d-4bb4-9253-c23bfe7d192c"),
                column: "Password",
                value: "$2b$10$Nox4QAzndCruBCaHGVO4DefndzpZa1jxfj35UE3eEft0ajmmuMtBK");
        }
    }
}
