using CoffeStore.Models.Resources;

namespace CoffeStore.Models.DomainExceptions
{
    public class NotAdminException : Exception
    {
        public NotAdminException() : base(ErrorMessages.NOT_ADMIN_MESSAGE) { }
    }
}