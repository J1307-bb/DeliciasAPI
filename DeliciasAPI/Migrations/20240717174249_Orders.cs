using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliciasAPI.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Meals_IdMeal",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_IdMeal",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "IdMeal",
                table: "Orders",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    IdOrderItem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IdMeal = table.Column<int>(type: "int", nullable: false),
                    OrderIdOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.IdOrderItem);
                    table.ForeignKey(
                        name: "FK_OrderItem_Meals_IdMeal",
                        column: x => x.IdMeal,
                        principalTable: "Meals",
                        principalColumn: "IdMeal",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderIdOrder",
                        column: x => x.OrderIdOrder,
                        principalTable: "Orders",
                        principalColumn: "IdOrder");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_IdMeal",
                table: "OrderItem",
                column: "IdMeal");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderIdOrder",
                table: "OrderItem",
                column: "OrderIdOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "IdMeal");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdMeal",
                table: "Orders",
                column: "IdMeal");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Meals_IdMeal",
                table: "Orders",
                column: "IdMeal",
                principalTable: "Meals",
                principalColumn: "IdMeal",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
