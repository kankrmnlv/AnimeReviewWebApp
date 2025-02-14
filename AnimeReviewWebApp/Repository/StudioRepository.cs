﻿using AnimeReviewWebApp.Data;
using AnimeReviewWebApp.Interfaces;
using AnimeReviewWebApp.Models;

namespace AnimeReviewWebApp.Repository
{
    public class StudioRepository : IStudioInterface
    {
        private readonly ApplicationDbContext _context;
        public StudioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<Anime> GetAnimeByStudio(int studioId)
        {
            return _context.AnimeStudios.Where(s => s.Studio.Id == studioId).Select(a => a.Anime).ToList();
        }

        public Studio GetStudio(int studioId)
        {
            return _context.Studios.Where(s => s.Id == studioId).FirstOrDefault();
        }

        public ICollection<Studio> GetStudioOfAnime(int animId)
        {
            return _context.AnimeStudios.Where(a => a.Anime.Id == animId).Select(s => s.Studio).ToList();
        }

        public ICollection<Studio> GetStudios()
        {
            return _context.Studios.ToList();
        }

        public bool StudioExists(int studioId)
        {
            return _context.Studios.Any(s => s.Id == studioId);
        }
    }
}
