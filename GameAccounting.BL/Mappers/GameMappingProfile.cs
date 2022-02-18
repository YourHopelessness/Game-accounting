using AutoMapper;
using GameAccounting.BL.Models;
using GameAccounting.DAL.Entity;

namespace GameAccounting.BL.Mappers
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            CreateMap<Genre, GenreDto>();
            CreateMap<Game, GameDto>()
                .ForMember(d => d.Developer, d => d.MapFrom(d => d.Developer.Name));
            CreateMap<Developer, DeveloperDto>();
        }
    }
}
