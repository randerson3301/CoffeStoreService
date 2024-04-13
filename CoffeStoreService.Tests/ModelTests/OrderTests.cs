using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Orders.Domain;
using CoffeStore.Modules.Orders.Domain.Enums;
using CoffeStoreService.Tests.Mocks;
using NUnit.Framework;
using System.Linq;

namespace CoffeStoreService.Tests.ModelTests
{
    internal class OrderTests
    {
        [Test]
        public void Add_OrderItem_OrderItemsConstainsSameItem()
        {
            var customer = CustomerMock.GetCustomer();
            var deliveryAddress = CustomerMock.GetCustomerAddress();

            decimal price = 2.5m;
            var orderItems = new List<OrderItem>();
            orderItems.Add(new OrderItem(Guid.NewGuid(), price, 1));

            var order = new Order(deliveryAddress, customer.Id);

            order.AddOrderItems(orderItems);

            Assert.IsTrue(order.OrderItems.AsEnumerable().ContainsRange(orderItems));
        }

        [Test]
        public void Add_OrderItem_IncrementQuantity_SubtotalShouldBeMultiplied()
        {
            var customer = CustomerMock.GetCustomer();
            var deliveryAddress = CustomerMock.GetCustomerAddress();
            decimal price = 2.5m;
            var orderItems = new List<OrderItem>();
            var orderItem = new OrderItem(Guid.NewGuid(), price, 1);
            var order = new Order(deliveryAddress, customer.Id);

            orderItems.Add(orderItem);

            orderItem.Quantity = 2;

            order.AddOrderItems(orderItems);

            var expectedAmount = price * orderItem.Quantity;

            Assert.AreEqual(expectedAmount, orderItem.Subtotal);
        }

        [Test]
        public void Add_OrderItems_OrderTotalAmountEqualsItemsSubtotalSum()
        {
            var customer = CustomerMock.GetCustomer();
            var deliveryAddress = CustomerMock.GetCustomerAddress();
            decimal price = 2.5m;
            decimal price2 = 3m;
            var orderItem = new OrderItem(Guid.NewGuid(), price, 2);
            var orderItem2 = new OrderItem(Guid.NewGuid(), price2, 3);
            var orderItems = new List<OrderItem>();
            var order = new Order(deliveryAddress, customer.Id);

            orderItems.Add(orderItem);
            orderItems.Add(orderItem2);

            order.AddOrderItems(orderItems);

            var expectedAmount = orderItem.Subtotal + orderItem2.Subtotal;

            Assert.AreEqual(expectedAmount, order.Amount);
        }

        [Test]
        public void Remove_OrderItem_DecreaseQuantity_SubtotalShouldBeDecreased()
        {
            var customer = CustomerMock.GetCustomer();
            var deliveryAddress = CustomerMock.GetCustomerAddress();
            decimal price = 2.5m;
            int quantity = 2;
            var orderItem = new OrderItem(Guid.NewGuid(), price, quantity);
            var orderItems = new List<OrderItem>();
            var order = new Order(deliveryAddress, customer.Id);

            var firstAmount = orderItem.Subtotal;

            orderItem.Quantity--;
            orderItems.Add(orderItem);

            order.AddOrderItems(orderItems);

            var expectedAmount = price * orderItem.Quantity;

            Assert.AreEqual(expectedAmount, orderItem.Subtotal);
            Assert.IsTrue(expectedAmount < firstAmount);
        }

        [Test]
        public void Remove_OrderItems_OrderTotalAmountEqualsItemsSubtotalSum()
        {
            var customer = CustomerMock.GetCustomer();
            var deliveryAddress = CustomerMock.GetCustomerAddress();
            decimal price = 2.5m;
            decimal price2 = 3m;
            var orderItem = new OrderItem(Guid.NewGuid(), price, 2);
            var orderItem2 = new OrderItem(Guid.NewGuid(), price2, 3);
            var orderItems = new List<OrderItem>();
            var order = new Order(deliveryAddress, customer.Id);
            
            orderItems.Add(orderItem);
            orderItems.Add(orderItem2);

            order.AddOrderItems(orderItems);

            order.RemoveOrderItem(orderItem);

            Assert.AreEqual(orderItem2.Subtotal, order.Amount);
        }

        [Test]
        public void Add_Order_ShouldHaveADeliveryStatusOfNew()
        {
            var customer = CustomerMock.GetCustomer();
            var deliveryAddress = CustomerMock.GetCustomerAddress();
            decimal price = 2.5m;
            var orderItem = new OrderItem(Guid.NewGuid(), price, 2);
            var orderItems = new List<OrderItem>();
            var order = new Order(deliveryAddress, customer.Id);

            orderItems.Add(orderItem);

            order.AddOrderItems(orderItems);

            Assert.AreEqual(DeliveryStatusEnum.New, order.DeliveryStatus);
        }

        [TestCase(DeliveryStatusEnum.InTransit)]
        [TestCase(DeliveryStatusEnum.Delivered)]
        [TestCase(DeliveryStatusEnum.Canceled)]
        public void ChangeDeliveryStatus_ModifiesDeliveryStatus(DeliveryStatusEnum newStatus)
        {
            var customer = CustomerMock.GetCustomer();
            var deliveryAddress = CustomerMock.GetCustomerAddress();

            var order = new Order(deliveryAddress, customer.Id);

            order.ChangeDeliveryStatus(newStatus);

            Assert.AreEqual(newStatus, order.DeliveryStatus);
        }
    }
}
