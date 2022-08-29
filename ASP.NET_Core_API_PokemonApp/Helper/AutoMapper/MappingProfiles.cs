using ASP.NET_Core_API_PokemonApp.DTO;
using ASP.NET_Core_API_PokemonApp.Models;
using AutoMapper;

namespace ASP.NET_Core_API_PokemonApp.Helper.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDTO>()
                .ReverseMap();

            CreateMap<Pokemon, PokemonDetailsDTO>()
                .ForMember(pokemon => pokemon.Owners, opt => opt.MapFrom(model => model.PokemonOwners.Select(x => x.Owner).ToList()))
                .ForMember(pokemon => pokemon.Reviews, opt => opt.MapFrom(model => model.Reviews))
                .ReverseMap();

            CreateMap<Owner, OwnerDTO>()
                .ReverseMap();

            CreateMap<Review, ReviewDTO>()
                .ReverseMap();

            CreateMap<Category, CategoryDTO>()
                .ReverseMap();

            CreateMap<Country, CountryDTO>()
                .ReverseMap();

            CreateMap<Reviewer, ReviewerDTO>()
                .ReverseMap();
        }
    }
}
