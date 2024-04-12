using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Models.DTO.UserDTO;

public class UserForUpdateDto
{


    [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
    public string? FirstName { get; set; }


    [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
    public string? LastName { get; set; }


    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }


    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters")]
    public string? Password { get; set; }


    public DateTime? BirthDate { get; set; }


    [Phone(ErrorMessage = "Invalid phone number")]
    public string? Phone { get; set; }


    public string? Role { get; set; }

    public IFormFile? ImgUrl { get; set; }
}