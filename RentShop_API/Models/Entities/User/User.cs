namespace RentShop_API.Models.Entities.User
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string Status { get; set; }

    }
}
