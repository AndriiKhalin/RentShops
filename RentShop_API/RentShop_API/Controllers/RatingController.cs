using AutoMapper;
using Entities.Models;
using Entities;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.DTO.RatingDTO;
using Entities.DTO.UserDTO;
using Interfaces.ILoggerService;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public RatingController(IWrapperRepository repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RatingDto>))]
        public async Task<IActionResult> GetRatings()
        {
            var ratings = _mapper.Map<IEnumerable<RatingDto>>(await _repository.Rating.GetRatings());
            _logger.LogInfo("We take all ratings from database");
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo("We returned all ratings from database");
            return Ok(ratings);
        }

        [HttpGet("{ratingId}")]
        [ProducesResponseType(200, Type = typeof(RatingDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRating(Guid ratingId)
        {
            if (!await _repository.Rating.RatingExists(ratingId))
            {
                _logger.LogError($"Ratings with id: {ratingId}, hasn't been found in db.");
                return NotFound();
            }

            var rating = _mapper.Map<RatingDto>(await _repository.Rating.GetRating(ratingId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned rating with id: {ratingId}");
            return Ok(rating);
        }

        [HttpGet("{ratingId}/user")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserByRating(Guid ratingId)
        {
            if (!await _repository.Rating.RatingExists(ratingId))
            {
                _logger.LogError($"Ratings with id: {ratingId}, hasn't been found in db.");
                return NotFound();
            }

            var userByRating = _mapper.Map<UserDto>(await _repository.Rating.GetUserByRating(ratingId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned user by rating with id: {ratingId}");
            return Ok(userByRating);
        }

    }
}
