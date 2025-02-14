﻿using AnimeReviewWebApp.Models;

namespace AnimeReviewWebApp.Interfaces
{
    public interface IReviewerInterface
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int reviewerId);
        ICollection<Review> GetReviewsByReviewer(int reviewerId);
        bool ReviewerExists(int reviewerId);
        bool CreateReviewer(Reviewer reviewer);
        bool Save();
    }
}
