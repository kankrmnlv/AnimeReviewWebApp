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
            var review = _context.Reviews.Where(a => a.Anime.Id == animId);

            if(review.Count() <= 0)
            {
                return 0;
            }

            return (decimal)review.Sum(r => r.Rating) / review.Count();
        }

        public bool CreateAnime(int studioId, int genreId, Anime anime)
        {
            var animeStudioEntity = _context.Studios.Where(a => a.Id == studioId).FirstOrDefault();
            var genre = _context.Genres.Where(a => a.Id == genreId).FirstOrDefault();

            var animeStudio = new AnimeStudio
            {
                Studio = animeStudioEntity,
                Anime = anime,
            };

            _context.Add(animeStudio);

            var animeGenre = new AnimeGenre
            {
                Genre = genre,
                Anime = anime
            };

            _context.Add(animeGenre);

            _context.Add(anime);

            return Save();
        }
        public bool UpdateAnime(int studioId, int genreId, Anime anime)
        {
            var animeStudioEntity = _context.Studios.Where(a => a.Id == studioId).FirstOrDefault();
            var genre = _context.Genres.Where(a => a.Id == genreId).FirstOrDefault();

            var animeStudio = new AnimeStudio
            {
                Studio = animeStudioEntity,
                Anime = anime,
            };

            _context.Update(animeStudio);

            var animeGenre = new AnimeGenre
            {
                Genre = genre,
                Anime = anime
            };

            _context.Update(animeGenre);

            _context.Update(anime);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
