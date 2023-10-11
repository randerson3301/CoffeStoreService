using CoffeStoreService.API.Models.Enums;

namespace CoffeStoreService.API.Models.Aggregates.OrderAggregate
{
    public sealed class Order
    {
        private readonly Guid _id;
        private readonly List<OrderItem> _orderItems;

        public Guid Id => _id;
        public CustomerAddress CustomerAddress;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public decimal Amount => _orderItems.Sum(item => item.Subtotal);
        public DeliveryStatusEnum DeliveryStatus { get; private set; } 

        public Order(CustomerAddress customerAddress)
        {
            CustomerAddress = customerAddress;
            _orderItems = new List<OrderItem>();
            _id = Guid.NewGuid();
            DeliveryStatus = DeliveryStatusEnum.New;
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            _orderItems.Add(orderItem);
        }

        public void RemoveOrderItem(OrderItem orderItem)
        {
            _orderItems.Remove(orderItem);
        }

        public void ChangeDeliveryStatus(DeliveryStatusEnum newStatus)
        {
            DeliveryStatus = newStatus;
        }
    }
}