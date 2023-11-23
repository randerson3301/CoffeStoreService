using CoffeStore.EcommerceApp.Adapters.Contracts;
using CoffeStore.EcommerceApp.Requests.Order;
using CoffeStore.Models.Aggregates.OrderAggregate;

namespace CoffeStore.EcommerceApp.Adapters
{
    public class OrderAdapter : IOrderAdapter
    {
        public ICustomerAdapter _adapter;
        public OrderAdapter(ICustomerAdapter customerAdapter)
        {
            _adapter = customerAdapter;
        }

        public Order ConvertToDomain(CreateOrderRequest request)
        {
            var order = new Order(_adapter.ConvertToDomainAddress(request.addressToDeliver), request.CustomerId);
            order.Amount = request.Total;
            foreach( var item in request.OrderItems)
            {
                order.AddOrderItem(new OrderItem(new ProductItem(item.Product.Id, item.Product.Price, item.Product.ImagePath, item.Product.Title), item.Quantity, item.SubTotal));
            }
            

            return order;
        }
    }
}
