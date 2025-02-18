using AnimeReviewWebApp.Data;
using AnimeReviewWebApp.Models;
using AnimeReviewWebApp.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeReviewWebApp.Test.Repository
{
    public class AnimeRepositoryTest
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            if(await context.Anime.CountAsync() <= 0)
            {
                for(int i = 0; i < 10; i++)
                {
                    context.Anime.Add(new Anime()
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
                    });
                    await context.SaveChangesAsync();
                }
            }

            return context;
        }

        [Fact]
        public async void AnimeRepository_GetAnime_ReturnsAnAnime()
        {
            //Arrange
            var name = "Attack on Titan";
            var dbContext = await GetDbContext();
            var animeRepos = new AnimeRepository(dbContext);

            //Act
            var result = animeRepos.GetAnime(name);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Anime>();
        }

        [Fact]
        public async void AnimeRepository_GetAnimeRating_ReturnsDecimalBetween0and10()
        {
            //Arrange
            var animId = 1;
            var dbContext = await GetDbContext();
            var animeRepos = new AnimeRepository(dbContext);

            //Act
            var result = animeRepos.GetAnimeRating(animId);

            //Assert
            result.Should().NotBe(0);
            result.Should().BeInRange(1, 10);
        }
    }
}
