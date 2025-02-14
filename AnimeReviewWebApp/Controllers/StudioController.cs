using AnimeReviewWebApp.Data;
using AnimeReviewWebApp.Dto;
using AnimeReviewWebApp.Interfaces;
using AnimeReviewWebApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeReviewWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudioController : Controller
    {
        private readonly IStudioInterface _studioInterface;
        private readonly IMapper _mapper;

        public StudioController(IStudioInterface studioInterface, IMapper mapper)
        {
            _studioInterface = studioInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Studio>))]
        public IActionResult GetStudios()
        {
            var studios = _mapper.Map<List<StudioDto>>(_studioInterface.GetStudios());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(studios);
        }

        [HttpGet("{studioId}")]
        [ProducesResponseType(200, Type = typeof(Studio))]
        [ProducesResponseType(400)]
        public IActionResult GetStudio(int studioId)
        {
            if (!_studioInterface.StudioExists(studioId))
            {
                return NotFound();
            }

            var studio = _mapper.Map<StudioDto>(_studioInterface.GetStudio(studioId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(studio);
        }

        [HttpGet("{studioId}/anime")]
        [ProducesResponseType(200, Type = typeof(Studio))]
        [ProducesResponseType(400)]
        public IActionResult GetAnimeByStudio(int studioId)
        {
            if (!_studioInterface.StudioExists(studioId))
            {
                return NotFound();
            }

            var studio = _mapper.Map<List<AnimeDto>>(_studioInterface.GetAnimeByStudio(studioId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(studio);
        }
    }
}
