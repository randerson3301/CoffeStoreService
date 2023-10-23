using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.ViewModels.Customer;

namespace CoffeStoreService.Tests.Mocks
{
    internal static class CustomerMock
    {
        public static Customer GetCustomer()
        {
            var customerAccess = new CustomerAccess("teste@teste.com", "senha123");
            return new Customer("Test", DateOnly.MinValue, "1234", customerAccess);
        }

        public static CustomerViewModel GetCustomerViewModel()
        {
            return new CustomerViewModel()
            {
                Name = "John Doe",
                BirthDate = new DateOnly(2000, 1, 1),
                Document = "70152246070",
                Email = "john.doe@example.com",
            };
        }


        public static CustomerDto GetDto()
        {
            return new CustomerDto()
            {
                Name = "John Doe",
                BirthDate = new DateOnly(2000, 1, 1),
                Document = "70152246070",
                Email = "john.doe@example.com",
                Password = "Password123",               
                Id = Guid.NewGuid(),
            };
        }

        public static CustomerDto GetDtoWithAddress()
        {
            return new CustomerDto()
            {
                Name = "John Doe",
                BirthDate = new DateOnly(2000, 1, 1),
                Document = "70152246070",
                Email = "john.doe@example.com",
                Password = "Password123",
                DeliveryAddress = new CustomerAddressDto
                {
                    ZipCode = "12345678",
                    Address = "123 Main St",
                    Number = 123,
                    Neighborhood = "Suburb",
                    City = "City",
                    State = "ST",
                },
                Id = Guid.NewGuid(),
            };
        }

        public static DeliveryAddress GetCustomerAddress() {
            return new DeliveryAddress("12345678", "123 Main St", 123, "", "Suburb", "City", "ST");            
        }
    }
}
