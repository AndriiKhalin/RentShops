using AutoMapper;
using Entities.DTO;
using Entities.Models;
using Entities;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public RatingController(IWrapperRepository repository, RentDbContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Rating>))]
        public async Task<IActionResult> GetRatings()
        {
            var ratings = _mapper.Map<IEnumerable<RatingDto>>(await _repository.Rating.GetRatings());
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
            if (!await _repository.Rating.RatingExists(ratingId))
            {
                return NotFound();
            }

            var rating = _mapper.Map<RatingDto>(await _repository.Rating.GetRating(ratingId));
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
            if (!await _repository.Rating.RatingExists(ratingId))
            {
                return NotFound();
            }

            var userByRating = _mapper.Map<UserDto>(await _repository.Rating.GetUserByRating(ratingId));
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

            var ratingsByUser = _mapper.Map<IEnumerable<RatingDto>>(await _repository.Rating.GetRatingsByUser(userId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(ratingsByUser);
        }
    }
}
