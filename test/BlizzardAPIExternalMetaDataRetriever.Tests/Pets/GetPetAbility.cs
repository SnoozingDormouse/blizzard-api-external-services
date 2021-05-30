using BlizzardAPIExternalMetaDataRetriever.Pets;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace BlizzardAPIExternalMetaDataRetriever.Tests.Pets
{
    public partial class When_Blizzard_Pet_Data_Is_Requested
    {
        public class GetPetAbility
        {
            [Fact]
            public async void PetAbility_Is_Parsed_Correctly()
            {
                // arrange
                var testBootstrapper = new TestBootstrapper();
                var mockBlizzardApiHelper = new MockBlizzardApiHelper();

                mockBlizzardApiHelper.SetupPetAbilityMock(testBootstrapper);
                mockBlizzardApiHelper.SetupBlizzardIMSMock(testBootstrapper);

                var mediator = testBootstrapper.ServiceProvider.GetRequiredService<IMediator>();

                // act
                var result = await mediator.Send(new GetPetAbilityRequest { Id = 110 });

                // assert
                using (new AssertionScope())
                {
                    result.Should().NotBeNull();
                    result.Name.Should().Be("Bite");
                    result.PetAbilityType.Should().Be("Beast");
                    result.PetAbilityTypeId.Should().Be(7);
                    result.Rounds.Should().Be(1);
                }
            }
        }
    }
}
