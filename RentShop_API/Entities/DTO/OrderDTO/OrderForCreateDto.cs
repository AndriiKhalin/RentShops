using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.OrderDTO;

public class OrderForCreateDto
{
    [Required(ErrorMessage = "Price is required")]
    [Range(0, float.MaxValue, ErrorMessage = "Price must be a positive number")]
    public float Price { get; set; }

    [Required(ErrorMessage = "Date from is required")]
    public DateTime DateFrom { get; set; }

    [Required(ErrorMessage = "Date to is required")]
    public DateTime DateTo { get; set; }

    [Required(ErrorMessage = "Image URL is required")]
    public IFormFile? TransportImgUrl { get; set; }
}