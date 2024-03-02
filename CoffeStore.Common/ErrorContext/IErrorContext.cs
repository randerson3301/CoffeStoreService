namespace CoffeStore.Common.ErrorContext
{
    public interface IErrorContext
    {
        public void AddError(ErrorType errorType, object error);

        public ErrorViewModel GetErrors();
    }
}
