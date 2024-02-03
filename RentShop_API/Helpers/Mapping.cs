using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace Helpers;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Transport, TransportDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Rating, RatingDto>().ReverseMap();
        CreateMap<Transaction, TransactionDto>().ReverseMap();
        CreateMap<LogTransaction, LogTransactionDto>().ReverseMap();
        CreateMap<TransportAvailable, TransportAvailableDto>().ReverseMap();
        CreateMap<Shop, ShopDto>().ReverseMap();
    }
}