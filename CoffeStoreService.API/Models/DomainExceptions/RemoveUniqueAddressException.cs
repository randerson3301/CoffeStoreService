using CoffeStoreService.API.Resources;

namespace CoffeStoreService.API.Models.DomainExceptions
{
    public sealed class RemoveUniqueAddressException : Exception
    {
        public RemoveUniqueAddressException() : base(ErrorMessages.CANNOT_REMOVE_ADDRESS) { }
    }
}
