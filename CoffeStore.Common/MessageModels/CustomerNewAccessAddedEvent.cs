namespace CoffeStore.Common.MessageModels
{
    public sealed record CustomerNewAccessAddedEvent(Guid CustomerId, string Email, string Password)
    {
    }
}
