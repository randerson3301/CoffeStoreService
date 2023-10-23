namespace CoffeStore.EcommerceApp.Dtos
{
    public class CustomerDto: BaseCustomerDto
    {
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }        
        public CustomerAddressDto DeliveryAddress { get; set; }
    }
}
