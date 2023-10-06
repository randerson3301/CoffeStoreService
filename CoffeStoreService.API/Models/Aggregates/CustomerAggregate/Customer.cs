using CoffeStoreService.API.Models.DomainExceptions;

namespace CoffeStoreService.API.Models.Aggregates.CustomerAggregate
{
    public sealed class Customer
    {
        private readonly Guid _id;
        private readonly List<DeliveryAddress> _deliveryAddresses;

        public Guid Id => _id;
        public string FullName { get; }
        public DateOnly BirthDate { get; }
        public string Document { get; }
        public IReadOnlyCollection<DeliveryAddress> DeliveryAddress => _deliveryAddresses.AsReadOnly();
        public CustomerAccess CustomerAccess { get; set; }


        public Customer(string fullName, DateOnly birthDate, string document, CustomerAccess customerAccess)
        {
            FullName = fullName;
            BirthDate = birthDate;
            Document = document;
            CustomerAccess = customerAccess;
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
                throw new RemoveUniqueAddressException();
            }

            _deliveryAddresses.Remove(addressToRemove);
        }

        public void UpdateAddress(DeliveryAddress newAddress)
        {
            var existingAddress = _deliveryAddresses.Find(a => a.ZipCode == newAddress.ZipCode);

            if(!existingAddress.Equals(null))
            {
                AddAddress(newAddress);
                RemoveAddress(existingAddress);
            }
        }        
    }
}
