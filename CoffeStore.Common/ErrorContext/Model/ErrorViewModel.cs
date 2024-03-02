namespace CoffeStore.Common.ErrorContext
{
    public sealed class ErrorViewModel
    {
        public ErrorViewModel(ErrorType errorType, object errors)
        {
            ErrorType = errorType;
            ErrorTypeDesc = errorType.ToString();
            Errors = errors;
        }
        public ErrorType ErrorType { get; set; }
        public string ErrorTypeDesc { get; set; }
        public object Errors { get; set; }
    }
}
