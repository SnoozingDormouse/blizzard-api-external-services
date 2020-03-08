using Microsoft.EntityFrameworkCore.Migrations;

namespace BlizzardData.Data.Migrations
{
    public partial class NullableIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CriteriaId",
                table: "Achievements",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Achievements",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CriteriaId",
                table: "Achievements",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Achievements",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
