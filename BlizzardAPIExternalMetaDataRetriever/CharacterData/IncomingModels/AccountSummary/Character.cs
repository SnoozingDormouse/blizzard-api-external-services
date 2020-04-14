using BlizzardAPIExternalMetaDataRetriever.Shared.Models;
using System;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData.Model.AccountSummary
{
    public class Character
    {
        public HttpLink character { get; set; }
        public HttpLink protected_character { get; set; }
        public string name { get; set; }
        public UInt64 id { get; set; }
        public Realm realm { get; set; }
        public KeyNameId playable_class { get; set; }
        public KeyNameId playable_race { get; set; }
        public TypeName gender { get; set; }
        public TypeName faction { get; set; }
        public int level { get; set; }
    }
}
