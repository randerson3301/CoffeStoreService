using CoffeStoreService.API.Models.CustomerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStoreService.Tests.ModelTests
{
    internal class CustomerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Add_DeliveryAddress_ReturnsSameAddress()
        {
            var customer = new Customer("Test", DateOnly.MinValue, "1234");
            var newAddress = new DeliveryAddress("123", "Rua Test", 123, "bloco 8", "Vila Test", "Test City", "SP");

            customer.AddAddress(newAddress);

            Assert.IsTrue(customer.DeliveryAddress.Contains(newAddress));
        }
    }        
}
