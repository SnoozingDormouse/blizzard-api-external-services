using AutoMapper;
using BlizzardData.Domain.Entities;

namespace BlizzardAPIExternalMetaDataRetriever.Pets
{
    public class BattlePetProfile : Profile
    {
        public BattlePetProfile()
        {
            CreateMap<Pet, BattlePet>();
        }
    }
}
