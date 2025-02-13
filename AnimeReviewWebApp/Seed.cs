using AnimeReviewWebApp.Data;
using AnimeReviewWebApp.Models;

namespace AnimeReviewWebApp
{
    public class Seed
    {
        private readonly ApplicationDbContext _context;
        public Seed(ApplicationDbContext context)
        {
            _context = context;
        }
        public void SeedDataContext()
        {
            if (!_context.AnimeStudios.Any())
            {
                var animeStudios = new List<AnimeStudio>()
                {
                    new AnimeStudio()
                    {
                        Anime = new Anime()
                        {
                            Title = "Attack on Titan",
                            ReleaseDate = new DateTime(2013, 4, 7),
                            AnimeGenres = new List<AnimeGenre>()
                            {
                                new AnimeGenre { Genre = new Genre() { Name = "Shonen" } }
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title = "Attack on Titan", Text = "Attack on Titan is a legendary anime, must watch!", Rating = 5,
                                    Reviewer = new Reviewer() { FirstName = "Kaniet", LastName = "Kurmanaliev" } },
                                new Review { Title = "The story", Text = "Attack on Titan is in my top 3 anime. The story is just so good", Rating = 5,
                                    Reviewer = new Reviewer() { FirstName = "Nabi", LastName = "Karypbekov" } },
                                new Review { Title = "Mikasa", Text = "This anime is so unpredictable and breathtaking. I love Mikasa!", Rating = 3,
                                    Reviewer = new Reviewer() { FirstName = "Danil", LastName = "Nikiforov" } },
                            }
                        },
                        Studio = new Studio()
                        {
                            Name = "MAPPA",
                            Description = "MAPPA is a Japanese animation studio headquartered in Nakano, Tokyo, founded in 2011",
                            Country = new Country()
                            {
                                Name = "Japan"
                            }
                        }
                    },
                    new AnimeStudio()
                    {
                        Anime = new Anime()
                        {
                            Title = "One Piece",
                            ReleaseDate = new DateTime(1999,10,20),
                            AnimeGenres = new List<AnimeGenre>()
                            {
                                new AnimeGenre { Genre = new Genre() { Name = "Adventure"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Childish",Text = "I could not keep watching this anime, it is too childing for me!", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Aigul", LastName = "Mukhataeva" } },
                                new Review { Title="Very cool",Text = "I just love following along this adventure!", Rating = 4,
                                Reviewer = new Reviewer(){ FirstName = "Vitalii", LastName = "Fuks" } },
                                new Review { Title="Mid",Text = "It is not bad, but neither not the best. Love pirates though!", Rating = 3,
                                Reviewer = new Reviewer(){ FirstName = "Anastasia", LastName = "Khegai" } },
                            }
                        },
                        Studio = new Studio()
                        {
                            Name = "Toei Animation",
                            Description = "Toei Animation is a Japanese animation studio primarily controlled by its namesake Toei Company.",
                            Country = new Country()
                            {
                                Name = "Japan"
                            }
                        }
                    },
                    new AnimeStudio()
                    {
                        Anime = new Anime()
                        {
                            Title = "Dota: Dragon's Blood",
                            ReleaseDate = new DateTime(2021,3,25),
                            AnimeGenres = new List<AnimeGenre>()
                            {
                                new AnimeGenre { Genre = new Genre() { Name = "Fantasy"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Great lore",Text = "As a regular Dota 2 player, love how the lore turns out!", Rating = 4,
                                Reviewer = new Reviewer(){ FirstName = "Andrei", LastName = "Guskov" } },
                                new Review { Title="Nice fantasy anime",Text = "A good fantasy anime", Rating = 3,
                                Reviewer = new Reviewer(){ FirstName = "Sergei", LastName = "Aleksandrov" } },
                                new Review { Title="Awful",Text = "I hate Dota 2 game, and this anime is as bad as the game", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Yulia", LastName = "Chugueva" } },
                            }
                        },
                        Studio = new Studio()
                        {
                            Name = "Studio Mir",
                            Description = "Studio Mir is a South Korean animation studio based in Seoul. Among other works, the studio animated most of the American TV series.",
                            Country = new Country()
                            {
                                Name = "South Korea"
                            }
                        }
                    }
                };
                _context.AnimeStudios.AddRange(animeStudios);
                _context.SaveChanges();
            }
        }
    }
}