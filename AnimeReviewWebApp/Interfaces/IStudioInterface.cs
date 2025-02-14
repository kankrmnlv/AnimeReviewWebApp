using AnimeReviewWebApp.Models;

namespace AnimeReviewWebApp.Interfaces
{
    public interface IStudioInterface
    {
        ICollection<Studio> GetStudios();
        Studio GetStudio(int studioId);
        ICollection<Studio> GetStudioOfAnime(int animId);
        ICollection<Anime> GetAnimeByStudio(int studioId);
        bool StudioExists(int studioId);
    }
}
