using CoffeStore.Common.Seedwork;
using CoffeStore.Modules.Orders.Domain.Enums;

namespace CoffeStore.Modules.Orders.Domain
{
    internal sealed class Order: Entity
    {
        public Guid CustomerId { get; }
        public DeliveryAddress CustomerAddress { get; }               
        public decimal Amount => OrderItems.Sum(i => i.Price);
        public DeliveryStatusEnum DeliveryStatus { get; private set; }
        public DateTime CreatedAt { get; set; }

        private List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems { get; }

        public Order(DeliveryAddress customerAddress, Guid customerId)
        {
            CustomerAddress = customerAddress;
            OrderItems = new List<OrderItem>();
            DeliveryStatus = DeliveryStatusEnum.New;
            CustomerId = customerId;
            CreatedAt = DateTime.Now;
            _orderItems = new List<OrderItem>();
        }

        public void AddOrderItems(IEnumerable<OrderItem> orderItems)
        {
            _orderItems.AddRange(orderItems);
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
