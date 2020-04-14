using System;
using System.Collections.Generic;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData.Model.AccountSummary
{
    public class WowAccount
    {
        public UInt64 id { get; set; }
        public IEnumerable<Character> characters { get; set; }
    }
}