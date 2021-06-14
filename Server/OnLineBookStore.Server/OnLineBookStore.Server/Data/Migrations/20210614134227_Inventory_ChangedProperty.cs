using Microsoft.EntityFrameworkCore.Migrations;

namespace OnLineBookStore.Server.Data.Migrations
{
    public partial class Inventory_ChangedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PercentageLimitTreshhold",
                table: "Inventories",
                newName: "QantityLimitTreshhold");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QantityLimitTreshhold",
                table: "Inventories",
                newName: "PercentageLimitTreshhold");
        }
    }
}
