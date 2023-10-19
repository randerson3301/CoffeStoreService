namespace CoffeStore.EcommerceApp.Dtos
{
    public class CustomerDto
    {
        public CustomerDto()
        {
        }

        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public string? Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}