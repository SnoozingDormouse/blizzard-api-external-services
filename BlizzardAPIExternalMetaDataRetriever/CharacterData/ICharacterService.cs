using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData
{
    public interface ICharacterService
    {
        void UpdateCharacterData(string region, string realm, string name);
    }
}
