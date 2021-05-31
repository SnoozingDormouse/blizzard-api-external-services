using Microsoft.EntityFrameworkCore.Migrations;

namespace BlizzardData.Data.Migrations
{
    public partial class AddPetBattleGameData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "BlizzardId",
                table: "Characters",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "BattlePetAbilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Family = table.Column<int>(type: "int", nullable: false),
                    Rounds = table.Column<int>(type: "int", nullable: false),
                    Cooldown = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattlePetAbilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BattlePetBattlePetAbilities",
                columns: table => new
                {
                    BattlePetId = table.Column<int>(type: "int", nullable: false),
                    BattlePetAbilityId = table.Column<int>(type: "int", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    SlotColumn = table.Column<int>(type: "int", nullable: false),
                    RequiredLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattlePetBattlePetAbilities", x => new { x.BattlePetId, x.BattlePetAbilityId });
                });

            migrationBuilder.CreateTable(
                name: "BattlePets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Family = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCapturable = table.Column<bool>(type: "bit", nullable: false),
                    IsTradable = table.Column<bool>(type: "bit", nullable: false),
                    IsBattlePet = table.Column<bool>(type: "bit", nullable: false),
                    IsAllianceOnly = table.Column<bool>(type: "bit", nullable: false),
                    IsHordeOnly = table.Column<bool>(type: "bit", nullable: false),
                    SourceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattlePets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattlePetAbilities");

            migrationBuilder.DropTable(
                name: "BattlePetBattlePetAbilities");

            migrationBuilder.DropTable(
                name: "BattlePets");

            migrationBuilder.AlterColumn<decimal>(
                name: "BlizzardId",
                table: "Characters",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
