using BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Xunit;
using entities = BlizzardData.Domain.Entities;

namespace BlizzardAPIExternalMetaDataRetriever.Tests
{
    public class AchievementService
    {
        public AchievementService()
        {
        }

        [Theory]
        [InlineData(".\\IncomingJson\\Achievement12989.json")]
        public void GivenExampleJson_Is_Parsed_Correctly_12989(string filename)
        {
            var json = File.ReadAllText(filename);
            var result = JsonConvert.DeserializeObject<Achievement>(json);

            using (new AssertionScope())
            {
                result.Id.Should().Be(12989);
                result.Category.Name.Should().Be("Battle for Azeroth");
                result.Criteria.ChildCriteria.Count().Should().Be(5);

                var diplomatDescription = "Azerothian Diplomat";
                var childAchievement = 
                    result
                    .Criteria
                    .ChildCriteria
                    .Where(cc => cc.Description == diplomatDescription)
                    .SingleOrDefault()?
                    .Achievement;

                childAchievement.Id.Should().Be(12947);
                childAchievement.Name.Should().Be(diplomatDescription);
            }
        }

        [Theory]
        [InlineData(".\\IncomingJson\\Achievement556.json")]
        public void GivenExampleJson_Is_Parsed_Correctly_556(string filename)
        {
            var json = File.ReadAllText(filename);
            var result = JsonConvert.DeserializeObject<Achievement>(json);

            using (new AssertionScope())
            {
                result.Id.Should().Be(556);
                result.Criteria.Should().BeNull();
            }
        }

        [Theory]
        [InlineData(".\\IncomingJson\\Achievement12989.json")]
        public void BattleforAzerothPathfinderPartOne_IsAbleToRecordBothHordeAndAllianceOptions(string filename)
        {
            var json = File.ReadAllText(filename);
            var achievement = JsonConvert.DeserializeObject<Achievement>(json);

            var mockBlizzardAPIService = new Mock<BlizzardAPIService>();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            using (var dbContextWrite = new DataContext(options))
            {
                var sut = new Achievements.AchievementService(
                    dbContextWrite,
                    mockBlizzardAPIService.Object);

                sut.TransformAndStore(achievement);
            }

            using (var dbContextRead = new DataContext(options))
            using (new AssertionScope())
            {
                var storedEntry = dbContextRead.Achievements.Where(a => a.Id == 12989).SingleOrDefault();
                
                // Achievement is entered to dbo.Achievement correctly
                storedEntry.Should().NotBeNull();
                storedEntry.Name.Should().Be("Battle for Azeroth Pathfinder, Part One");
                storedEntry.Description.Should().Be("Complete the Kul Tiras and Zandalar achievements listed below.");
                storedEntry.Points.Should().Be(25);
                storedEntry.IsAccountWide.Should().Be(true);
                storedEntry.RewardDescription.Should().Be("Reward: Increased Mount Speed in Kul Tiras and Zandalar");

                // Category is in dbo.Categories
                var storedCategory = dbContextRead.Categories.Where(c => c.Id == achievement.Category.Id).SingleOrDefault();
                storedCategory.Name.Should().Be("Battle for Azeroth");
                storedCategory.Id.Should().Be(15298);

                // Criteria is stored in dbo.Criteria and a link is created in dbo.AchievementCriteria
                var storedCriteria = dbContextRead.Criteria.Where(c => c.Id == 68499).SingleOrDefault();
                storedCriteria.Description.Should().Be(null);
                storedCriteria.Amount.Should().Be(0);
                storedCriteria.OperatorType.Should().Be("AND");
                storedCriteria.OperatorName.Should().Be("Complete All");

                var storedAchievementCriteria = dbContextRead.AchievementCriterias.Where(ac => ac.CriteriaId == 68499).SingleOrDefault();
                storedAchievementCriteria.AchievementId.Should().Be(12989);

                // this parent criteria should not be stored in dbo.CriteriaCriteria as a child criteria
                var storedCriteriaChildCriteria = dbContextRead.CriteriaCriterias.Where(c => c.ChildCriteriaId == 68499).ToList();
                storedCriteriaChildCriteria.Count.Should().Be(0);

                // but it should appear in dbo.CriteriaCriterias as a parent criteria
                var storedCriteriaCriteria = dbContextRead.CriteriaCriterias.Where(c => c.CriteriaId == 68499).ToList();
                storedCriteriaCriteria.Should().HaveCountGreaterThan(0);

                // storing the child criteria
                var battleForAzerothExplorerChildCriteria = dbContextRead.Criteria.Where(c => c.Id == 68500).SingleOrDefault();
                battleForAzerothExplorerChildCriteria.Description.Should().Be("Battle for Azeroth Explorer");
                battleForAzerothExplorerChildCriteria.Amount.Should().Be(0);
                battleForAzerothExplorerChildCriteria.AchievementId.Should().Be(12988);

                // a child-criteria of PathFinder one which has it's own child criteria
                var completeZoneQuestsCriteria = dbContextRead.Criteria.Where(c => c.Id == 68504).SingleOrDefault();
                completeZoneQuestsCriteria.Description.Should().Be("Complete Zone Questlines");
                completeZoneQuestsCriteria.Amount.Should().Be(1);
                completeZoneQuestsCriteria.OperatorType.Should().Be("COMPLETE_AT_LEAST");
                completeZoneQuestsCriteria.OperatorName.Should().Be("Complete At Least");

                var completeZoneQuestsAchievementCriteria =
                    dbContextRead.AchievementCriterias.Where(ac => ac.CriteriaId == 68500).SingleOrDefault();
                completeZoneQuestsAchievementCriteria.AchievementId.Should().Be(12989);

                var completeZoneQuestsCriteriaChildCriteria = dbContextRead.CriteriaCriterias.Where(cc => cc.CriteriaId == 68504).ToList();
                completeZoneQuestsCriteriaChildCriteria.Count.Should().Be(2);
                var completeZoneQuestsChildCriteria = completeZoneQuestsCriteriaChildCriteria.Select(c => c.ChildCriteriaId);
                completeZoneQuestsChildCriteria.Should().Contain(68501);
                completeZoneQuestsChildCriteria.Should().Contain(68502);

                var allianceCompleteZoneQuest = dbContextRead.Criteria.Where(c => c.Id == 68501).SingleOrDefault();
                allianceCompleteZoneQuest.Description.Should().Be("Loremaster of Kul Tiras");
                allianceCompleteZoneQuest.Amount.Should().Be(0);
                allianceCompleteZoneQuest.Faction.Should().Be(entities::PlayerFaction.Alliance);
                allianceCompleteZoneQuest.AchievementId.Should().Be(12593);
                
                var hordeCompleteZoneQuests = dbContextRead.Criteria.Where(c => c.Id == 68502).SingleOrDefault();
                hordeCompleteZoneQuests.Description.Should().Be("Zandalar Forever!");
                hordeCompleteZoneQuests.Amount.Should().Be(0);
                hordeCompleteZoneQuests.Faction.Should().Be(entities::PlayerFaction.Horde);
                hordeCompleteZoneQuests.AchievementId.Should().Be(12479);
            }
        }
    }
}
