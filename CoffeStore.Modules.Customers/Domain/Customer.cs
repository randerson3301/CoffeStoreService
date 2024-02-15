using CoffeStore.Modules.Customers.Domain.DomainExceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeStore.Modules.Customers.Domain
{
    internal sealed class Customer
    {
        private readonly Guid _id;
        private readonly ICollection<CustomerAddress> _deliveryAddresses;

        public Guid Id => _id;
        public string FullName { get; }
        public DateOnly BirthDate { get; }
        public string Document { get; }
        [NotMapped]
        public IReadOnlyCollection<CustomerAddress> DeliveryAddresses => _deliveryAddresses.ToList().AsReadOnly();
        public string Email { get; }

        public Customer(string fullName, DateOnly birthDate, string document, string email, Guid id = default)
        {
            FullName = fullName;
            BirthDate = birthDate;
            Document = document;
            Email = email;

            _deliveryAddresses = new List<CustomerAddress>();
            _id = (id == Guid.Empty) ? Guid.NewGuid() : id;
        }


        public void AddAddress(CustomerAddress newAddress)
        {
            if (_deliveryAddresses.Contains(newAddress))
            {
                throw new AddSameAddressException();
            }

            _deliveryAddresses.Add(newAddress);
        }

        public void RemoveAddress(CustomerAddress addressToRemove)
        {
            if (_deliveryAddresses.Count == 1)
            {
                throw new RemoveUniqueAddressException();
            }

            _deliveryAddresses.Remove(addressToRemove);
        }
    }

}
