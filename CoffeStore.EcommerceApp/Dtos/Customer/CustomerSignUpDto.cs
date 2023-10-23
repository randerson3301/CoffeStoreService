namespace CoffeStore.EcommerceApp.Dtos
{
    public class CustomerSignUpDto: BaseCustomerDto
    {                      
        public string Password { get; set; } = string.Empty;
        public CustomerAddressDto DeliveryAddress { get; set; }
        
    }
}