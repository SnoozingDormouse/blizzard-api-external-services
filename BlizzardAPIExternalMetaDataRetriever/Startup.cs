using System;
using BlizzardAPIExternalMetaDataRetriever.Achievements;
using BlizzardAPIExternalMetaDataRetriever.Reputations.RetrieveFromBlizzardAPI;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BlizzardAPIExternalMetaDataRetriever
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration["Database:ConnectionString"] ?? throw new ArgumentNullException("Database:ConnectionString");
        }

        public IConfiguration Configuration { get; }
        public String ConnectionString { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddDbContext<DataContext>(
                options => 
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(ConnectionString), ServiceLifetime.Scoped);

            services.AddScoped<IBlizzardAPIService, BlizzardAPIService>();

            services.AddScoped<IAchievementService>(s => new AchievementService(
                s.GetService<DataContext>(),
                s.GetService<IBlizzardAPIService>()));

            services.AddScoped<IReputationService>(s => new ReputationService(
                s.GetService<DataContext>(),
                s.GetService<IBlizzardAPIService>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "External Blizzard API Connector Service");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
