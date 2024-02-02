using AutoMapper;
using RentShop_API.Dto;


namespace RentShop_API.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
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