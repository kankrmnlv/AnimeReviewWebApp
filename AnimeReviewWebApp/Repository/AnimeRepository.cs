using AnimeReviewWebApp.Data;
using AnimeReviewWebApp.Interfaces;
using AnimeReviewWebApp.Models;

namespace AnimeReviewWebApp.Repository
{
    public class AnimeRepository : IAnimeInterface
    {
        private readonly ApplicationDbContext _context;
        public AnimeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AnimeExists(int animId)
        {
            return _context.Anime.Any(a => a.Id == animId);
        }

        public ICollection<Anime> GetAnimeList()
        {
            return _context.Anime.OrderBy(a => a.Id).ToList();
        }

        public Anime GetAnime(int id)
        {
            return _context.Anime.Where(a => a.Id == id).FirstOrDefault();
        }

        public Anime GetAnime(string title)
        {
            return _context.Anime.Where(a => a.Title == title).FirstOrDefault();

        }

        public decimal GetAnimeRating(int animId)
        {
            var review = _context.Reviews.Where(a => a.Id == animId);

            if(review.Count() <= 0)
            {
                return 0;
            }

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }
    }
}
