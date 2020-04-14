using BlizzardAPIExternalMetaDataRetriever.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData.Model.AccountSummary
{
    public class AccountProfileSummary
    {
        /*
         * Because this endpoint provides data about the current logged-in user's
         * World of Warcraft account, it requires an access token with the 
         * wow.profile scope acquired via the Authorization Code Flow.
         */

        public UInt64 id { get; set; }
        public IEnumerable<WowAccount> wow_accounts { get; set; }
        public HttpLink collections { get; set; }
    }
}
