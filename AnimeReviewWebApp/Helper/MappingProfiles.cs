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
            CreateMap<AnimeDto, Anime>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Studio, StudioDto>();
            CreateMap<StudioDto, Studio>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
}
