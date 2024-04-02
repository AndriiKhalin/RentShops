using AutoMapper;
using Entities.DTO.OrderDTO;
using Entities.Models;
using Microsoft.Extensions.Configuration;

namespace Helpers.UrlResolver;

public class OrderUrlResorver : IValueResolver<Order, OrderDto, string>
{
    private readonly IConfiguration _configuration;

    public OrderUrlResorver(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string Resolve(Order source, OrderDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.TransportImgUrl))
        {
            return _configuration["API_url"] + source.TransportImgUrl;
        }
        return null;
    }
}