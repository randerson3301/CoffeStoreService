using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeStore.Modules.Customers.Seedwork
{
    public struct DeliveryAddress
    {
        public DeliveryAddress(string zipCode, string address, int number, string? complement, string neighborhood, string city, string state)
        {
            ZipCode = zipCode;
            Address = address;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
        }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public string? Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
