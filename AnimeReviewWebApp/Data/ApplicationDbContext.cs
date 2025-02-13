using AnimeReviewWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeReviewWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Anime> Anime { get; set; }
        public DbSet<AnimeStudio> AnimeStudios { get; set; }
        public DbSet<AnimeGenre> AnimeGenres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimeGenre>().HasKey(ag => new { ag.AnimeId, ag.GenreId });
            modelBuilder.Entity<AnimeGenre>().HasOne(a => a.Anime).WithMany(ag => ag.AnimeGenres).HasForeignKey(a => a.AnimeId);
            modelBuilder.Entity<AnimeGenre>().HasOne(a => a.Genre).WithMany(ag => ag.AnimeGenres).HasForeignKey(g => g.GenreId);

            modelBuilder.Entity<AnimeStudio>().HasKey(a => new { a.AnimeId, a.StudioId });
            modelBuilder.Entity<AnimeStudio>().HasOne(a => a.Anime).WithMany(ag => ag.AnimeStudios).HasForeignKey(a => a.AnimeId);
            modelBuilder.Entity<AnimeStudio>().HasOne(a => a.Studio).WithMany(ag => ag.AnimeStudios).HasForeignKey(g => g.StudioId);
        }
    }
}
