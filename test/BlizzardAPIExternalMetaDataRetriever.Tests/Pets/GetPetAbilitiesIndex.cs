using BlizzardAPIExternalMetaDataRetriever.Pets;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace BlizzardAPIExternalMetaDataRetriever.Tests.Pets
{
    public class GetPetAbilitiesIndex
    {
        [Fact]
        public async void PetAbilitiesIndex_Is_Parsed_Correctly()
        {
            // arrange
            var testBootstrapper = new TestBootstrapper();
            var mockBlizzardApiHelper = new MockBlizzardApiHelper();

            mockBlizzardApiHelper.SetupPetAbilitiesIndexMock(testBootstrapper);
            mockBlizzardApiHelper.SetupBlizzardIMSMock(testBootstrapper);

            var mediator = testBootstrapper.ServiceProvider.GetRequiredService<IMediator>();

            // act
            var result = await mediator.Send(new GetPetAbilitiesIndexRequest());

            // assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Should().NotBeEmpty();
                result.Count().Should().Be(4);
            }
        }
    }
}
