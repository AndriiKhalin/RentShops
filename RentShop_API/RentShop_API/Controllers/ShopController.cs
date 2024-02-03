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
    public class ShopController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public ShopController(IWrapperRepository repository, RentDbContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Shop>))]
        public async Task<IActionResult> GetShops()
        {
            var shops = _mapper.Map<IEnumerable<ShopDto>>(await _repository.Shop.GetShops());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(shops);
        }

        [HttpGet("byId/{shopId}")]
        [ProducesResponseType(200, Type = typeof(Shop))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShop(Guid shopId)
        {
            if (!await _repository.Shop.ShopExists(shopId))
            {
                return NotFound();
            }

            var shopById = _mapper.Map<ShopDto>(await _repository.Shop.GetShop(shopId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(shopById);
        }

        [HttpGet("byName/{adressShop}")]
        [ProducesResponseType(200, Type = typeof(Shop))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShop(string adressShop)
        {
            if (!await _repository.Shop.ShopExists(adressShop))
            {
                return NotFound();
            }

            var shopByAdress = _mapper.Map<ShopDto>(await _repository.Shop.GetShop(adressShop));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(shopByAdress);
        }
    }
}
