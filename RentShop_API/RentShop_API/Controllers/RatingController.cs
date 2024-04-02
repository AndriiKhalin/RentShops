using AutoMapper;
using Entities.Models;
using Entities;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.DTO.RatingDTO;
using Entities.DTO.TransportDTO;
using Entities.DTO.UserDTO;
using Interfaces.ILoggerService;
using Entities.DTO.CategoryDTO;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public RatingController(IUnitOfWork repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
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

        [HttpGet("{ratingId}/transport")]
        [ProducesResponseType(200, Type = typeof(TransportDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransportByRating(Guid ratingId)
        {
            if (!await _repository.Rating.RatingExists(ratingId))
            {
                _logger.LogError($"Ratings with id: {ratingId}, hasn't been found in db.");
                return NotFound();
            }

            var transportByRating = _mapper.Map<TransportDto>(await _repository.Rating.GetTransportByRating(ratingId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned transport by rating with id: {ratingId}");
            return Ok(transportByRating);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(RatingDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRating([FromQuery] Guid userId, [FromQuery] Guid transportId, [FromForm] RatingForCreateDto ratingCreate)
        {
            if (ratingCreate == null)
            {
                _logger.LogError("Rating object is null");
                return BadRequest(ModelState);
            }

            if (!await _repository.User.UserExists(userId) || !await _repository.Transport.TransportExists(transportId))
            {
                _logger.LogError($"Hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            var ratingMap = await _repository.Rating.CreateRating(userId, transportId, ratingCreate);
            await _repository.Save();

            _logger.LogInfo($"New Rating create success");
            var createdRating = _mapper.Map<RatingDto>(ratingMap);

            return CreatedAtAction(nameof(GetRating), new { ratingId = createdRating.Id }, createdRating);
        }


        [HttpPut("{ratingId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRating(Guid ratingId, [FromForm] RatingForUpdateDto ratingUpdate)
        {
            if (ratingUpdate == null)
            {
                _logger.LogError($"Rating object sent from client is null.");
                return BadRequest(ModelState);
            }

            if (!await _repository.Rating.RatingExists(ratingId))
            {
                _logger.LogError($"Rating with id: {ratingId}, hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }


            await _repository.Rating.UpdateRating(ratingId, ratingUpdate);
            await _repository.Save();


            _logger.LogInfo($"Update Rating with ID: {ratingId}");
            return NoContent();
        }

        [HttpDelete("{ratingId}")]
        public async Task<IActionResult> DeleteRating(Guid ratingId)
        {
            if (!await _repository.Rating.RatingExists(ratingId))
            {
                _logger.LogError($"Rating with id: {ratingId}, hasn't been found in db.");
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _repository.Rating.DeleteRating(ratingId);
            await _repository.Save();

            _logger.LogInfo($"Rating delete with id: {ratingId} in our database");

            return NoContent();

        }

    }
}
