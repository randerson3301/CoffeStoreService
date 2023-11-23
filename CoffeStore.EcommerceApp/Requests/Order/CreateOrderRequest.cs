using CoffeStore.EcommerceApp.Requests.Customer.Dtos;

namespace CoffeStore.EcommerceApp.Requests.Order
{
    public class CreateOrderRequest
    {
        public List<OrderItemDto> OrderItems { get; set; }
        public decimal Total { get; set; }
        public string CustomerId { get; set; }
        public CustomerAddressDto addressToDeliver { get; set; }
    }
}
