namespace CoffeStore.EcommerceApp.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
    }
}
