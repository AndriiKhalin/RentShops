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

namespace Helpers;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserForCreateDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();

        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Order, OrderForCreateDto>().ReverseMap();
        CreateMap<Order, OrderForUpdateDto>().ReverseMap();

        CreateMap<Transport, TransportDto>()
            .ForMember(x => x.ImgUrl, o => o.MapFrom<TransportUrlResolver>()).ReverseMap();
        CreateMap<Transport, TransportForCreateDto>().ReverseMap();
        CreateMap<Transport, TransportForUpdateDto>().ReverseMap();

        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryForCreateDto>().ReverseMap();
        CreateMap<Category, CategoryForUpdateDto>().ReverseMap();

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

        CreateMap<Shop, ShopDto>().ReverseMap();
        CreateMap<Shop, ShopForCreateDto>().ReverseMap();
        CreateMap<Shop, ShopForUpdateDto>().ReverseMap();

    }
}