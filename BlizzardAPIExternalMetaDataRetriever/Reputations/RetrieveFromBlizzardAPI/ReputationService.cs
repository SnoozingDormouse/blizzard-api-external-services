using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlizzardAPIExternalMetaDataRetriever.Reputations.RetrieveFromBlizzardAPI.IncomingModels;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
using Newtonsoft.Json;
using entities = BlizzardData.Domain.Entities;

namespace BlizzardAPIExternalMetaDataRetriever.Reputations.RetrieveFromBlizzardAPI
{
    public class ReputationService : IReputationService
    {
        private readonly IDataContext _dataContext;
        private readonly IBlizzardAPIService _blizzardAPIService;
        public readonly string _indexApiPath;
        public readonly string _reputationFactionPath;

        public ReputationService(
            IDataContext dataContext,
            IBlizzardAPIService blizzardAPIService)
        {
            _indexApiPath = "/data/wow/reputation-faction/index";
            _reputationFactionPath = "/data/wow/reputation-faction/{0}";
            _dataContext = dataContext;
            _blizzardAPIService = blizzardAPIService;
        }

        public async Task<string> Update(int id)
        {
            string result;
            try
            {
                await GetAndPersistReputationFaction(id);
                result = "Ok";
            }
            catch (Exception ex)
            {
                result = ex.Message + ex.StackTrace;
            }

            return result;
        }

        private async Task GetAndPersistReputationFaction(int id)
        {
            var reputationFaction = await GetReputationFaction(id);
            TransformAndStore(reputationFaction);
        }

        public async Task<string> UpdateAll()
        {
            Stopwatch stopwatch = new Stopwatch();
            int currentReputationId = -1;
            stopwatch.Start();

            var response = await _blizzardAPIService.GetBlizzardAPIResponseAsJsonAsync(_indexApiPath);
            var reputationFactionIds = 
                JsonConvert.DeserializeObject<ReputationFactionIndex>(response)
                .factions.Select(a => a.id);

            reputationFactionIds = reputationFactionIds.Concat(
                JsonConvert.DeserializeObject<ReputationFactionIndex>(response)
                .root_factions.Select(a => a.id));

            foreach (var id in reputationFactionIds)
            {
                try
                {
                    currentReputationId = id;
                    await GetAndPersistReputationFaction(id);
                }
                catch (HttpRequestException)
                {
                    // retry
                    await GetAndPersistReputationFaction(currentReputationId);
                    continue;
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();

                    return
                        String.Format("After {0:hh\\:mm\\:ss} \r\n", stopwatch.Elapsed) +
                        String.Format("An error occurred when retrieving and persisting reputation faction {0}\r\n", currentReputationId)
                        + ex.Message.ToString()
                        + ex.StackTrace.ToString();
                }
            }

            int numReputations = reputationFactionIds.Count();
            stopwatch.Stop();

            return String.Format("{0} reputations downloaded in {1:hh\\:mm\\:ss}", reputationFactionIds.Count(), stopwatch.Elapsed);
            
        }

        internal async Task<ReputationFaction> GetReputationFaction(int id)
        {
            var url = String.Format(_reputationFactionPath, id);
            var response = await _blizzardAPIService.GetBlizzardAPIResponseAsJsonAsync(url).ConfigureAwait(true);

            return 
                JsonConvert.DeserializeObject<ReputationFaction>(response);
        }

        private void TransformAndStore(ReputationFaction reputationFaction)
        {
            TransformAndPersistReputationFaction(reputationFaction);
            _dataContext.SaveChanges();
        }

        private void TransformAndPersistReputationFaction(ReputationFaction reputationFaction)
        {
            var currentReputationFaction = _dataContext.ReputationFactions.Where(rf => rf.Id == reputationFaction.id).SingleOrDefault();

            var playerFaction = (entities::PlayerFaction)Enum.Parse(typeof(entities::PlayerFaction), reputationFaction.player_faction.Name);

            if (currentReputationFaction == null)
            {
                _dataContext.ReputationFactions.Add(
                    new entities::ReputationFaction
                    {
                        Id = reputationFaction.id,
                        Name = reputationFaction.name,
                        Description = reputationFaction.description,
                        CanParagon = reputationFaction.can_paragon,
                        ReputationTiers = reputationFaction.reputation_tiers.id,
                        PlayerFaction = playerFaction
                    });
            }
            else
            {
                currentReputationFaction.Id = reputationFaction.id;
                currentReputationFaction.Name = reputationFaction.name;
                currentReputationFaction.Description = reputationFaction.description;
                currentReputationFaction.CanParagon = reputationFaction.can_paragon;
                currentReputationFaction.ReputationTiers = reputationFaction.reputation_tiers.id;
                currentReputationFaction.PlayerFaction = playerFaction;
            }
        }

        public void RegisterReputationWithCriteria(int criteriaId, int reputationId)
        {
            var currentReputationCriteria =
                            _dataContext
                            .CriteriaReputations
                            .Where(cr => cr.CriteriaId == criteriaId && cr.ReputationId == reputationId)
                            .SingleOrDefault();

            if (currentReputationCriteria == null)
            {
                _dataContext.CriteriaReputations.Add(
                    new entities::CriteriaReputation
                    {
                        CriteriaId = criteriaId,
                        ReputationId = reputationId
                    });
            }
        }
    }
} 
