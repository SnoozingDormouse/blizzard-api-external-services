using System;
using BlizzardAPIExternalMetaDataRetriever.Shared.Models;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData.Model.CharacterSummary
{
    public class CharacterProfileSummary
    {
        [JsonProperty("id")]
        public UInt64 BlizzardId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public TypeName Gender { get; set; }

        [JsonProperty("faction")]
        public TypeName Faction { get; set; }

        [JsonProperty("race")]
        public KeyNameId Race { get; set; }

        [JsonProperty("character_class")]
        public KeyNameId CharacterClass { get; set; }

        [JsonProperty("active_spec")]
        public KeyNameId ActiveSpec { get; set; }

        [JsonProperty("realm")]
        public Realm Realm { get; set; }

        [JsonProperty("guild")]
        public Guild Guild { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("experience")]
        public int Experience { get; set; }

        [JsonProperty("achievement_points")]
        public int AchievementPoints { get; set; }

        [JsonProperty("achievements")]
        public HttpLink Achievements { get; set; }

        [JsonProperty("titles")]
        public HttpLink Titles { get; set; }

        [JsonProperty("pvp_summary")]
        public HttpLink PvpSummary { get; set; }

        [JsonProperty("encounters")]
        public HttpLink Encounters { get; set; }

        [JsonProperty("media")]
        public HttpLink Media { get; set; }

        [JsonProperty("last_login_timestamp")]
        public UInt64 LastLoginTimestamp { get; set; }

        [JsonProperty("average_item_level")]
        public int AverageItemLevel { get; set; }

        [JsonProperty("equipped_item_level")]
        public int EquippedItemLevel { get; set; }

        [JsonProperty("specializations")]
        public HttpLink Specializations { get; set; }

        [JsonProperty("statistics")]
        public HttpLink Statistics { get; set; }

        [JsonProperty("mythic_keystone_profile")]
        public HttpLink MythicKeystoneProfile { get; set; }

        [JsonProperty("equipment")]
        public HttpLink Equipment { get; set; }

        [JsonProperty("appearance")]
        public HttpLink Appearance { get; set; }

        [JsonProperty("collections")]
        public HttpLink Collections { get; set; }

        [JsonProperty("active_title")]
        public ActiveTitle ActiveTitle { get; set; }

        [JsonProperty("reputations")]
        public HttpLink Reputations { get; set; }

        [JsonProperty("quests")]
        public HttpLink Quests { get; set; }

        [JsonProperty("achievements_statistics")]
        public HttpLink AchievementsStatistics { get; set; }

        [JsonProperty("professions")]
        public HttpLink Professions { get; set; }
    }
}
