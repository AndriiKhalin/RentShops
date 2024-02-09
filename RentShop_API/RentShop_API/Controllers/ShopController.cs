using AutoMapper;
using Entities.Models;
using Entities;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.DTO.ShopDTO;
using Interfaces.ILoggerService;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IWrapperRepository _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public ShopController(IWrapperRepository repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShopDto>))]
        public async Task<IActionResult> GetShops()
        {
            var shops = _mapper.Map<IEnumerable<ShopDto>>(await _repository.Shop.GetShops());
            _logger.LogInfo("We take all shops from database");
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo("We returned all shops from database");
            return Ok(shops);
        }

        [HttpGet("byId/{shopId}")]
        [ProducesResponseType(200, Type = typeof(ShopDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShop(Guid shopId)
        {
            if (!await _repository.Shop.ShopExists(shopId))
            {
                _logger.LogError($"Shop with id: {shopId}, hasn't been found in db.");
                return NotFound();
            }

            var shopById = _mapper.Map<ShopDto>(await _repository.Shop.GetShop(shopId));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned shop with id: {shopId}");
            return Ok(shopById);
        }

        [HttpGet("byName/{adressShop}")]
        [ProducesResponseType(200, Type = typeof(ShopDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShop(string adressShop)
        {
            if (!await _repository.Shop.ShopExists(adressShop))
            {
                _logger.LogError($"Transaction with adress: {adressShop}, hasn't been found in db.");
                return NotFound();
            }

            var shopByAdress = _mapper.Map<ShopDto>(await _repository.Shop.GetShop(adressShop));
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInfo($"Returned shop with adress: {adressShop}");
            return Ok(shopByAdress);
        }
    }
}
