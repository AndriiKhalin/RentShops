using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.OrderDTO;

public class OrderForCreateDto
{
    [Required(ErrorMessage = "Price is required")]
    [Range(0, float.MaxValue, ErrorMessage = "Price must be a positive number")]
    public float Price { get; set; }

    [Required(ErrorMessage = "Date from is required")]
    public DateTime OrderDateFrom { get; set; }

    [Required(ErrorMessage = "Date to is required")]
    public DateTime OrderDateTo { get; set; }

    public IFormFile? TransportImgUrl { get; set; }

}