namespace AnimeReviewWebApp.Models
{
    public class Anime
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<AnimeStudio> AnimeStudios { get; set; }
        public ICollection<AnimeGenre> AnimeGenres { get; set; }
    }
}
