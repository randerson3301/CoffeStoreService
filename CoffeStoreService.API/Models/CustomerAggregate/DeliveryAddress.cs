namespace CoffeStoreService.API.Models.CustomerAggregate
{
    public class DeliveryAddress
    {
        public DeliveryAddress(string cep, string address, int number, string? complement, string neighborhood, string city, string state)
        {
            Cep = cep ?? throw new ArgumentNullException(nameof(cep));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood ?? throw new ArgumentNullException(nameof(neighborhood));
            City = city ?? throw new ArgumentNullException(nameof(city));
            State = state ?? throw new ArgumentNullException(nameof(state));
            Id = Guid.NewGuid();    
        }
        public Guid Id { get;}
        public string Cep {get;}
        public string Address {get; }
        public int Number {get;}
        public string? Complement {get;}
        public string Neighborhood {get;}
        public string City {get;}  
        public string State {get;}  
        

        
        
    }
}
