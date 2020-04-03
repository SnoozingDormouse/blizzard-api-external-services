using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData
{
    public class CharacterService : ICharacterService
    {
        public void UpdateCharacterData(string region, string realm, string name)
        {
            // 1. update character information (fetch from blizzard api)
            //    so we have realm, name, LEVEL, BLIZZARDID, lastupdated date
            throw new NotImplementedException();
        }
    }
}
