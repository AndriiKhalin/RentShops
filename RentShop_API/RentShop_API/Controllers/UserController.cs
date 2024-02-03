using AutoMapper;
using Entities.DTO;
using Entities.Models;
using Entities;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public UserController(IWrapperRepository repository, RentDbContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        public async Task<IActionResult> GetUsers()
        {
            var users = _mapper.Map<IEnumerable<UserDto>>(await _repository.User.GetUsers());
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
            if (!await _repository.User.UserExists(userId))
            {
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(await _repository.User.GetUser(userId));
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
            if (!await _repository.User.UserExists(userId))
            {
                return NotFound();
            }

            var lastDateOrder = await _repository.User.GetLastUserOrder(userId);
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

            await _repository.User.CreateUser(userMap);
            await _repository.Save();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdUser = _mapper.Map<UserDto>(userMap);

            return CreatedAtAction(nameof(GetUser), new { userId = createdUser.Id }, createdUser);
        }
    }
}
