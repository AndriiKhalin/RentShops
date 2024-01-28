namespace RentShop_API.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        public string Phone { get; set; }

        public Role Role { get; set; }
    }
}
