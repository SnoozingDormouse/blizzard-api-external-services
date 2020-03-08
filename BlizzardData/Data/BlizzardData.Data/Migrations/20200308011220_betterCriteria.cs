using Microsoft.EntityFrameworkCore.Migrations;

namespace BlizzardData.Data.Migrations
{
    public partial class betterCriteria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Criteria");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Criteria",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Criteria",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Criteria");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Criteria");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Criteria",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
