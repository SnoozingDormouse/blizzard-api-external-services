using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
using BlizzardData.Data.Features.BattlePetFeatures;
using FakeItEasy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;

namespace BlizzardAPIExternalMetaDataRetriever.Tests
{
    public class TestBootstrapper
    {
        public TestBootstrapper()
        {
            var services = new ServiceCollection();
            
            services.AddMediatR(typeof(Startup), typeof(CreateBattlePetCommand));
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IBlizzardAPIService, BlizzardAPIService>();

            SetupFakes();
            InjectFakes(services);

            this.ServiceProvider = services.BuildServiceProvider();
        }
        public MockHttpMessageHandler MockHttpMessageHandler { get; } = new();

        public ServiceProvider ServiceProvider { get; }

        public IHttpClientFactory FakeClientFactory { get; } = A.Fake<IHttpClientFactory>();

        private void SetupFakes()
        {
            A.CallTo(() => FakeClientFactory.CreateClient(A<string>.Ignored)).Returns(new HttpClient(this.MockHttpMessageHandler));
        }

        private void InjectFakes(IServiceCollection services)
        {
            services.AddDbContext<IDataContext, DataContext>
                (options => options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()),
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped);

            IOptions <BattlenetSettings> fakeBattlenetOptions = Options.Create<BattlenetSettings>(new BattlenetSettings
            {
                ClientId = "clientId",
                ClientSecret = "clientSecret",
                BlizzardApiURL = "https://fakeblizzardapiendpoint/{0}?namespace={1}&locale=en_GB&access_token={2}",
                TokenURL = "https://fakeblizzardims/oauth/token"
            });

            services.AddSingleton(FakeClientFactory);

            services.AddTransient<IBlizzardAPIService, BlizzardAPIService>(sp => new BlizzardAPIService(
                fakeBattlenetOptions,
                FakeClientFactory));
        }
    }
}
