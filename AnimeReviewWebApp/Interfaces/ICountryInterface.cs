using AnimeReviewWebApp.Models;

namespace AnimeReviewWebApp.Interfaces
{
    public interface ICountryInterface
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByStudio(int studioId);
        ICollection<Studio> GetStudiosFromCountry(int countryId);
        bool CountryExists(int id);
        bool CreateCountry(Country country);
        bool Save();
    }
}
