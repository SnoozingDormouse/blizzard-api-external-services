using Microsoft.EntityFrameworkCore.Migrations;

namespace BlizzardData.Data.Migrations
{
    public partial class CriteriaRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlizzardDescription",
                table: "Criteria");

            migrationBuilder.DropColumn(
                name: "RootId",
                table: "Criteria");

            migrationBuilder.AddColumn<int>(
                name: "AchievementId",
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
                name: "AchievementId",
                table: "Criteria");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Criteria");

            migrationBuilder.AddColumn<string>(
                name: "BlizzardDescription",
                table: "Criteria",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RootId",
                table: "Criteria",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
