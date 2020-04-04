using Microsoft.EntityFrameworkCore.Migrations;

namespace BlizzardData.Data.Migrations
{
#pragma warning disable IDE1006 // Naming Styles
    public partial class amountto64bitunsigned : Migration
#pragma warning restore IDE1006 // Naming Styles
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Criteria",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "CharacterCriterias",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Amount",
                table: "Criteria",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<long>(
                name: "Amount",
                table: "CharacterCriterias",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
