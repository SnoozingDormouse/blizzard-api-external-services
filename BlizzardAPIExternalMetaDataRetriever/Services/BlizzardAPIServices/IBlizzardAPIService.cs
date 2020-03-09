using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices
{
    public interface IBlizzardAPIService
    {
        string GetBlizzardGameDataAPIResponseAsJson(string apiPath);
        string GetBlizzardDefaultProfileAPIResponseAsJson(string apiPath);
        string GetBlizzardProfileAPIResponseAsJson(string apiPath, string realm, string character);
    }
}
