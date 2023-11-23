using CoffeStore.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using CoffeStore.Models.Aggregates.CustomerAggregate;

namespace CoffeStore.Models.Aggregates.OrderAggregate
{
    public sealed class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public DeliveryAddress CustomerAddress;
        public ICollection<OrderItem> OrderItems { get; private set; }
        public decimal Amount { get; set; }
        public DeliveryStatusEnum DeliveryStatus { get; private set; }
        public DateTime CreatedAt { get; set; }

        public Order(DeliveryAddress customerAddress, string customerId)
        {
            CustomerAddress = customerAddress;
            OrderItems = new List<OrderItem>();
            DeliveryStatus = DeliveryStatusEnum.New;
            CustomerId = customerId;
            CreatedAt = DateTime.Now;
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            OrderItems.Add(orderItem);
        }

        public void RemoveOrderItem(OrderItem orderItem)
        {
            OrderItems.Remove(orderItem);
        }

        public void ChangeDeliveryStatus(DeliveryStatusEnum newStatus)
        {
            DeliveryStatus = newStatus;
        }
    }
}