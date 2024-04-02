using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Entities.DTO.TransportDTO;

public class TransportForUpdateDto
{
    [StringLength(50, ErrorMessage = "Model can't be longer than 50 characters")]
    public string? Mark { get; set; }

    [StringLength(50, ErrorMessage = "Mark can't be longer than 50 characters")]
    public string? Model { get; set; }


    [Range(0, double.MaxValue, ErrorMessage = "Price per minute must be a positive number")]
    public float? PriceMinute { get; set; }


    [Range(0, int.MaxValue, ErrorMessage = "Maximum speed must be a positive number")]
    public int? MaxSpeed { get; set; }


    public IFormFile? ImgUrl { get; set; }


    [Range(0, int.MaxValue, ErrorMessage = "Maximum weight must be a positive number")]
    public int? MaxWeight { get; set; }

}