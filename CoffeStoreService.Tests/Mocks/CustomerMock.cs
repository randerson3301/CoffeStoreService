using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStoreService.Tests.Mocks
{
    internal static class CustomerMock
    {
        public static Customer GetCustomer()
        {
            var customerAccess = new CustomerAccess("teste@teste.com", "senha123");
            return new Customer("Test", DateOnly.MinValue, "1234", customerAccess);
        }
    }
}
