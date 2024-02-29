using AutoMapper;
using Entities.Models;
using Entities;
using Interfaces.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.DTO.ShopDTO;
using Interfaces.ILoggerService;
using Entities.DTO.CategoryDTO;

namespace RentShop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly RentDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public ShopController(IUnitOfWork repository, RentDbContext context, IMapper mapper, ILoggerManager logger)
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

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ShopDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateShop([FromBody] ShopForCreateDto shopCreate)
        {
            if (shopCreate == null)
            {
                _logger.LogError("Shop object is null");
                return BadRequest(ModelState);
            }

            if (await _repository.Shop.ShopExists(shopCreate.Address))
            {
                _logger.LogError($"Shop object with:{shopCreate.Address} already exist in our database");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            var shopMap = _mapper.Map<Shop>(shopCreate);

            await _repository.Shop.CreateShop(shopMap);
            await _repository.Save();

            _logger.LogInfo($"New Shop create success");
            var createdShop = _mapper.Map<ShopDto>(shopMap);

            return CreatedAtAction(nameof(GetShop), new { shopId = createdShop.Id }, createdShop);
        }


        [HttpPut("{shopId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateShop(Guid shopId, [FromBody] ShopForUpdateDto shopUpdate)
        {
            if (shopUpdate == null)
            {
                _logger.LogError($"Shop object sent from client is null.");
                return BadRequest(ModelState);
            }

            if (!await _repository.Shop.ShopExists(shopId))
            {
                _logger.LogError($"Shop with id: {shopId}, hasn't been found in db.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }

            var shopEntity = await _repository.Shop.GetShop(shopId);

            _mapper.Map(shopUpdate, shopEntity);

            _repository.Shop.UpdateShop(shopEntity);
            await _repository.Save();

            _logger.LogInfo($"Update Shop with ID: {shopId}");
            return NoContent();
        }

        [HttpDelete("{shopId}")]
        public async Task<IActionResult> DeleteShop(Guid shopId)
        {
            if (!await _repository.Shop.ShopExists(shopId))
            {
                _logger.LogError($"Shop with id: {shopId}, hasn't been found in db.");
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                _logger.LogWarn("Model is invalid");
                return BadRequest(ModelState);
            }
            _repository.Shop.DeleteShop(shopId);
            await _repository.Save();

            _logger.LogInfo($"Shop delete with id: {shopId} in our database");

            return NoContent();

        }
    }
}
