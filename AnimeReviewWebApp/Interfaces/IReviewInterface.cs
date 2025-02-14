﻿using AnimeReviewWebApp.Models;

namespace AnimeReviewWebApp.Interfaces
{
    public interface IReviewInterface
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfAnime(int animId);
        bool ReviewExists(int reviewId);
        bool CreateReview(Review review);
        bool Save();
    }
}
