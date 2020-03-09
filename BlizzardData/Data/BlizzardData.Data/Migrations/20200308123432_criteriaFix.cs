using Microsoft.EntityFrameworkCore.Migrations;

namespace BlizzardData.Data.Migrations
{
    public partial class criteriaFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Criteria");

            migrationBuilder.AddColumn<string>(
                name: "BlizzardDescription",
                table: "Criteria",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Criteria",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RootId",
                table: "Criteria",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlizzardDescription",
                table: "Criteria");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Criteria");

            migrationBuilder.DropColumn(
                name: "RootId",
                table: "Criteria");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Criteria",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
