using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentShop_API.Dto;
using RentShop_API.Repository;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopRepository _shopRepository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;

        public ShopController(IShopRepository shopRepository, RentDbContext context, IMapper mapper)
        {
            _shopRepository = shopRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Shop>))]
        public async Task<IActionResult> GetShops()
        {
            var shops = _mapper.Map<List<ShopDto>>(await _shopRepository.GetShops());
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
            if (!await _shopRepository.ShopExists(shopId))
            {
                return NotFound();
            }

            var shopById = _mapper.Map<ShopDto>(await _shopRepository.GetShop(shopId));
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
            if (!await _shopRepository.ShopExists(adressShop))
            {
                return NotFound();
            }

            var shopByAdress = _mapper.Map<ShopDto>(await _shopRepository.GetShop(adressShop));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(shopByAdress);
        }

    }
}
