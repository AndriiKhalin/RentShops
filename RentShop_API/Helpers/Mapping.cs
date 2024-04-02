using AutoMapper;

using Entities.DTO.CategoryDTO;
using Entities.DTO.LogTransactionDTO;
using Entities.DTO.OrderDTO;
using Entities.DTO.RatingDTO;
using Entities.DTO.ShopDTO;
using Entities.DTO.TransactionDTO;
using Entities.DTO.TransportAvailableDTO;
using Entities.DTO.TransportDTO;
using Entities.DTO.UserDTO;
using Entities.Models;
using Helpers.UrlResolver;

namespace Helpers;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<User, UserDto>()
            .ForMember(x => x.ImgUrl, o => o.MapFrom<UserUrlResolver>())
            .ReverseMap();
        CreateMap<User, UserForCreateDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();

        CreateMap<Order, OrderDto>().ForMember(x => x.TransportImgUrl, o => o.MapFrom<OrderUrlResorver>()).ReverseMap();
        CreateMap<Order, OrderForCreateDto>().ReverseMap();
        CreateMap<Order, OrderForUpdateDto>().ReverseMap();

        CreateMap<Transport, TransportDto>()
            .ForMember(x => x.ImgUrl, o => o.MapFrom<TransportUrlResolver>()).ReverseMap();
        CreateMap<Transport, TransportForCreateDto>().ReverseMap();
        CreateMap<Transport, TransportForUpdateDto>().ReverseMap();

        CreateMap<TransportCategory, TransportCategoryDto>().ReverseMap();
        CreateMap<TransportCategory, TransportCategoryForCreateDto>().ReverseMap();
        CreateMap<TransportCategory, TransportCategoryForUpdateDto>().ReverseMap();

        CreateMap<Rating, RatingDto>().ReverseMap();
        CreateMap<Rating, RatingForCreateDto>().ReverseMap();
        CreateMap<Rating, RatingForUpdateDto>().ReverseMap();

        CreateMap<Transaction, TransactionDto>().ReverseMap();
        CreateMap<Transaction, TransactionForCreateDto>().ReverseMap();
        CreateMap<Transaction, TransactionForUpdateDto>().ReverseMap();

        CreateMap<LogTransaction, LogTransactionDto>().ReverseMap();
        CreateMap<LogTransaction, LogTransactionForCreateDto>().ReverseMap();
        CreateMap<LogTransaction, LogTransactionForUpdateDto>().ReverseMap();

        CreateMap<TransportAvailable, TransportAvailableDto>().ReverseMap();
        CreateMap<TransportAvailable, TransportAvailableForCreateDto>().ReverseMap();
        CreateMap<TransportAvailable, TransportAvailableForUpdateDto>().ReverseMap();

        CreateMap<Shop, ShopDto>().ForMember(x => x.ImgUrl, o => o.MapFrom<ShopUrlResorver>()).ReverseMap();
        CreateMap<Shop, ShopForCreateDto>().ReverseMap();
        CreateMap<Shop, ShopForUpdateDto>().ReverseMap();

    }
}