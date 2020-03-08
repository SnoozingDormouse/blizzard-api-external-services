using Microsoft.EntityFrameworkCore.Migrations;

namespace BlizzardData.Data.Migrations
{
    public partial class columnNamesChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Criteria",
                newName: "CriteriaName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Achievements",
                newName: "AchievementName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CriteriaName",
                table: "Criteria",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "AchievementName",
                table: "Achievements",
                newName: "Name");
        }
    }
}
