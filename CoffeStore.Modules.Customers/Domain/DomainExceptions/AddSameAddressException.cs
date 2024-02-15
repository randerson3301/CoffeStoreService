using CoffeStore.Modules.Customers.Resources;

namespace CoffeStore.Modules.Customers.Domain.DomainExceptions
{
    internal sealed class AddSameAddressException : InvalidOperationException
    {
        public AddSameAddressException() : base(ErrorMessages.CANNOT_ADD_SAME_ADDRESS) { }
    }
}
