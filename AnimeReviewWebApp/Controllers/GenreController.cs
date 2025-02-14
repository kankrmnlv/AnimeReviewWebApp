using AnimeReviewWebApp.Dto;
using AnimeReviewWebApp.Interfaces;
using AnimeReviewWebApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeReviewWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        private readonly IGenreInterface _genreInterface;
        private readonly IMapper _mapper;

        public GenreController(IGenreInterface genreInterface, IMapper mapper)
        {
            _genreInterface = genreInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        public IActionResult GetGenres()
        {
            var genres = _mapper.Map<List<GenreDto>>(_genreInterface.GetGenres());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(genres);
        }

        [HttpGet("{genreId}")]
        [ProducesResponseType(200, Type = typeof(Genre))]
        [ProducesResponseType(400)]
        public IActionResult GetGenre(int genreId)
        {
            if (!_genreInterface.GenreExists(genreId))
            {
                return NotFound();
            }

            var genre = _mapper.Map<GenreDto>(_genreInterface.GetGenre(genreId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(genre);
        }

        [HttpGet("anime/{genreId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Anime>))]
        [ProducesResponseType(400)]
        public IActionResult GetAnimeByGenreId(int genreId)
        {
            var anime = _mapper.Map<List<AnimeDto>>(_genreInterface.GetAnimeByGenre(genreId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(anime);
        }
    }
}
