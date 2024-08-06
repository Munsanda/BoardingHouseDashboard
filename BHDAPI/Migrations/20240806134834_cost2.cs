using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHDAPI.Migrations
{
    /// <inheritdoc />
    public partial class cost2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cost_BoardingHouses_BoardingHouseId",
                table: "Cost");

            migrationBuilder.DropForeignKey(
                name: "FK_Cost_Rents_rentId",
                table: "Cost");

            migrationBuilder.DropForeignKey(
                name: "FK_Cost_Repairs_repairId",
                table: "Cost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cost",
                table: "Cost");

            migrationBuilder.RenameTable(
                name: "Cost",
                newName: "Costs");

            migrationBuilder.RenameIndex(
                name: "IX_Cost_repairId",
                table: "Costs",
                newName: "IX_Costs_repairId");

            migrationBuilder.RenameIndex(
                name: "IX_Cost_rentId",
                table: "Costs",
                newName: "IX_Costs_rentId");

            migrationBuilder.RenameIndex(
                name: "IX_Cost_BoardingHouseId",
                table: "Costs",
                newName: "IX_Costs_BoardingHouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Costs",
                table: "Costs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_BoardingHouses_BoardingHouseId",
                table: "Costs",
                column: "BoardingHouseId",
                principalTable: "BoardingHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Rents_rentId",
                table: "Costs",
                column: "rentId",
                principalTable: "Rents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Repairs_repairId",
                table: "Costs",
                column: "repairId",
                principalTable: "Repairs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_BoardingHouses_BoardingHouseId",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Rents_rentId",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Repairs_repairId",
                table: "Costs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Costs",
                table: "Costs");

            migrationBuilder.RenameTable(
                name: "Costs",
                newName: "Cost");

            migrationBuilder.RenameIndex(
                name: "IX_Costs_repairId",
                table: "Cost",
                newName: "IX_Cost_repairId");

            migrationBuilder.RenameIndex(
                name: "IX_Costs_rentId",
                table: "Cost",
                newName: "IX_Cost_rentId");

            migrationBuilder.RenameIndex(
                name: "IX_Costs_BoardingHouseId",
                table: "Cost",
                newName: "IX_Cost_BoardingHouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cost",
                table: "Cost",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cost_BoardingHouses_BoardingHouseId",
                table: "Cost",
                column: "BoardingHouseId",
                principalTable: "BoardingHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cost_Rents_rentId",
                table: "Cost",
                column: "rentId",
                principalTable: "Rents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cost_Repairs_repairId",
                table: "Cost",
                column: "repairId",
                principalTable: "Repairs",
                principalColumn: "Id");
        }
    }
}
