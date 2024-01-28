using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentShop_API.Dto;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public RatingController(IRatingRepository ratingRepository, RentDbContext context, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Rating>))]
        public async Task<IActionResult> GetRatings()
        {
            var ratings = _mapper.Map<List<RatingDto>>(await _ratingRepository.GetRatings());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(ratings);
        }

        [HttpGet("{ratingId}")]
        [ProducesResponseType(200, Type = typeof(Rating))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRating(Guid ratingId)
        {
            if (!await _ratingRepository.RatingExists(ratingId))
            {
                return NotFound();
            }

            var rating = _mapper.Map<RatingDto>(await _ratingRepository.GetRating(ratingId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(rating);
        }

        [HttpGet("{ratingId}/user")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserByRating(Guid ratingId)
        {
            if (!await _ratingRepository.RatingExists(ratingId))
            {
                return NotFound();
            }

            var userByRating = _mapper.Map<UserDto>(await _ratingRepository.GetUserByRating(ratingId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(userByRating);
        }

        [HttpGet("{userId}/ratings")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Rating>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRatingsByUser(Guid userId)
        {
            if (!await _context.Users.AnyAsync(x => x.Id == userId))
            {
                return NotFound();
            }

            var ratingsByUser = _mapper.Map<List<RatingDto>>(await _ratingRepository.GetRatingsByUser(userId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(ratingsByUser);
        }
    }
}
