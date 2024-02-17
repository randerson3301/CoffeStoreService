using Azure.Core;
using CoffeStore.Modules.Customers.Application.Commands;
using CoffeStore.Modules.Customers.Application.ViewModels;
using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Customers.Seedwork;
using Isopoh.Cryptography.Argon2;
using System.Net;
using System.Reflection.Emit;

namespace CoffeStore.Modules.Customers.Application.Adapters
{
    internal class CustomerAdapter : ICustomerAdapter
    {
        public Customer ConvertToDomain(CreateCustomerCommand request)
        {
            var domain = new Customer(request.Name, request.BirthDate, request.Document, request.Email);
            var newAddress = request.DeliveryAddress;

            domain.TryAddAddress(ConvertToDomain(domain.Id, newAddress));

            return domain;
        }

        public CustomerAddress ConvertToDomain(DeleteCustomerAddressCommand request)
        {
            var address = new CustomerAddress();
            address.Set(request.Id, request.AddressToRemove);
            return address;
        }

        public CustomerAddress ConvertToDomain(Guid customerId, DeliveryAddress address)
        {
            var customerAddress = new CustomerAddress();
            customerAddress.Set(customerId, address);
            return customerAddress;
        }

        public CustomerViewModel ConvertToViewModel(Customer domain)
        {
            var viewModel = new CustomerViewModel()
            {
                Id = domain.Id,
                BirthDate = domain.BirthDate,
                Document = domain.Document,
                Email = domain.Email,
                Name = domain.FullName,
                Addresses = domain.DeliveryAddresses.Select(a => new DeliveryAddress(a.ZipCode, a.Address, a.Number, a.Complement, a.Neighborhood, a.City, a.State)).AsEnumerable()
            };

            return viewModel;
        }
    }
}
