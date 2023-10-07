using CoffeStoreService.API.Models.DomainExceptions;
using CoffeStoreService.API.Models.Enums;

namespace CoffeStoreService.API.Models.Aggregates.EmployeeAggregate
{
    public class Employee
    {
        private readonly List<Employee> _employees;
        private readonly List<ShopAddress> _shopAddresses;
        private readonly Guid _id;

        public Guid Id => _id;
        public string FullName { get; }
        public string Email { get; }
        public string Login { get; }
        public string Password { get; }
        public ProfileEnum Profile { get; }
        

        public IReadOnlyCollection<Employee> Subordinates => _employees.AsReadOnly();

        public IReadOnlyCollection<ShopAddress> ShopAddresses => _shopAddresses.AsReadOnly();

        public bool IsEnabled { get; private set; }

        public Employee(string fullName, string email, string login, string password, ProfileEnum profile)
        {
            FullName = fullName;
            Email = email;
            Login = login;
            Password = password;
            Profile = profile;
            IsEnabled = true;
            _employees = new List<Employee>();
            _shopAddresses = new List<ShopAddress>();
        }

        public void AddEmployee(Employee employee)
        {
            if(Profile != ProfileEnum.Admin)
            {
                throw new NotAdminException();
            }

            _employees.Add(employee);
        }

        public void AddNewShopAddress(ShopAddress shopAddress)
        {
            if (Profile != ProfileEnum.Admin)
            {
                throw new NotAdminException();
            }

            _shopAddresses.Add(shopAddress);
        }
        

        public Employee DisableEmployee(Employee employeeToBeDisabled)
        {
            if (Profile != ProfileEnum.Admin)
            {
                throw new NotAdminException();
            }

            employeeToBeDisabled.IsEnabled = false;

            _employees.Remove(employeeToBeDisabled);

            return employeeToBeDisabled;
        }

        public void RemoveShopAddress(ShopAddress shopAddress)
        {
            if (Profile != ProfileEnum.Admin)
            {
                throw new NotAdminException();
            }

            _shopAddresses.Remove(shopAddress);
        }
    }
}