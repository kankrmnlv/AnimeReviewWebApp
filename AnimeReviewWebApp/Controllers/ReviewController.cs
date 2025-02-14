using AnimeReviewWebApp.Dto;
using AnimeReviewWebApp.Interfaces;
using AnimeReviewWebApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeReviewWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewInterface _reviewInterface;
        private readonly IMapper _mapper;

        public ReviewController(IReviewInterface reviewInterface, IMapper mapper)
        {
            _reviewInterface = reviewInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewInterface.GetReviews());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewInterface.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var review = _mapper.Map<ReviewDto>(_reviewInterface.GetReview(reviewId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(review);
        }

        [HttpGet("anime/{animId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfAnime(int animId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewInterface.GetReviewsOfAnime(animId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reviews);
        }
    }
}
