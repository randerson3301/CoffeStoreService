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


        public bool TryAddAddress(CustomerAddress newAddress)
        {
            if (_deliveryAddresses.Contains(newAddress))
            {
                return false;
            }

            _deliveryAddresses.Add(newAddress);
            return true;
        }

        public bool TryRemoveAddress(CustomerAddress addressToRemove)
        {
            if (_deliveryAddresses.Count == 1)
            {
                return false;
            }

            _deliveryAddresses.Remove(addressToRemove);
            return true;
        }
    }

}
