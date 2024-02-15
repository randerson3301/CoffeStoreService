using CoffeStore.Modules.Customers.Resources;

namespace CoffeStore.Modules.Customers.Domain.DomainExceptions
{
    internal sealed class RemoveUniqueAddressException : InvalidOperationException
    {
        public RemoveUniqueAddressException() : base(ErrorMessages.CANNOT_REMOVE_ADDRESS) { }
    }
}
