using CoffeStore.EcommerceApp.Requests.Customer.Dtos;

namespace CoffeStore.EcommerceApp.ViewModels
{
    public class CustomerViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public IEnumerable<CustomerAddressDto> Addresses { get; set; }
    }
}
