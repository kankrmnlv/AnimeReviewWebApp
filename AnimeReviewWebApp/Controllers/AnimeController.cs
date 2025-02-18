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
        private readonly IReviewInterface _reviewInterface;

        public AnimeController(IAnimeInterface animeInterface, IMapper mapper, IReviewInterface reviewInterface)
        {
            _animeInterface = animeInterface;
            _mapper = mapper;
            _reviewInterface = reviewInterface;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAnime([FromQuery] int studioId, [FromQuery] int genreId, [FromBody] AnimeDto createAnime)
        {
            if (createAnime == null)
            {
                return BadRequest(ModelState);
            }

            var anime = _animeInterface.GetAnimeTrimToUpper(createAnime);

            if (anime != null)
            {
                ModelState.AddModelError("", "Anime already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var animeMap = _mapper.Map<Anime>(createAnime);

            if (!_animeInterface.CreateAnime(studioId, genreId, animeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{animId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAnime(int animId, [FromQuery] int studioId, [FromQuery] int genreId, [FromBody] AnimeDto updatedAnime)
        {
            if (updatedAnime == null)
            {
                return BadRequest(ModelState);
            }

            if (animId != updatedAnime.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_animeInterface.AnimeExists(animId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var animeMap = _mapper.Map<Anime>(updatedAnime);

            if (!_animeInterface.UpdateAnime(studioId, genreId, animeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the anime");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{animeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAnime(int animeId)
        {
            if (!_animeInterface.AnimeExists(animeId))
            {
                return NotFound(ModelState);
            }

            var reviewsToDelete = _reviewInterface.GetReviewsOfAnime(animeId);
            var animeToDelete = _animeInterface.GetAnime(animeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewInterface.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviews");

            }

            if (!_animeInterface.DeleteAnime(animeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the anime");
            }

            return NoContent();
        }
    }
}
