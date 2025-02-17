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
        private readonly ICountryInterface _countryInterface;

        public StudioController(IStudioInterface studioInterface, IMapper mapper, ICountryInterface countryInterface)
        {
            _studioInterface = studioInterface;
            _mapper = mapper;
            _countryInterface = countryInterface;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStudio([FromQuery] int countryId, [FromBody] StudioDto createStudio)
        {
            if (createStudio == null)
            {
                return BadRequest(ModelState);
            }

            var studio = _studioInterface.GetStudios().Where(s => s.Name.Trim().ToUpper() == createStudio.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (studio != null)
            {
                ModelState.AddModelError("", "Studio already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studioMap = _mapper.Map<Studio>(createStudio);

            studioMap.Country = _countryInterface.GetCountry(countryId);

            if (!_studioInterface.CreateStudio(studioMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{studioId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStudio(int studioId, [FromBody] StudioDto updatedStudio)
        {
            if (updatedStudio == null)
            {
                return BadRequest(ModelState);
            }

            if (studioId != updatedStudio.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_studioInterface.StudioExists(studioId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studioMap = _mapper.Map<Studio>(updatedStudio);

            if (!_studioInterface.UpdateStudio(studioMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the studio");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
