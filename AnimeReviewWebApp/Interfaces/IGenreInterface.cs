using AnimeReviewWebApp.Models;

namespace AnimeReviewWebApp.Interfaces
{
    public interface IGenreInterface
    {
        ICollection<Genre> GetGenres();
        Genre GetGenre(int id);
        ICollection<Anime> GetAnimeByGenre(int genreId);
        bool GenreExists(int id);
    }
}
