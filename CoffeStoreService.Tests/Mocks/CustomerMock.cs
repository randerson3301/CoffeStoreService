using CoffeStore.Modules.Customers.Application.Commands;
using CoffeStore.Modules.Customers.Application.ViewModels;
using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Customers.Seedwork;

namespace CoffeStoreService.Tests.Mocks
{
    internal static class CustomerMock
    {
        public static Customer GetCustomer()
        {
            return new Customer("Test", DateOnly.MinValue, "1234", "teste@teste.com");
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


        //public static CreateCustomerRequest GetDto()
        //{
        //    return new CreateCustomerRequest()
        //    {
        //        Name = "John Doe",
        //        BirthDate = new DateOnly(2000, 1, 1),
        //        Document = "70152246070",
        //        Email = "john.doe@example.com",
        //        Password = "Password123",               
        //        Id = Guid.NewGuid(),
        //    };
        //}

        //public static CreateCustomerRequest GetDtoWithAddress()
        //{
        //    return new CreateCustomerRequest()
        //    {
        //        Name = "John Doe",
        //        BirthDate = new DateOnly(2000, 1, 1),
        //        Document = "70152246070",
        //        Email = "john.doe@example.com",
        //        Password = "Password123",
        //        DeliveryAddress = new CustomerAddressDto
        //        {
        //            ZipCode = "12345678",
        //            Address = "123 Main St",
        //            Number = 123,
        //            Neighborhood = "Suburb",
        //            City = "City",
        //            State = "ST",
        //        },
        //        Id = Guid.NewGuid(),
        //    };
        //}

        public static DeliveryAddress GetCustomerAddress()
        {
            return new DeliveryAddress("12345678", "123 Main St", 123, "", "Suburb", "City", "ST");
        }

        public static CreateCustomerCommand GetCommand()
        {
            return new CreateCustomerCommand()
            {
                Name = "John Doe",
                BirthDate = new DateOnly(2000, 1, 1),
                Document = "70152246070",
                Email = "john.doe@example.com",
                Password = "Password123",
                DeliveryAddress = GetCustomerAddress(),
            };
        }
    }
}
