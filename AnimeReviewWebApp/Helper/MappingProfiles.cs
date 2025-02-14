using AnimeReviewWebApp.Dto;
using AnimeReviewWebApp.Models;
using AutoMapper;

namespace AnimeReviewWebApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Anime, AnimeDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Studio, StudioDto>();
            CreateMap<Review, ReviewDto>();
        }
    }
}
