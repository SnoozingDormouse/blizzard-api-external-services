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
        public class GetPetIndex
        {
            [Fact]
            public async void PetIndex_Is_Parsed_Correctly()
            {
                // arrange
                var testBootstrapper = new TestBootstrapper();
                var mockBlizzardApiHelper = new MockBlizzardApiHelper();

                mockBlizzardApiHelper.SetupPetIndexMock(testBootstrapper);
                mockBlizzardApiHelper.SetupBlizzardIMSMock(testBootstrapper);

                var mediator = testBootstrapper.ServiceProvider.GetRequiredService<IMediator>();

                // act
                var result = await mediator.Send(new GetPetIndexRequest());

                // assert
                using (new AssertionScope())
                {
                    result.Should().NotBeNull();
                    result.Should().NotBeEmpty();
                    result.Count().Should().Be(1418);
                }
            }
        }
    }
}
