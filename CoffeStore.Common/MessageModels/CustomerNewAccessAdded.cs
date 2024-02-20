namespace CoffeStore.Common.MessageModels
{
    public sealed record CustomerNewAccessAdded(Guid CustomerId, string Email, string Password)
    {
    }
}
