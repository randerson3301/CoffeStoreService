using CoffeStoreService.API.Resources;

namespace CoffeStoreService.API.Models.DomainExceptions
{
    public sealed class ReviewUnavailableProductException : Exception
    {
        public ReviewUnavailableProductException() : base(ErrorMessages.CANNOT_REVIEW_UNAVAILABLE_PRODUCT) { }
    }
}
