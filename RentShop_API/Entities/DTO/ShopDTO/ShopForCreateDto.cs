using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Models.DTO.ShopDTO;

public class ShopForCreateDto
{
    [Required(ErrorMessage = "Address is required")]
    [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Work time start is required")]
    public TimeSpan WorkTimeStart { get; set; }

    [Required(ErrorMessage = "Work time end is required")]
    public TimeSpan WorkTimeEnd { get; set; }

    public IFormFile? ImgUrl { get; set; }
}