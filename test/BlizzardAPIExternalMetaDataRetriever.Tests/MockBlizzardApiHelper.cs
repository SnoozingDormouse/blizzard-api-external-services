using System.IO;
using System.Net.Http;
using BlizzardAPIExternalMetaDataRetriever.Services.Authorization;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;

namespace BlizzardAPIExternalMetaDataRetriever.Tests
{
    public class MockBlizzardApiHelper
    {
        protected readonly string _petJson;
        protected readonly AccessToken _accessToken;

        public MockBlizzardApiHelper()
        {
            _accessToken = new AccessToken("USFtjbQtF9j2GOPGcQP2BtBWnyvfHiem10", 1800);
            _petJson = File.ReadAllText(@".\Pets\Pet_MechanicalSquirrel_39.json");
        }

       
        public void SetupBlizzardIMSMock(TestBootstrapper testBootstrapper)
        {
            var fakeTokenReponse = new TokenResponse
            {
                access_token = _accessToken.Token,
                expires_in = 1800
            };

            var fakeTokenResponseJson = JsonConvert.SerializeObject(fakeTokenReponse);

            var blizzardIMSPath = @"https://fakeblizzardims/oauth/token";
            testBootstrapper.MockHttpMessageHandler
                    .When(HttpMethod.Post, blizzardIMSPath)
                    .Respond("application/json", fakeTokenResponseJson);

        }

        internal void SetupPetAbilitiesIndexMock(TestBootstrapper testBootstrapper)
        {
            var petAbilitiesIndexJson = File.ReadAllText(@".\Pets\PetAbilitiesIndex.json");

            var blizzardPetAbilitiesIndexPath = @"https://fakeblizzardapiendpoint/data/wow/pet-ability/index?namespace=static-eu&locale=en_GB&access_token=" + _accessToken.Token;
            testBootstrapper.MockHttpMessageHandler
                    .When(HttpMethod.Get, blizzardPetAbilitiesIndexPath)
                    .Respond("application/json", petAbilitiesIndexJson);
        }

        internal void SetupPetAbilityMock(TestBootstrapper testBootstrapper)
        {
            var petAbilityJson = File.ReadAllText(@".\Pets\PetAbility.json");

            var blizzardPetAbilityPath = @"https://fakeblizzardapiendpoint/data/wow/pet-ability/110?namespace=static-eu&locale=en_GB&access_token=" + _accessToken.Token;
            testBootstrapper.MockHttpMessageHandler
                    .When(HttpMethod.Get, blizzardPetAbilityPath)
                    .Respond("application/json", petAbilityJson);
        }

        public void SetupPetIndexMock(TestBootstrapper testBootstrapper)
        {
            var petIndexJson = File.ReadAllText(@".\Pets\PetIndex.json");

            var blizzardPetIndexPath = @"https://fakeblizzardapiendpoint/data/wow/pet/index?namespace=static-eu&locale=en_GB&access_token=" + _accessToken.Token;
            testBootstrapper.MockHttpMessageHandler
                    .When(HttpMethod.Get, blizzardPetIndexPath)
                    .Respond("application/json", petIndexJson);
        }

        public void SetupPetMock(TestBootstrapper testBootstrapper)
        {
            var petJson = File.ReadAllText(@".\Pets\Pet_MechanicalSquirrel_39.json");

            var blizzardPetPath = @"https://fakeblizzardapiendpoint/data/wow/pet/39?namespace=static-eu&locale=en_GB&access_token=" + _accessToken.Token;
            testBootstrapper.MockHttpMessageHandler
                    .When(HttpMethod.Get, blizzardPetPath)
                    .Respond("application/json", petJson);
        }

    }
}
