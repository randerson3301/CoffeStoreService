using CoffeStore.Modules.Customers.Seedwork;

namespace CoffeStore.Modules.Customers.Infra.PersistenceModels
{
    internal class CustomerAddressEntity
    {
        public void Set(Guid id, DeliveryAddress newAddress)
        {
            CustomerId = id;
            Address = newAddress.Address;
            City = newAddress.City;
            Complement = newAddress.Complement;
            Neighborhood = newAddress.Neighborhood;
            Number = newAddress.Number;
            State = newAddress.State;
            ZipCode = newAddress.ZipCode;
        }

        public Guid CustomerId { get; set; }
        public CustomerEntity? Customer { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public string? Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}

