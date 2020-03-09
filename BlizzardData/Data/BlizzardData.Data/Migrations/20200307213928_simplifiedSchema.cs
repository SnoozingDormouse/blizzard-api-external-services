using Microsoft.EntityFrameworkCore.Migrations;

namespace BlizzardData.Data.Migrations
{
    public partial class simplifiedSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Categories_CategoryId",
                table: "Achievements");

            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Criteria_CriteriaId",
                table: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_CategoryId",
                table: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_Achievements_CriteriaId",
                table: "Achievements");

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

            migrationBuilder.AlterColumn<int>(
                name: "CriteriaId",
                table: "Achievements",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Achievements",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "CriteriaId",
                table: "Achievements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Achievements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_CategoryId",
                table: "Achievements",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_CriteriaId",
                table: "Achievements",
                column: "CriteriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Categories_CategoryId",
                table: "Achievements",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Criteria_CriteriaId",
                table: "Achievements",
                column: "CriteriaId",
                principalTable: "Criteria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
