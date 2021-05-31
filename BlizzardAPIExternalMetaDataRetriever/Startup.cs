using System;
using BlizzardAPIExternalMetaDataRetriever.Achievements;
using BlizzardAPIExternalMetaDataRetriever.Reputations.RetrieveFromBlizzardAPI;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlizzardAPIExternalMetaDataRetriever
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public String ConnectionString { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup), typeof(IDataContext));
            services.AddHttpClient();
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Startup));

            services.Configure<BattlenetSettings>(options => Configuration.GetSection("Battlenet").Bind(options));

            services.AddDbContext<IDataContext, DataContext>(
                options => 
                    options
                        .UseLazyLoadingProxies()
                        .UseSqlServer(Configuration.GetSection("Database:ConnectionString").Value), ServiceLifetime.Scoped);

            services.AddTransient<IBlizzardAPIService, BlizzardAPIService>();

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
