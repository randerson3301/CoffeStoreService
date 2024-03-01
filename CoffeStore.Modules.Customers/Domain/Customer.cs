using CoffeStore.Common.Seedwork;
using CoffeStore.Modules.Customers.Domain.DomainEvents;
using CoffeStore.Modules.Customers.Domain.DomainExceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeStore.Modules.Customers.Domain
{
    internal sealed class Customer: Entity
    {
        private readonly ICollection<CustomerAddress> _deliveryAddresses;
       
        public string FullName { get; }
        public DateOnly BirthDate { get; }
        public string Document { get; }
        public string Email { get; }

        [NotMapped]
        public IReadOnlyCollection<CustomerAddress> DeliveryAddresses => _deliveryAddresses.ToList().AsReadOnly();
      
        public Customer(string fullName, DateOnly birthDate, string document, string email)
        {
            FullName = fullName;
            BirthDate = birthDate;
            Document = document;
            Email = email;

            _deliveryAddresses = new List<CustomerAddress>();
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

        public void AddAccess(string password)
        {
            AddDomainEvent(new CustomerAccessCreatedDomainEvent(Id, Email, password));
        }
    }

}
