using AutoMapper;
using Entities.Models;
using Entities;
using Entities.DTO.RatingDTO;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Interfaces.ILoggerService;
using Entities.DTO.UserDTO;
using Microsoft.EntityFrameworkCore;
using Entities.DTO.CategoryDTO;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public UserController(IUnitOfWork repository, RentDbContext context, IMapper mapper,
            ILoggerManager logger)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        public async Task<IActionResult> GetUsers()
        {
            var users = _mapper.Map<IEnumerable<UserDto>>(await _repository.User.GetUsers());
            _logger.LogInfo("We take users from database");

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            _logger.LogInfo("We returned all users from database");
            return Ok(users);
        }

        [HttpGet("id/{userId}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            if (!await _repository.User.UserExists(userId))
            {
                _logger.LogError($"User with id: {userId}, hasn't been found in db.");
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(await _repository.User.GetUser(userId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            _logger.LogInfo($"Returned user with id: {userId}");
            return Ok(user);
        }

        [HttpGet("name/{userName}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserByName(string userName)
        {
            if (!await _repository.User.UserExists(userName))
            {
                _logger.LogError($"User with  name: {userName}, hasn't been found in db.");
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(await _repository.User.GetUser(userName));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            _logger.LogInfo($"Returned user with  name: {userName}");
            return Ok(user);
        }

        [HttpGet("{userId}/lastOrder")]
        [ProducesResponseType(200, Type = typeof(DateTime))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetLastUserOrder(Guid userId)
        {
            if (!await _repository.User.UserExists(userId))
            {
                _logger.LogError($"User with id: {userId}, hasn't been found in db.");
                return NotFound();
            }

            var lastDateOrder = await _repository.User.GetLastUserOrder(userId);
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            _logger.LogInfo($"Returned last order by user with id: {userId}");
            return Ok(lastDateOrder);
        }

        [HttpGet("{userId}/ratings")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RatingDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRatingsByUser(Guid userId)
        {
            if (!await _repository.User.UserExists(userId))
            {
                _logger.LogError($"User with id: {userId}, hasn't been found in db.");
                return NotFound();
            }

            var ratingsByUser = _mapper.Map<IEnumerable<RatingDto>>(await _repository.User.GetRatingsByUser(userId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            _logger.LogInfo($"Returned ratings by user with id: {userId}");
            return Ok(ratingsByUser);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromBody] UserForCreateDto userCreate)
        {
            if (userCreate == null)
            {
                _logger.LogError("User object is null");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            var userMap = _mapper.Map<User>(userCreate);

            await _repository.User.CreateUser(userMap);
            await _repository.Save();

            _logger.LogInfo($"New User create success");
            var createdUser = _mapper.Map<UserDto>(userMap);

            return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.Id }, createdUser);
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserForUpdateDto userUpdate)
        {
            if (userUpdate == null)
            {
                _logger.LogError($"User object sent from client is null.");
                return BadRequest(ModelState);
            }

            if (!await _repository.User.UserExists(userId))
            {
                _logger.LogError($"User with id: {userId}, hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            var userEntity = await _repository.User.GetUser(userId);

            _mapper.Map(userUpdate, userEntity);

            _repository.User.UpdateUser(userEntity);
            await _repository.Save();

            _logger.LogInfo($"Update User with ID: {userId}");
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            if (!await _repository.User.UserExists(userId))
            {
                _logger.LogError($"User with id: {userId}, hasn't been found in db.");
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            _repository.User.DeleteUser(userId);
            await _repository.Save();

            _logger.LogInfo($"User delete with id: {userId} in our database");

            return NoContent();

        }
    }



}

