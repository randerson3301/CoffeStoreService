using CoffeStore.Models.DomainExceptions;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CoffeStore.Models.Aggregates.CustomerAggregate
{
    public sealed class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string FullName { get;  set; }
        public DateTime BirthDate { get;  set; }
        public string Document { get;  set; }
        //public IReadOnlyCollection<DeliveryAddress> DeliveryAddress => _deliveryAddresses.AsReadOnly();
        public ICollection<DeliveryAddress> DeliveryAddress { get; set; }
        public CustomerAccess CustomerAccess { get; set; }


        public Customer(string fullName, DateTime birthDate, string document, CustomerAccess customerAccess)
        {
            FullName = fullName;
            BirthDate = birthDate;
            Document = document;
            CustomerAccess = customerAccess;
            DeliveryAddress = new List<DeliveryAddress>();
        }


        public void AddAddress(DeliveryAddress newAddress)
        {
            DeliveryAddress.Add(newAddress);
        }

        public void RemoveAddress(DeliveryAddress addressToRemove)
        {
            if (DeliveryAddress.HasSingleElement())
            {
                throw new RemoveUniqueAddressException();
            }

            DeliveryAddress.Remove(addressToRemove);
        }       
    }
}
