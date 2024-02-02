using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentShop_API.Dto;
using RentShop_API.Interfaces;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, RentDbContext context, IMapper mapper)
        {
            _userRepository = userRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        public async Task<IActionResult> GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(await _userRepository.GetUsers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            if (!await _userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(await _userRepository.GetUser(userId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpGet("{userId}/lastOrder")]
        [ProducesResponseType(200, Type = typeof(DateTime))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserLastOrder(Guid userId)
        {
            if (!await _userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var lastDateOrder = await _userRepository.GetLastUserOrder(userId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(lastDateOrder);
        }

        //[HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public async Task<IActionResult> CreateUser(UserDto userCreate)
        //{
        //    if (userCreate == null)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var user = _userRepository.GetUsers().Result.FirstOrDefault(u => u.Name.Trim().ToLower() == userCreate.Name.Trim().ToLower());

        //    if (user != null)
        //    {
        //        ModelState.AddModelError("", "User already exists");
        //        return StatusCode(422, ModelState);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var userMap = _mapper.Map<User>(userCreate);

        //    if (!await _userRepository.CreateUser(userMap))
        //    {
        //        ModelState.AddModelError("", "Something wrong while saving");
        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok("Successful create");
        //}

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
            {
                return BadRequest(ModelState);
            }

            var userMap = _mapper.Map<User>(userCreate);

            var user = await _userRepository.CreateUser(userMap);

            if (user == null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userDto = _mapper.Map<UserDto>(user);

            return CreatedAtAction(nameof(GetUser), new { userId = userDto.Id }, userDto);
        }
    }
}
