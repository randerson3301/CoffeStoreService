namespace CoffeStore.EcommerceApp.Dtos
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Document { get; set; }                
        public CustomerLoginDto Login { get; set; }
        public CustomerAddressDto DeliveryAddress { get; set; }
    }
}
