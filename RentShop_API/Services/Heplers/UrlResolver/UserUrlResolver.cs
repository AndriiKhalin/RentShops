using AutoMapper;
using Models.DTO.UserDTO;
using Models.Entities;
using Microsoft.Extensions.Configuration;

namespace Services.Heplers.UrlResolver;

public class UserUrlResolver : IValueResolver<User, UserDto, string>
{
    private readonly IConfiguration _configuration;

    public UserUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(User source, UserDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.ImgUrl))
        {
            return _configuration["API_url"] + source.ImgUrl;
        }
        return null;
    }
}