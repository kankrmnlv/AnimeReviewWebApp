using AnimeReviewWebApp.Data;
using AnimeReviewWebApp.Interfaces;
using AnimeReviewWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeReviewWebApp.Repository
{
    public class ReviewerRepository : IReviewerInterface
    {
        private readonly ApplicationDbContext _context;
        public ReviewerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(r => r.Id == reviewerId);
        }
    }
}
