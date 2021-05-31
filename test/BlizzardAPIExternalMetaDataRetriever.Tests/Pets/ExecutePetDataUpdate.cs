using BlizzardAPIExternalMetaDataRetriever.Pets;
using BlizzardData.Data;
using BlizzardData.Domain.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace BlizzardAPIExternalMetaDataRetriever.Tests.Pets
{
    public class ExecutePetDataUpdate
    {
        [Fact]
        public async void When_PetDataUpdate_Is_Requested()
        {
            // arrange
            var testBootstrapper = new TestBootstrapper();
            var mockBlizzardApiHelper = new MockBlizzardApiHelper();

            mockBlizzardApiHelper.SetupPetMock(testBootstrapper);
            mockBlizzardApiHelper.SetupPetIndexMock(testBootstrapper);
            mockBlizzardApiHelper.SetupPetAbilityMock(testBootstrapper);
            mockBlizzardApiHelper.SetupPetAbilitiesIndexMock(testBootstrapper);

            mockBlizzardApiHelper.SetupBlizzardIMSMock(testBootstrapper);

            var mediator = testBootstrapper.ServiceProvider.GetRequiredService<IMediator>();
            var database = testBootstrapper.ServiceProvider.GetRequiredService<IDataContext>();

            // act
            var result = await mediator.Send(new PetDataUpdateRequest());
            var storedEntries = database.BattlePets;

            var mechanicalSquirrel = storedEntries.Where(p => p.Id == 39).SingleOrDefault();

            // assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Should().StartWith("3 pets and 4 pet abilities downloaded");

                storedEntries.Should().NotBeNull();
                storedEntries.Count().Should().Be(3);

                mechanicalSquirrel.Should().NotBeNull();
                mechanicalSquirrel.Name.Should().Be("Mechanical Squirrel");
                mechanicalSquirrel.Family.Should().Be(BattlePetFamily.Mechanical);
                mechanicalSquirrel.Description.Should().NotBeNull();
                mechanicalSquirrel.SourceName.Should().Be("Profession");
            }
        }
    }
}
