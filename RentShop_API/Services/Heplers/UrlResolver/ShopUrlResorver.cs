using AutoMapper;
using Models.DTO.ShopDTO;
using Models.Entities;
using Microsoft.Extensions.Configuration;

namespace Services.Heplers.UrlResolver;

public class ShopUrlResorver : IValueResolver<Shop, ShopDto, string>
{
    private readonly IConfiguration _configuration;

    public ShopUrlResorver(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string Resolve(Shop source, ShopDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.ImgUrl))
        {
            return _configuration["API_url"] + source.ImgUrl;
        }
        return null;
    }
}