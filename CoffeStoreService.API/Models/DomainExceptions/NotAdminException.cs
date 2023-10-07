using CoffeStoreService.API.Resources;

namespace CoffeStoreService.API.Models.DomainExceptions
{
    public class NotAdminException: Exception
    {
        public NotAdminException(): base(ErrorMessages.NOT_ADMIN_MESSAGE) { }
    }
}