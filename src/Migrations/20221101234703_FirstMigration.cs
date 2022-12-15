using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnsualLotery.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    QuotasAmount = table.Column<int>(type: "int", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EditedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Raffles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EditedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raffles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quotas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RaffleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuotasAmount = table.Column<int>(type: "int", nullable: false),
                    QuotasNumbers = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    RaffleFk = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EditedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quotas_Raffles_RaffleId",
                        column: x => x.RaffleId,
                        principalTable: "Raffles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotas_RaffleId",
                table: "Quotas",
                column: "RaffleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Quotas");

            migrationBuilder.DropTable(
                name: "Raffles");
        }
    }
}
