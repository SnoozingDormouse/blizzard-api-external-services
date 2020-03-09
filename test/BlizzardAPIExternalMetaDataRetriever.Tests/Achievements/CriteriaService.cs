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

        [Fact]
        public void GivenResponse_WhenSingleAchievementAndCriteriaHasChild_ResultsAreAsExpected()
        {
            var childCriteria =
                new incoming::Criteria
                {
                    id = 1001,
                    amount = 210,
                    is_completed = false,
                    child_criteria = null
                };

            var topLevelCriteria = 
                new incoming::Criteria
                {
                    id = 1000,
                    amount = 21,
                    is_completed = true,
                    child_criteria = new List<incoming::Criteria> { childCriteria }
                };

            // arrange
            var achievement = new incoming::Achievement
            {
                id = 100,
                criteria = topLevelCriteria
            };

            var achievements = new List<incoming::Achievement>() { achievement };
            string response = WrapJsonSerialization(achievements);

            //act

            ReadOnlyCollection<models::Criteria> actual = _sut.GetAllCriteria(response);

            // assert
            using (var scope = new AssertionScope())
            {
                actual.Count().Should().Be(2);
                models::Criteria criteria;

                criteria = actual.Where(c => c.Id == 1000).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 21;
                criteria.Description = null;
                criteria.IsCompleted = true;
                criteria.ParentId = null;

                criteria = actual.Where(c => c.Id == 1001).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 210;
                criteria.Description = null;
                criteria.IsCompleted = false;
                criteria.ParentId = 1000;
            }
        }

        [Fact]
        public void GivenResponse_WhenSingleAchievementAndCriteriaHasMultipleChildren_ResultsAreAsExpected()
        {
            var childCriteria1 =
                new incoming::Criteria
                {
                    id = 1001,
                    amount = 210,
                    is_completed = false,
                    child_criteria = null
                };

            var childCriteria2 =
                new incoming::Criteria
                {
                    id = 1002,
                    amount = 0,
                    is_completed = true,
                    child_criteria = null
                };

            var topLevelCriteria =
                new incoming::Criteria
                {
                    id = 1000,
                    amount = 21,
                    is_completed = true,
                    child_criteria = new List<incoming::Criteria> { childCriteria1, childCriteria2 }
                };

            // arrange
            var achievement = new incoming::Achievement
            {
                id = 100,
                criteria = topLevelCriteria
            };

            var achievements = new List<incoming::Achievement>() { achievement };
            string response = WrapJsonSerialization(achievements);

            //act

            ReadOnlyCollection<models::Criteria> actual = _sut.GetAllCriteria(response);

            // assert
            using (var scope = new AssertionScope())
            {
                actual.Count().Should().Be(3);
                models::Criteria criteria;

                criteria = actual.Where(c => c.Id == 1000).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 21;
                criteria.Description = null;
                criteria.IsCompleted = true;
                criteria.ParentId = null;

                criteria = actual.Where(c => c.Id == 1001).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 210;
                criteria.Description = null;
                criteria.IsCompleted = false;
                criteria.ParentId = 1000;

                criteria = actual.Where(c => c.Id == 1002).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 0;
                criteria.Description = null;
                criteria.IsCompleted = true;
                criteria.ParentId = 1000;
            }
        }

        [Fact]
        public void GivenResponse_WhenSingleAchievementAndCriteriaHas2LevelsOfChildren_ResultsAreAsExpected()
        {
            var childCriteria1 =
                new incoming::Criteria
                {
                    id = 1001,
                    amount = 210,
                    is_completed = false,
                    child_criteria = null
                };

            var childCriteria2 =
                new incoming::Criteria
                {
                    id = 1002,
                    amount = 0,
                    is_completed = true,
                    child_criteria = new List<incoming::Criteria> { childCriteria1 }
                };

            var topLevelCriteria =
                new incoming::Criteria
                {
                    id = 1000,
                    amount = 21,
                    is_completed = true,
                    child_criteria = new List<incoming::Criteria> { childCriteria2 }
                };

            // arrange
            var achievement = new incoming::Achievement
            {
                id = 100,
                criteria = topLevelCriteria
            };

            var achievements = new List<incoming::Achievement>() { achievement };
            string response = WrapJsonSerialization(achievements);

            //act

            ReadOnlyCollection<models::Criteria> actual = _sut.GetAllCriteria(response);

            // assert
            using (var scope = new AssertionScope())
            {
                actual.Count().Should().Be(3);
                models::Criteria criteria;

                criteria = actual.Where(c => c.Id == 1000).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 21;
                criteria.Description = null;
                criteria.IsCompleted = true;
                criteria.ParentId = null;

                criteria = actual.Where(c => c.Id == 1001).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 210;
                criteria.Description = null;
                criteria.IsCompleted = false;
                criteria.ParentId = 1000;

                criteria = actual.Where(c => c.Id == 1002).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 0;
                criteria.Description = null;
                criteria.IsCompleted = true;
                criteria.ParentId = 1000;
            }
        }

        [Fact]
        public void GivenResponse_WhenSingleAchievementAndCriteriaHas3LevelsOfChildren_ResultsAreAsExpected()
        {
            var childCriteria3 =
                new incoming::Criteria
                {
                    id = 1003,
                    amount = 0,
                    is_completed = false,
                    child_criteria = null
                };

            var childCriteria2 =
                new incoming::Criteria
                {
                    id = 1002,
                    amount = 10,
                    is_completed = true,
                    child_criteria = new List<incoming::Criteria> { childCriteria3 }
                };

            var childCriteria1 =
                new incoming::Criteria
                {
                    id = 1001,
                    amount = 0,
                    is_completed = true,
                    child_criteria = new List<incoming::Criteria> { childCriteria2 }
                };

            var topLevelCriteria =
                new incoming::Criteria
                {
                    id = 1000,
                    amount = 21,
                    is_completed = true,
                    child_criteria = new List<incoming::Criteria> { childCriteria1 }
                };

            // arrange
            var achievement = new incoming::Achievement
            {
                id = 100,
                criteria = topLevelCriteria
            };

            var achievements = new List<incoming::Achievement>() { achievement };
            string response = WrapJsonSerialization(achievements);

            //act

            ReadOnlyCollection<models::Criteria> actual = _sut.GetAllCriteria(response);

            // assert
            using (var scope = new AssertionScope())
            {
                actual.Count().Should().Be(4);
                models::Criteria criteria;

                criteria = actual.Where(c => c.Id == 1000).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 21;
                criteria.Description = null;
                criteria.IsCompleted = true;
                criteria.ParentId = null;

                criteria = actual.Where(c => c.Id == 1001).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 0;
                criteria.Description = null;
                criteria.IsCompleted = true;
                criteria.ParentId = 1000;

                criteria = actual.Where(c => c.Id == 1002).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 10;
                criteria.Description = null;
                criteria.IsCompleted = true;
                criteria.ParentId = 1001;

                criteria = actual.Where(c => c.Id == 1003).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 0;
                criteria.Description = null;
                criteria.IsCompleted = false;
                criteria.ParentId = 1002;
            }
        }

        [Fact]
        public void GivenResponse_WhenSingleAchievementAndCriteriaHas4LevelsOfChildren_ResultsAreAsExpected()
        {
            var childCriteria4 =
                new incoming::Criteria
                {
                    id = 1004,
                    amount = 50,
                    is_completed = true,
                    child_criteria = null
                };

            var childCriteria3 =
                new incoming::Criteria
                {
                    id = 1003,
                    amount = 0,
                    is_completed = false,
                    child_criteria = new List<incoming::Criteria> { childCriteria4 }
                };

            var childCriteria2 =
                new incoming::Criteria
                {
                    id = 1002,
                    amount = 10,
                    is_completed = true,
                    child_criteria = new List<incoming::Criteria> { childCriteria3 }
                };

            var childCriteria1 =
                new incoming::Criteria
                {
                    id = 1001,
                    amount = 0,
                    is_completed = true,
                    child_criteria = new List<incoming::Criteria> { childCriteria2 }
                };

            var topLevelCriteria =
                new incoming::Criteria
                {
                    id = 1000,
                    amount = 21,
                    is_completed = true,
                    child_criteria = new List<incoming::Criteria> { childCriteria1 }
                };

            // arrange
            var achievement = new incoming::Achievement
            {
                id = 100,
                criteria = topLevelCriteria
            };

            var achievements = new List<incoming::Achievement>() { achievement };
            string response = WrapJsonSerialization(achievements);

            //act

            ReadOnlyCollection<models::Criteria> actual = _sut.GetAllCriteria(response);

            // assert
            using (var scope = new AssertionScope())
            {
                actual.Count().Should().Be(5);
                models::Criteria criteria;

                criteria = actual.Where(c => c.Id == 1000).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 21;
                criteria.Description = null;
                criteria.IsCompleted = true;
                criteria.ParentId = null;

                criteria = actual.Where(c => c.Id == 1001).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 0;
                criteria.Description = null;
                criteria.IsCompleted = true;
                criteria.ParentId = 1000;

                criteria = actual.Where(c => c.Id == 1002).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 10;
                criteria.Description = null;
                criteria.IsCompleted = true;
                criteria.ParentId = 1001;

                criteria = actual.Where(c => c.Id == 1003).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 0;
                criteria.Description = null;
                criteria.IsCompleted = false;
                criteria.ParentId = 1002;

                criteria = actual.Where(c => c.Id == 1004).Single();
                criteria.AchievementId = 100;
                criteria.Amount = 50;
                criteria.Description = null;
                criteria.IsCompleted = true;
                criteria.ParentId = 1003;
            }
        }

        private static string WrapJsonSerialization(List<Achievement> achievements)
        {
            return "{ \"achievements\": " + JsonConvert.SerializeObject(achievements) + "}";
        }
    }
}
