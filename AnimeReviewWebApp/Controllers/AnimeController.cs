using AnimeReviewWebApp.Dto;
using AnimeReviewWebApp.Interfaces;
using AnimeReviewWebApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeReviewWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : Controller
    {
        private readonly IAnimeInterface _animeInterface;
        private readonly IMapper _mapper;
        public AnimeController(IAnimeInterface animeInterface, IMapper mapper)
        {
            _animeInterface = animeInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Anime>))]
        public IActionResult GetAnimeList()
        {
            var anime = _mapper.Map<List<AnimeDto>>(_animeInterface.GetAnimeList());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(anime);
        }

        [HttpGet("{animId}")]
        [ProducesResponseType(200, Type = typeof(Anime))]
        [ProducesResponseType(400)]
        public IActionResult GetAnime(int animId)
        {
            if (!_animeInterface.AnimeExists(animId))
            {
                return NotFound();
            }

            var anime = _mapper.Map<AnimeDto>(_animeInterface.GetAnime(animId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(anime);
        }

        [HttpGet("{animId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetAnimeRating(int animId)
        {
            if (!_animeInterface.AnimeExists(animId))
            {
                return NotFound();
            }

            var rating = _animeInterface.GetAnimeRating(animId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(rating);
        }
    }
}
