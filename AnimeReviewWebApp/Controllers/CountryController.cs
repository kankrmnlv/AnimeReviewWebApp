using AnimeReviewWebApp.Dto;
using AnimeReviewWebApp.Interfaces;
using AnimeReviewWebApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeReviewWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryInterface _countryInterface;
        private readonly IMapper _mapper;

        public CountryController(ICountryInterface countryInterface, IMapper mapper)
        {
            _mapper = mapper;
            _countryInterface = countryInterface;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryInterface.GetCountries());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryInterface.CountryExists(countryId))
            {
                return NotFound();
            }

            var country = _mapper.Map<CountryDto>(_countryInterface.GetCountry(countryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(country);
        }

        [HttpGet("/studios/{studioId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryOfStudio(int studioId)
        {
            var country = _mapper.Map<CountryDto>(_countryInterface.GetCountryByStudio(studioId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(country);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto createCountry)
        {
            if (createCountry == null)
            {
                return BadRequest(ModelState);
            }

            var country = _countryInterface.GetCountries().Where(c => c.Name.Trim().ToUpper() == createCountry.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(createCountry);
            if (!_countryInterface.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto updatedCountry)
        {
            if (updatedCountry == null)
            {
                return BadRequest(ModelState);
            }

            if (countryId != updatedCountry.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_countryInterface.CountryExists(countryId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(updatedCountry);

            if (!_countryInterface.UpdateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the country");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCountry(int countryId)
        {
            if (!_countryInterface.CountryExists(countryId))
            {
                return NotFound(ModelState);
            }

            var countryToDelete = _countryInterface.GetCountry(countryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_countryInterface.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the country");
            }

            return NoContent();
        }
    }
}
