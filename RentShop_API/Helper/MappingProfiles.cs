using AutoMapper;
using RentShop_API.Dto;


namespace RentShop_API.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<Transport, TransportDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Rating, RatingDto>();
        CreateMap<Transaction, TransactionDto>();
        CreateMap<LogTransaction, LogTransactionDto>();
        CreateMap<TransportAvailable, TransportAvailableDto>();
        CreateMap<Shop, ShopDto>();
    }
}