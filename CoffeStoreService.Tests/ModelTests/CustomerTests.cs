using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Customers.Domain.DomainExceptions;
using CoffeStore.Modules.Customers.Seedwork;
using CoffeStoreService.Tests.Mocks;
using NUnit.Framework;

namespace CoffeStoreService.Tests.ModelTests
{
    internal class CustomerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Add_DeliveryAddress_CustomerAddressesContainsSameAddress()
        {
            var customer = CustomerMock.GetCustomer();
            var newAddress = new DeliveryAddress("123", "Rua Test", 123, "bloco 8", "Vila Test", "Test City", "SP");
            var customerAddress = new CustomerAddress();

            customerAddress.Set(customer.Id, newAddress);

            customer.TryAddAddress(customerAddress);

            Assert.IsTrue(customer.DeliveryAddresses.Any());
        }

        [Test]
        public void Remove_DeliveryAddress_CustomerAddressesDoNotContainsTheAddress()
        {
            var customer = CustomerMock.GetCustomer();

            var addressToRemove = new CustomerAddress();
            var addressToRemove2 = new CustomerAddress();

            addressToRemove.Set(customer.Id, new DeliveryAddress("123", "Rua Test", 123, "bloco 8", "Vila Test", "Test City", "SP"));
            addressToRemove2.Set(customer.Id, new DeliveryAddress("123", "Rua Test22", 123, "bloco 8", "Vila Test", "Test City", "SP"));

            customer.TryAddAddress(addressToRemove);
            customer.TryAddAddress(addressToRemove2);

            customer.TryRemoveAddress(addressToRemove2);

            Assert.IsFalse(customer.DeliveryAddresses.Contains(addressToRemove2));
        }

        [Test]
        public void Remove_DeliveryAddress_ThrowsErrorIfCustomerAddressesIsEmpty()
        {
            var customer = CustomerMock.GetCustomer();

            var addressToRemove = new CustomerAddress();
            addressToRemove.Set(customer.Id, new DeliveryAddress("123", "Rua Test", 123, "bloco 8", "Vila Test", "Test City", "SP"));

            customer.TryAddAddress(addressToRemove);
           
            Assert.IsFalse(customer.TryRemoveAddress(addressToRemove));
        }                       
    }        
}
