using Microsoft.EntityFrameworkCore.Migrations;

namespace OnLineBookStore.Server.Data.Migrations
{
    public partial class AddedQantityLimitToBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityLimit",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityLimit",
                table: "Books");
        }
    }
}
