namespace CoffeStoreService.API.Models.CustomerAggregate
{
    public class DeliveryAddress
    {
        public DeliveryAddress(string zipCode, string address, int number, string? complement, string neighborhood, string city, string state)
        {
            ZipCode = zipCode ?? throw new ArgumentNullException(nameof(zipCode));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood ?? throw new ArgumentNullException(nameof(neighborhood));
            City = city ?? throw new ArgumentNullException(nameof(city));
            State = state ?? throw new ArgumentNullException(nameof(state));
        }
        public string ZipCode {get;}
        public string Address {get;}
        public int Number { get; set; }
        public string? Complement { get; set; }
        public string Neighborhood {get;}
        public string City {get;}  
        public string State {get;}  
        

        
        
    }
}
