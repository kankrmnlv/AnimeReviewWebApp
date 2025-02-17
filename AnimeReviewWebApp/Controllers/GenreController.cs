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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGenre([FromBody] GenreDto createGenre)
        {
            if(createGenre == null)
            {
                return BadRequest(ModelState);
            }

            var genre = _genreInterface.GetGenres().Where(g => g.Name.Trim().ToUpper() == createGenre.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if(genre != null)
            {
                ModelState.AddModelError("", "Genre already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genreMap = _mapper.Map<Genre>(createGenre);
            if (!_genreInterface.CreateGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{genreId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGenre(int genreId, [FromBody] GenreDto updatedGenre)
        {
            if(updatedGenre == null)
            {
                return BadRequest(ModelState);
            }

            if(genreId != updatedGenre.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_genreInterface.GenreExists(genreId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genreMap = _mapper.Map<Genre>(updatedGenre);

            if (!_genreInterface.UpdateGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the genre");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
