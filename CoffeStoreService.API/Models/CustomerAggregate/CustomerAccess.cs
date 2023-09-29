namespace CoffeStoreService.API.Models.CustomerAggregate
{
    public class CustomerAccess
    {
        public string Email { get; }
        public string Password { get; }

        public CustomerAccess(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}