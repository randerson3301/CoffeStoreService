using CoffeStoreService.API.Models.CustomerAggregate;
using System.Collections.ObjectModel;

namespace CoffeStoreService.API.Models.OrderAggregate
{
    public sealed class Order
    {
        private readonly Guid _id;
        private readonly List<OrderItem> _orderItems;

        public Guid Id => _id;
        public CustomerAddress CustomerAddress;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public decimal Amount => _orderItems.Sum(item => item.Subtotal);

        public Order(CustomerAddress customerAddress)
        {
            CustomerAddress = customerAddress;
            _orderItems = new List<OrderItem>();
            _id = Guid.NewGuid();
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            _orderItems.Add(orderItem);
        }
    }
}