using AnimeReviewWebApp.Data;
using AnimeReviewWebApp.Interfaces;
using AnimeReviewWebApp.Models;

namespace AnimeReviewWebApp.Repository
{
    public class CountryRepository : ICountryInterface
    {
        private readonly ApplicationDbContext _context;
        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CountryExists(int id)
        {
            return _context.Countries.Any(c => c.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByStudio(int studioId)
        {
            return _context.Studios.Where(s => s.Id == studioId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Studio> GetStudiosFromCountry(int countryId)
        {
            return _context.Studios.Where(s => s.Country.Id == countryId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
