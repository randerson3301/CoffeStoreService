using CoffeStore.Models.Resources;

namespace CoffeStore.Models.DomainExceptions
{
    public sealed class ReviewUnavailableProductException : Exception
    {
        public ReviewUnavailableProductException() : base(ErrorMessages.CANNOT_REVIEW_UNAVAILABLE_PRODUCT) { }
    }
}
