﻿using BlizzardAPIExternalMetaDataRetriever.Pets;
using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace BlizzardAPIExternalMetaDataRetriever.Tests.Pets
{
    public class GetPetAbility
    {
        [Theory]
        [InlineData(110, "Bite", "Beast", 7, 1, 0)]
        [InlineData(1583, "Warning!", "Mechanical", 9, 3, 8)]
        public async void PetAbility_Is_Parsed_Correctly(int id, string name, string petAbilityType, int petAbilityId, int rounds, int cooldown)
        {
            // arrange
            var testBootstrapper = new TestBootstrapper();
            var mockBlizzardApiHelper = new MockBlizzardApiHelper();

            mockBlizzardApiHelper.SetupPetAbilityMock(testBootstrapper);
            mockBlizzardApiHelper.SetupBlizzardIMSMock(testBootstrapper);

            var mediator = testBootstrapper.ServiceProvider.GetRequiredService<IMediator>();

            // act
            var result = await mediator.Send(new GetPetAbilityRequest { Id = id });

            // assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Name.Should().Be(name);
                result.PetAbilityType.Should().Be(petAbilityType);
                result.PetAbilityTypeId.Should().Be(petAbilityId);
                result.Rounds.Should().Be(rounds);
                result.Cooldown.Should().Be(cooldown);
            }
        }
    }
}
