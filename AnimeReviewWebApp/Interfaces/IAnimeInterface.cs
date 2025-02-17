using AnimeReviewWebApp.Models;

namespace AnimeReviewWebApp.Interfaces
{
    public interface IAnimeInterface
    {
        ICollection<Anime> GetAnimeList();
        Anime GetAnime(int id);
        Anime GetAnime(string title);
        decimal GetAnimeRating(int animId);
        bool AnimeExists(int animId);
        bool CreateAnime(int studioId, int genreId, Anime anime);
        bool UpdateAnime(int studioId, int genreId, Anime anime);
        bool Save();
    }
}
