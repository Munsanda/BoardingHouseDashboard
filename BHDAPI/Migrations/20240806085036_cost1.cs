using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHDAPI.Migrations
{
    /// <inheritdoc />
    public partial class cost1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    BoardingHouseId = table.Column<int>(type: "int", nullable: false),
                    rentId = table.Column<int>(type: "int", nullable: true),
                    repairId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cost_BoardingHouses_BoardingHouseId",
                        column: x => x.BoardingHouseId,
                        principalTable: "BoardingHouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cost_Rents_rentId",
                        column: x => x.rentId,
                        principalTable: "Rents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cost_Repairs_repairId",
                        column: x => x.repairId,
                        principalTable: "Repairs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cost_BoardingHouseId",
                table: "Cost",
                column: "BoardingHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Cost_rentId",
                table: "Cost",
                column: "rentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cost_repairId",
                table: "Cost",
                column: "repairId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cost");
        }
    }
}
