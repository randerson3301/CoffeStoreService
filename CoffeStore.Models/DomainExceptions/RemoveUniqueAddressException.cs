using CoffeStore.Models.Resources;

namespace CoffeStore.Models.DomainExceptions
{
    public sealed class RemoveUniqueAddressException : Exception
    {
        public RemoveUniqueAddressException() : base(ErrorMessages.CANNOT_REMOVE_ADDRESS) { }
    }
}
