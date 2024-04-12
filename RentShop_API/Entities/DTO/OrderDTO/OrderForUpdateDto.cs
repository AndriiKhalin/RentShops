using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Models.DTO.OrderDTO;

public class OrderForUpdateDto
{
    [Range(0, float.MaxValue, ErrorMessage = "Price must be a positive number")]
    public float? Price { get; set; }
    public DateTime? OrderDateFrom { get; set; }

    public DateTime? OrderDateTo { get; set; }
    public IFormFile? TransportImgUrl { get; set; }
}