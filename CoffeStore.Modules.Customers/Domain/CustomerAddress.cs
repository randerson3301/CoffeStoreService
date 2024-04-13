using CoffeStore.Common.Seedwork;
using System;

namespace CoffeStore.Modules.Customers.Domain
{
    internal sealed record CustomerAddress
    {
        public void Set(Guid customerId, DeliveryAddress deliveryAddress)
        {
            CustomerId = customerId;
            ZipCode = deliveryAddress.ZipCode;
            Address = deliveryAddress.Address;
            Number = deliveryAddress.Number;
            Complement = deliveryAddress.Complement;
            Neighborhood = deliveryAddress.Neighborhood;
            City = deliveryAddress.City;
            State = deliveryAddress.State;
        }

        public Guid CustomerId { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public string? Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
