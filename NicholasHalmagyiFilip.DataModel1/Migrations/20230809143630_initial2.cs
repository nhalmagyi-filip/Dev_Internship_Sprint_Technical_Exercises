/*
using Microsoft.EntityFrameworkCore.Migrations;

namespace NicholasHalmagyiFilip.DataModel.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrugUnits_Depots_DrugUnitDepotId",
                table: "DrugUnits");

            migrationBuilder.AlterColumn<int>(
                name: "DrugUnitDepotId",
                table: "DrugUnits",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DrugUnits_Depots_DrugUnitDepotId",
                table: "DrugUnits",
                column: "DrugUnitDepotId",
                principalTable: "Depots",
                principalColumn: "DepotId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrugUnits_Depots_DrugUnitDepotId",
                table: "DrugUnits");

            migrationBuilder.AlterColumn<int>(
                name: "DrugUnitDepotId",
                table: "DrugUnits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DrugUnits_Depots_DrugUnitDepotId",
                table: "DrugUnits",
                column: "DrugUnitDepotId",
                principalTable: "Depots",
                principalColumn: "DepotId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
*/