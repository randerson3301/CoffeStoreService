namespace CoffeStore.EcommerceApp.Dtos
{
    public class BaseCustomerDto
    {
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
    }
}
