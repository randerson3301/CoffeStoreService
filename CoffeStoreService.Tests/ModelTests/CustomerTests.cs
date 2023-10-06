using CoffeStoreService.API.Models.Aggregates.CustomerAggregate;
using CoffeStoreService.API.Models.DomainExceptions;

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

            customer.AddAddress(newAddress);

            Assert.IsTrue(customer.DeliveryAddress.Contains(newAddress));
        }

        [Test]
        public void Remove_DeliveryAddress_CustomerAddressesDoNotContainsTheAddress()
        {
            var customer = CustomerMock.GetCustomer();

            var addressToRemove = new DeliveryAddress("123", "Rua Test", 123, "bloco 8", "Vila Test", "Test City", "SP");
            var addressToRemove2 = new DeliveryAddress("123", "Rua Test22", 123, "bloco 8", "Vila Test", "Test City", "SP");

            customer.AddAddress(addressToRemove);
            customer.AddAddress(addressToRemove2);

            customer.RemoveAddress(addressToRemove2);

            Assert.IsFalse(customer.DeliveryAddress.Contains(addressToRemove2));
        }

        [Test]
        public void Remove_DeliveryAddress_ThrowsErrorIfCustomerAddressesIsEmpty()
        {
            var customer = CustomerMock.GetCustomer();

            var addressToRemove = new DeliveryAddress("123", "Rua Test", 123, "bloco 8", "Vila Test", "Test City", "SP");

            var expectedExceptionMessage = "Could not remove the address since customer should have at least one delivery address";

            customer.AddAddress(addressToRemove);

            string? actualExceptionMessage = Assert.Throws<RemoveUniqueAddressException>(() =>
            {
                customer.RemoveAddress(addressToRemove);
            }).Message;

            Assert.AreEqual(expectedExceptionMessage, actualExceptionMessage);
        }



        [Test]
        public void Update_DeliveryAddress_CustomerAddressesContainsUpdatedAddress()
        {
            var customer = CustomerMock.GetCustomer();

            var oldAddress = new DeliveryAddress("123", "Rua Test", 123, "bloco 8", "Vila Test", "Test City", "SP");
            var newAddress = new DeliveryAddress(oldAddress.ZipCode, oldAddress.Address, 55, "bloco 9", oldAddress.Neighborhood, oldAddress.City, oldAddress.State);
            
            customer.AddAddress(oldAddress);
            customer.UpdateAddress(newAddress);

            Assert.IsTrue(customer.DeliveryAddress.Contains(newAddress));
            Assert.IsFalse(customer.DeliveryAddress.Contains(oldAddress));
        }

        [Test]
        public void Add_NewCustomer_MustHaveCredentialAccess()
        {
            var customer = CustomerMock.GetCustomer();

            Assert.IsNotNull(customer.CustomerAccess);
        }

        [Test]
        public void Disable_CustomerAccess_ReturnsFalseForActivation()
        {
            var customer = CustomerMock.GetCustomer();

            customer.CustomerAccess.DisableAccess();

            Assert.IsFalse(customer.CustomerAccess.IsActive);
        }
    }        
}
