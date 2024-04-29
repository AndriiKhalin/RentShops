using AutoMapper;
using Interfaces.IEntityService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO.RatingDTO;
using Models.DTO.UserDTO;
using Interfaces.ILoggerService;
using Interfaces.IRepository;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public UserController(IUserService userService, RentDbContext context, IMapper mapper,
            ILoggerManager logger)
        {
            _userService = userService;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        public async Task<IActionResult> GetUsers()
        {
            var users = _mapper.Map<IEnumerable<UserDto>>(await _userService.GetUsers());
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
            if (!await _userService.UserExists(userId))
            {
                _logger.LogError($"User with id: {userId}, hasn't been found in db.");
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(await _userService.GetUser(userId));
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
            if (!await _userService.UserExists(userName))
            {
                _logger.LogError($"User with  name: {userName}, hasn't been found in db.");
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(await _userService.GetUser(userName));
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
            if (!await _userService.UserExists(userId))
            {
                _logger.LogError($"User with id: {userId}, hasn't been found in db.");
                return NotFound();
            }

            var lastDateOrder = await _userService.GetLastUserOrder(userId);
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
            if (!await _userService.UserExists(userId))
            {
                _logger.LogError($"User with id: {userId}, hasn't been found in db.");
                return NotFound();
            }

            var ratingsByUser = _mapper.Map<IEnumerable<RatingDto>>(await _userService.GetRatingsByUser(userId));
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
        public async Task<IActionResult> CreateUser([FromForm] UserForCreateDto userCreate)
        {

            var userMap = await _userService.CreateUser(userCreate);

            var createdUser = _mapper.Map<UserDto>(userMap);

            return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.Id }, createdUser);
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromForm] UserForUpdateDto userUpdate)
        {

            await _userService.UpdateUser(userId, userUpdate);

            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            await _userService.DeleteUser(userId);

            return NoContent();

        }
    }



}

