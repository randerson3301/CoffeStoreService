namespace CoffeStore.EcommerceApp.Requests.Customer.Dtos
{
    public class CustomerAddressDto
    {
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public string? Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
