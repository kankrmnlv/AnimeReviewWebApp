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
        private readonly IReviewerInterface _reviewerInterface;
        private readonly IAnimeInterface _animeInterface;

        public ReviewController(IReviewInterface reviewInterface, IMapper mapper, IReviewerInterface reviewerInterface, IAnimeInterface animeInterface)
        {
            _reviewInterface = reviewInterface;
            _mapper = mapper;
            _reviewerInterface = reviewerInterface;
            _animeInterface = animeInterface;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int animId, [FromBody] ReviewDto createReview)
        {
            if (createReview == null)
            {
                return BadRequest(ModelState);
            }

            var review = _reviewInterface.GetReviews().Where(r => r.Title.Trim().ToUpper() == createReview.Title.TrimEnd().ToUpper()).FirstOrDefault();

            if (review != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewMap = _mapper.Map<Review>(createReview);

            reviewMap.Anime = _animeInterface.GetAnime(animId);
            reviewMap.Reviewer = _reviewerInterface.GetReviewer(reviewerId);

            if (!_reviewInterface.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto updatedReview)
        {
            if (updatedReview == null)
            {
                return BadRequest(ModelState);
            }

            if (reviewId != updatedReview.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewInterface.ReviewExists(reviewId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewMap = _mapper.Map<Review>(updatedReview);

            if (!_reviewInterface.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewInterface.ReviewExists(reviewId))
            {
                return NotFound(ModelState);
            }

            var reviewToDelete = _reviewInterface.GetReview(reviewId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewInterface.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the review");
            }

            return NoContent();
        }
    }
}
