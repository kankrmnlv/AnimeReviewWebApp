using AnimeReviewWebApp.Data;
using AnimeReviewWebApp.Interfaces;
using AnimeReviewWebApp.Models;

namespace AnimeReviewWebApp.Repository
{
    public class GenreRepository : IGenreInterface
    {
        private readonly ApplicationDbContext _context;
        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool GenreExists(int id)
        {
            return _context.Genres.Any(g => g.Id == id);
        }

        public ICollection<Anime> GetAnimeByGenre(int genreId)
        {
            return _context.AnimeGenres.Where(ag => ag.GenreId == genreId).Select(g => g.Anime).ToList();
        }

        public Genre GetGenre(int id)
        {
            return _context.Genres.Where(g => g.Id == id).FirstOrDefault();
        }

        public ICollection<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}
