﻿using System;
using System.Collections.Generic;
using BlizzardAPIExternalMetaDataRetriever.Shared.Models;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
    public class ChildCriteria
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("amount")]
        public UInt64 Amount { get; set; }

        [JsonProperty("operator")]
        public TypeName MustComplete { get; set; }

        [JsonProperty("faction")]
        public TypeName Faction { get; set; }

        [JsonProperty("achievement")]
        public KeyNameId Achievement { get; set; }

        [JsonProperty("child_criteria")]
        public IEnumerable<ChildCriteria> SubCriteria { get; set; }
    }
}