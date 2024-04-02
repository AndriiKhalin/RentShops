using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO.ShopDTO;

public class ShopForUpdateDto
{
    [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters")]
    public string? Address { get; set; }

    public TimeSpan? WorkTimeStart { get; set; }

    public TimeSpan? WorkTimeEnd { get; set; }
    public IFormFile? ImgUrl { get; set; }
}