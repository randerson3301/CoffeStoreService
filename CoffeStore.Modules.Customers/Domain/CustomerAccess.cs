using Microsoft.EntityFrameworkCore;

namespace CoffeStore.Modules.Customers.Domain
{
    internal sealed class CustomerAccess
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

        public void DisableAccess() => _isActive = false;

    }

}
