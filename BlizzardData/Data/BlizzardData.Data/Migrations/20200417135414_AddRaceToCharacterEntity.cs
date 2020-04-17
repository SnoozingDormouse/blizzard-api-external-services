using Microsoft.EntityFrameworkCore.Migrations;

namespace BlizzardData.Data.Migrations
{
    public partial class AddRaceToCharacterEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Race",
                table: "Characters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Race",
                table: "Characters");
        }
    }
}
