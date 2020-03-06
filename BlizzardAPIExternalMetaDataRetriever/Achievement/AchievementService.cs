using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using model = BlizzardAPIModels.Achievement;

namespace BlizzardAPIExternalMetaDataRetriever.Achievement
{
    public class AchievementService : BlizzardAPIService, IAchievementService
    {
        public string _apiPath;

        public AchievementService(IHttpClientFactory clientFactory, string clientId, string clientSecret)
            : base(clientFactory, clientId, clientSecret)
        {
            _apiPath = "data/wow/achievement/index";
        }

        public bool UpdateAllAchievements()
        {
            string response = GetBlizzardAPIResponseAsJson(_apiPath);
            var achievementIds = JsonConvert.DeserializeObject<model::AchievementWrapper>(response).achievements.Select(a => a.id);
            
            //var entities = entities in the database - use EF - single dbcontext for achievements

            return (String.IsNullOrEmpty(response)) ? false : true;
        }
    }
} 
