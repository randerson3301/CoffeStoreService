using CoffeStore.Common.MessageModels;
using CoffeStore.Modules.Customers.Application.Commands;
using CoffeStore.Modules.Customers.Application.ViewModels;
using CoffeStore.Modules.Customers.Domain;
using CoffeStore.Modules.Customers.Domain.DomainEvents;
using CoffeStore.Common.Seedwork;
using Isopoh.Cryptography.Argon2;

namespace CoffeStore.Modules.Customers.Application.Adapters
{
    internal class CustomerAdapter : ICustomerAdapter
    {
        public Customer ConvertToDomain(CreateCustomerCommand request)
        {
            var domain = new Customer(request.Name, request.BirthDate, request.Document, request.Email);
            var newAddress = request.DeliveryAddress;

            domain.TryAddAddress(ConvertToDomain(domain.Id, newAddress));
            domain.AddAccess(Argon2.Hash(request.Password));

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

        public CustomerNewAccessAddedEvent ConvertToIntegrationEvent(CustomerAccessCreatedDomainEvent domainEvent)
        {
            return new CustomerNewAccessAddedEvent(domainEvent.CustomerId, domainEvent.Email, domainEvent.Password);
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
