using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels;
using BlizzardData.Data;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xunit;
using incoming = BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels;
using models = BlizzardData.Models;

namespace BlizzardAPIExternalMetaDataRetriever.Tests
{
    public class CriteriaService
    {
        Achievements.CriteriaService _sut;

        public CriteriaService()
        {
            var mockAchievementContext = new Mock<IAchievementContext>();
            var mockClientFactory = new Mock<IHttpClientFactory>();
            var aClientId = "1";
            var aClientSecret = "secret";

            _sut = new Achievements.CriteriaService(
                    mockAchievementContext.Object,
                    mockClientFactory.Object,
                    aClientId,
                    aClientSecret);
        }

        [Fact]
        public void GivenResponse_WhenSingleAchievement_ResultsAreAsExpected()
        {
            // arrange
            var achievement = new incoming::Achievement
            {
                id = 100,
                criteria = new incoming::Criteria
                {
                    id = 1000,
                    amount = 21,
                    is_completed = true,
                    child_criteria = null
                }
            };

            var achievements = new List<incoming::Achievement>() { achievement };
            string response = WrapJsonSerialization(achievements);

            //act

            ReadOnlyCollection<models::Criteria> actual = _sut.GetAllCriteria(response);

            // assert
            using (var scope = new AssertionScope())
            {
                actual.Count().Should().Be(1);
                models::Criteria criteria = actual.First();
                criteria.AchievementId = 100;
                criteria.Amount = 21;
                criteria.Description = null;
                criteria.Id = 1000;
                criteria.IsCompleted = true;
                criteria.ParentId = null;
            }
        }

        [Fact]
        public void GivenResponse_WhenMultipleAchievements_ResultsAreAsExpected()
        {
            // arrange
            var achievement1 = new incoming::Achievement
            {
                id = 100,
                criteria = new incoming::Criteria
                {
                    id = 1000,
                    amount = 21,
                    is_completed = true,
                    child_criteria = null
                }
            };

            var achievement2 = new incoming::Achievement
            {
                id = 101,
                criteria = new incoming::Criteria
                {
                    id = 1001,
                    amount = 2,
                    is_completed = false,
                    child_criteria = null
                }
            };

            var achievements = new List<incoming::Achievement>() { achievement1, achievement2 };
            string response = WrapJsonSerialization(achievements);

            //act

            ReadOnlyCollection<models::Criteria> actual = _sut.GetAllCriteria(response);

            // assert
            using (var scope = new AssertionScope())
            {
                actual.Count().Should().Be(2);
                models::Criteria criteria = actual.First();
                criteria.AchievementId = 100;
                criteria.Amount = 21;
                criteria.Description = null;
                criteria.Id = 1000;
                criteria.IsCompleted = true;
                criteria.ParentId = null;

                criteria = actual.TakeLast(1).Single();
                criteria.AchievementId = 101;
                criteria.Amount = 2;
                criteria.Description = null;
                criteria.Id = 1001;
                criteria.IsCompleted = false;
                criteria.ParentId = null;
            }
        }

        private static string WrapJsonSerialization(List<Achievement> achievements)
        {
            return "{ \"achievements\": " + JsonConvert.SerializeObject(achievements) + "}";
        }
    }
}
