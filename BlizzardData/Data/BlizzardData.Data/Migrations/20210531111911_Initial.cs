using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlizzardData.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AchievementCriterias",
                columns: table => new
                {
                    AchievementId = table.Column<int>(type: "int", nullable: false),
                    CriteriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementCriterias", x => new { x.AchievementId, x.CriteriaId });
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: false),
                    IsAccountWide = table.Column<bool>(type: "bit", nullable: false),
                    RewardDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterAchievements",
                columns: table => new
                {
                    CharacterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AchievementId = table.Column<int>(type: "int", nullable: false),
                    CompletedTimestamp = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterAchievements", x => new { x.CharacterId, x.AchievementId });
                });

            migrationBuilder.CreateTable(
                name: "CharacterCriterias",
                columns: table => new
                {
                    CharacterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CriteriaId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterCriterias", x => new { x.CharacterId, x.CriteriaId });
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    BlizzardId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    UserAccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Realm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faction = table.Column<int>(type: "int", nullable: false),
                    CharacterClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.BlizzardId);
                });

            migrationBuilder.CreateTable(
                name: "Criteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    OperatorType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faction = table.Column<int>(type: "int", nullable: true),
                    AchievementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criteria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CriteriaCriterias",
                columns: table => new
                {
                    CriteriaId = table.Column<int>(type: "int", nullable: false),
                    ChildCriteriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaCriterias", x => new { x.CriteriaId, x.ChildCriteriaId });
                });

            migrationBuilder.CreateTable(
                name: "CriteriaReputations",
                columns: table => new
                {
                    CriteriaId = table.Column<int>(type: "int", nullable: false),
                    ReputationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaReputations", x => new { x.CriteriaId, x.ReputationId });
                });

            migrationBuilder.CreateTable(
                name: "GoalCriterias",
                columns: table => new
                {
                    GoalId = table.Column<int>(type: "int", nullable: false),
                    CriteriaId = table.Column<int>(type: "int", nullable: false),
                    AchievementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalCriterias", x => new { x.GoalId, x.CriteriaId });
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentGoalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReputationFactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanParagon = table.Column<bool>(type: "bit", nullable: true),
                    ReputationTiers = table.Column<int>(type: "int", nullable: false),
                    PlayerFaction = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReputationFactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AchievementCriterias");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "BattlePetAbilities");

            migrationBuilder.DropTable(
                name: "BattlePetBattlePetAbilities");

            migrationBuilder.DropTable(
                name: "BattlePets");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CharacterAchievements");

            migrationBuilder.DropTable(
                name: "CharacterCriterias");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Criteria");

            migrationBuilder.DropTable(
                name: "CriteriaCriterias");

            migrationBuilder.DropTable(
                name: "CriteriaReputations");

            migrationBuilder.DropTable(
                name: "GoalCriterias");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "ReputationFactions");
        }
    }
}
