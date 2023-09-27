using CoffeStoreService.API.Resources;
using System.Collections.ObjectModel;

namespace CoffeStoreService.API.Models.CustomerAggregate
{
    public class Customer
    {
        private readonly Guid _id; 
        public Guid Id => _id;

        public string FullName { get; }
        public DateOnly BirthDate { get; }
        public string Document { get; }
        public ReadOnlyCollection<DeliveryAddress> DeliveryAddress => _deliveryAddresses.AsReadOnly();

        private readonly List<DeliveryAddress> _deliveryAddresses;

        public Customer(string fullName, DateOnly birthDate, string document)
        {
            FullName = fullName;
            BirthDate = birthDate;
            Document = document;
            _deliveryAddresses = new List<DeliveryAddress>();
            _id = Guid.NewGuid();
        }


        public void AddAddress(DeliveryAddress newAddress)
        {
            _deliveryAddresses.Add(newAddress);
        }

        public void RemoveAddress(DeliveryAddress addressToRemove)
        {            
            if (_deliveryAddresses.HasSingleElement())
            {
                throw new Exception(ErrorMessages.CANNOT_REMOVE_ADDRESS);
            }

            _deliveryAddresses.Remove(addressToRemove);
        }
    }
}
