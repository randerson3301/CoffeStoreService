using CoffeStoreService.API.Models.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStoreService.Tests.ModelTests
{
    internal class OrderTests
    {
        [Test]
        public void Add_OrderItem_OrderItemsConstainsSameItem()
        {
            var customer = CustomerMock.GetCustomer();
            var deliveryAddress = customer.DeliveryAddress.FirstOrDefault();
            
            var customerAddress = new CustomerAddress(customer.Id, deliveryAddress);

            decimal price = 2.5m;
            var productPrice = new ProductPrice(Guid.NewGuid(), price);
            var orderItem = new OrderItem(productPrice, 1);

            var order = new Order(customerAddress);

            order.AddOrderItem(orderItem);

            Assert.IsTrue(order.OrderItems.Contains(orderItem));
        }

        [Test]
        public void Add_OrderItem_IncrementQuantity_SubtotalShouldBeMultiplied()
        {
            var customer = CustomerMock.GetCustomer();
            var deliveryAddress = customer.DeliveryAddress.FirstOrDefault();

            var customerAddress = new CustomerAddress(customer.Id, deliveryAddress);

            decimal price = 2.5m;
            var productPrice = new ProductPrice(Guid.NewGuid(), price);            
            int quantity = 1;

            var orderItem = new OrderItem(productPrice, quantity);

            var order = new Order(customerAddress);

            orderItem.RaiseQuantity();

            order.AddOrderItem(orderItem);

            var expectedAmount = price * orderItem.Quantity;

            Assert.AreEqual(expectedAmount, orderItem.Subtotal);
        }

        [Test]
        public void Add_OrderItems_OrderTotalAmountEqualsItemsSubtotalSum()
        {
            var customer = CustomerMock.GetCustomer();
            var deliveryAddress = customer.DeliveryAddress.FirstOrDefault();

            var customerAddress = new CustomerAddress(customer.Id, deliveryAddress);

            decimal price = 2.5m;
            decimal price2 = 3m;

            var orderItem = new OrderItem(new ProductPrice(Guid.NewGuid(), price), 2);
            var orderItem2 = new OrderItem(new ProductPrice(Guid.NewGuid(), price2), 3);

            var order = new Order(customerAddress);

            order.AddOrderItem(orderItem);
            order.AddOrderItem(orderItem2);

            var expectedAmount = orderItem.Subtotal + orderItem2.Subtotal;

            Assert.AreEqual(expectedAmount, order.Amount);
        }
    }
}
