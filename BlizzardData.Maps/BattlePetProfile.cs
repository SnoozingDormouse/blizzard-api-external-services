using AutoMapper;
using BlizzardAPIExternalMetaDataRetriever.Pets;
using BlizzardData.Domain.Entities;

namespace BlizzardData.Maps
{
    public class DtoMapping : Profile
    {
        public DtoMapping()
        {
            CreateMap<Pet, BattlePet>();
        }
    }
}
