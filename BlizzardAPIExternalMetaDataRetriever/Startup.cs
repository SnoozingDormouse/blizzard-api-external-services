using System;
using BlizzardAPIExternalMetaDataRetriever.Achievements;
using BlizzardAPIExternalMetaDataRetriever.Reputations.RetrieveFromBlizzardAPI;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
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
        readonly string AllowSpecificOrigins = "_allowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200");
                        builder.WithOrigins("http://localhost:50421");
                    });
            });

            services.AddHttpClient();
            services.AddControllers();

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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Blizzard API External Services",
                    Description = "Microservice to collect Blizard World of Warcraft Data from the Blizzard API",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlizzardAPIExternalServices API 0.1.0"));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(AllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
