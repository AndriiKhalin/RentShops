using AutoMapper;
using Entities;
using Entities.DTO.ShopDTO;
using Entities.Models;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Security.Cryptography.Xml;

namespace Repository;

public class ShopRepository : BaseRepository<Shop>, IShopRepository
{
    private readonly RentDbContext _context;
    private readonly IFileProvider _fileProvider;
    private readonly IMapper _mapper;

    public ShopRepository(RentDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
    {
        _context = context;
        _fileProvider = fileProvider;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Shop>> GetShops()
    {
        return await GetAll().Result.OrderBy(x => x.CreatedUpdatedAt).ToListAsync();
    }

    public async Task<Shop> GetShop(Guid id)
    {
        return await GetByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Shop> GetShop(string adressShop)
    {
        return await GetByCondition(x => x.Address.Contains(adressShop)).FirstOrDefaultAsync();
    }

    public async Task<bool> ShopExists(Guid id)
    {
        return await Exists(id);
    }

    public async Task<Shop> CreateShop(ShopForCreateDto shop)
    {

        var src = "";
        string rootImg = "/Upload/Shop/";
        var shopname = $"{shop.Address}({Guid.NewGuid()}){Path.GetExtension(shop.ImgUrl.FileName)}";

        if (shop.ImgUrl is not null)
        {
            var root = @"D:\IT\My_Projects\RentShop\RentShop_UI\Stuff\Images\Upload\Shop\";


            var directoryPath = Path.GetDirectoryName(root);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            src = Path.Combine(root, shopname);

            using (var fileStream = new FileStream(src, FileMode.Create))
            {
                await shop.ImgUrl.CopyToAsync(fileStream);
            }
        }


        var shopMap = _mapper.Map<Shop>(shop);
        shopMap.ImgUrl = rootImg + shopname;
        shopMap.CreatedUpdatedAt = DateTime.Now;

        await Create(shopMap);
        return shopMap;
    }

    public void DeleteShop(Guid id)
    {
        Delete(id);
    }

    public async Task UpdateShop(Guid shopId, ShopForUpdateDto shop)
    {
        var shopEntity = await GetByCondition(x => x.Id == shopId).FirstOrDefaultAsync();
        var src = "";
        var root = @"D:\IT\My_Projects\RentShop\RentShop_UI\Stuff\Images\Upload\Shop\";
        string rootImg = "/Upload/Shop/";
        var shopname = $"{shop.Address}({Guid.NewGuid()}){Path.GetExtension(shop.ImgUrl.FileName)}";

        if (shop.ImgUrl is not null)
        {

            var directoryPath = Path.GetDirectoryName(root);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            src = Path.Combine(root, shopname);

            using (var fileStream = new FileStream(src, FileMode.Create))
            {
                await shop.ImgUrl.CopyToAsync(fileStream);
            }
        }

        if (!string.IsNullOrEmpty(shopEntity.ImgUrl))
        {
            string oldsrc = shopEntity.ImgUrl;
            System.IO.File.Delete(oldsrc);
        }

        _mapper.Map(shop, shopEntity);
        shopEntity.ImgUrl = rootImg + shopname;
        shopEntity.CreatedUpdatedAt = DateTime.Now;

        Update(shopEntity);
    }

    public async Task<bool> ShopExists(string adressName)
    {
        return await Exists(adressName);
    }
}