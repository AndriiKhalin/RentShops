using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Entities.DTO.TransportDTO;

public class TransportForCreateDto
{
    [Required(ErrorMessage = "Model is required")]
    [StringLength(50, ErrorMessage = "Model can't be longer than 50 characters")]
    public string? Mark { get; set; }

    [Required(ErrorMessage = "Mark is required")]
    [StringLength(50, ErrorMessage = "Mark can't be longer than 50 characters")]
    public string? Model { get; set; }

    [Required(ErrorMessage = "Price per minute is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Price per minute must be a positive number")]
    public float PriceMinute { get; set; }

    [Required(ErrorMessage = "Maximum speed is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Maximum speed must be a positive number")]
    public int MaxSpeed { get; set; }

    [Required(ErrorMessage = "Image URL is required")]
    public IFormFile ImgUrl { get; set; }

    [Required(ErrorMessage = "Maximum weight is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Maximum weight must be a positive number")]
    public int MaxWeight { get; set; }


}