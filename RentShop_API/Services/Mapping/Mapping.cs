using AutoMapper;
using Models.DTO.LogTransactionDTO;
using Models.DTO.OrderDTO;
using Models.DTO.RatingDTO;
using Models.DTO.ShopDTO;
using Models.DTO.TransactionDTO;
using Models.DTO.TransportAvailableDTO;
using Models.DTO.TransportCategoryDTO;
using Models.DTO.TransportDTO;
using Models.DTO.UserDTO;
using Models.Entities;
using Services.Heplers.UrlResolver;

namespace Services.Mapping;

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

        CreateMap<Transport, TransportDto>().ReverseMap();
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

        CreateMap<Shop, ShopDto>().ReverseMap();
        CreateMap<Shop, ShopForCreateDto>().ReverseMap();
        CreateMap<Shop, ShopForUpdateDto>().ReverseMap();

    }
}