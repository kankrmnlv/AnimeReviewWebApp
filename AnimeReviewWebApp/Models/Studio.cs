namespace AnimeReviewWebApp.Models
{
    public class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Country Country { get; set; }
        public ICollection<AnimeStudio> AnimeStudios { get; set; }
    }
}
