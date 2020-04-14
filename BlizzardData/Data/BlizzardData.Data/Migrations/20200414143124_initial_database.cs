using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlizzardData.Data.Migrations
{
    public partial class initial_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AchievementCriterias",
                columns: table => new
                {
                    AchievementId = table.Column<int>(nullable: false),
                    CriteriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementCriterias", x => new { x.AchievementId, x.CriteriaId });
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Points = table.Column<int>(nullable: false),
                    IsAccountWide = table.Column<bool>(nullable: false),
                    RewardDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterAchievements",
                columns: table => new
                {
                    CharacterId = table.Column<decimal>(nullable: false),
                    AchievementId = table.Column<int>(nullable: false),
                    CompletedTimestamp = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterAchievements", x => new { x.CharacterId, x.AchievementId });
                });

            migrationBuilder.CreateTable(
                name: "CharacterCriterias",
                columns: table => new
                {
                    CharacterId = table.Column<decimal>(nullable: false),
                    CriteriaId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterCriterias", x => new { x.CharacterId, x.CriteriaId });
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    BlizzardId = table.Column<decimal>(nullable: false),
                    UserAccountId = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Realm = table.Column<string>(nullable: true),
                    Faction = table.Column<int>(nullable: false),
                    CharacterClass = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    LastUpdatedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.BlizzardId);
                });

            migrationBuilder.CreateTable(
                name: "Criteria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    OperatorType = table.Column<string>(nullable: true),
                    OperatorName = table.Column<string>(nullable: true),
                    Faction = table.Column<int>(nullable: true),
                    AchievementId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criteria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CriteriaCriterias",
                columns: table => new
                {
                    CriteriaId = table.Column<int>(nullable: false),
                    ChildCriteriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaCriterias", x => new { x.CriteriaId, x.ChildCriteriaId });
                });

            migrationBuilder.CreateTable(
                name: "CriteriaReputations",
                columns: table => new
                {
                    CriteriaId = table.Column<int>(nullable: false),
                    ReputationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriteriaReputations", x => new { x.CriteriaId, x.ReputationId });
                });

            migrationBuilder.CreateTable(
                name: "GoalCriterias",
                columns: table => new
                {
                    GoalId = table.Column<int>(nullable: false),
                    CriteriaId = table.Column<int>(nullable: false),
                    AchievementId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalCriterias", x => new { x.GoalId, x.CriteriaId });
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ParentGoalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReputationFactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CanParagon = table.Column<bool>(nullable: true),
                    ReputationTiers = table.Column<int>(nullable: false),
                    PlayerFaction = table.Column<string>(nullable: false)
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
