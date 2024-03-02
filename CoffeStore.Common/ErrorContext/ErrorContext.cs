namespace CoffeStore.Common.ErrorContext
{
    public sealed class ErrorContext : IErrorContext
    {
        public ErrorViewModel Error { get; private set; }

        public void AddError(ErrorType errorType, object error)
        {
            Error = new ErrorViewModel(errorType, error);
        }

        public ErrorViewModel GetErrors() => Error;

        public void HandleErrors(bool? result)
        {
            throw new NotImplementedException();
        }
    }
}
