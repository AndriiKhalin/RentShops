using AutoMapper;
using Entities.DTO.TransportDTO;
using Entities.Models;
using Microsoft.Extensions.Configuration;

namespace Helpers;

public class TransportUrlResolver : IValueResolver<Transport, TransportDto, string>
{
    private readonly IConfiguration _configuration;

    public TransportUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(Transport source, TransportDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.ImgUrl))
        {
            return _configuration["API_url"] + source.ImgUrl;
        }
        return null;
    }
}