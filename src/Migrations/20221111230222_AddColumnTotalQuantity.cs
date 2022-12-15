using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnsualLotery.Migrations
{
    public partial class AddColumnTotalQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalQuantity",
                table: "Raffles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "Raffles");
        }
    }
}
