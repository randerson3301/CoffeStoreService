namespace CoffeStore.Modules.Customers.Application.ErrorContext
{
    internal interface IErrorContext
    {
        public void AddError(ErrorType errorType, object error);

        public ErrorViewModel GetErrors();
    }
}
