using BlizzardAPIExternalMetaDataRetriever.Pets;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BlizzardAPIExternalMetaDataRetriever.Tests.Pets
{
    public class GetPet
    {
        [Theory]
        [InlineData(39, "Mechanical Squirrel", "Profession", "PROFESSION")]
        [InlineData(849, "Chi-Ji Kite", "Profession", "PROFESSION")]
        public async void Pet_Is_Parsed_Correctly(int id, string name, string sourceName, string sourceType)
        {
            // arrange
            var testBootstrapper = new TestBootstrapper();
            var mockBlizzardApiHelper = new MockBlizzardApiHelper();

            mockBlizzardApiHelper.SetupPetMock(testBootstrapper);
            mockBlizzardApiHelper.SetupBlizzardIMSMock(testBootstrapper);

            var mediator = testBootstrapper.ServiceProvider.GetRequiredService<IMediator>();

            // act
            var result = await mediator.Send(new GetPetRequest { Id = id });

            // assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Name.Should().Be(name);
                result.Source.Name.Should().Be(sourceName);
                result.Source.Type.Should().Be(sourceType);
            }
        }
    }
}
