namespace Models.DTO.UserDTO;

public class UserDto
{
    public Guid Id { get; set; }

    public DateTime CreatedUpdatedAt { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }


    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime BirthDate { get; set; }

    public string Phone { get; set; }

    public string Role { get; set; }

    public string ImgUrl { get; set; }
}