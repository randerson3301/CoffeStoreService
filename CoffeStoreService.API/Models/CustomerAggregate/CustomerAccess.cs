namespace CoffeStoreService.API.Models.CustomerAggregate
{
    public sealed class CustomerAccess
    {
        public string Email { get; }
        public string Password { get; }
        public bool IsActive => _isActive;

        private bool _isActive;

        public CustomerAccess(string email, string password)
        {
            Email = email;
            Password = password;
            _isActive = true;
        }

        public void DeactivateAccess() => _isActive = false;
        
    }
}