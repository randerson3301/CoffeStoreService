using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStore.Infra.Settings
{
    public class CoffeStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ProductsCollectionName { get; set; } = null!;
        public string CustomersCollectionName { get; set; } = null!;
        public string OrdersCollectionName { get; set; } = null!;
        public string EmployeesCollectionName { get; set; } = null!;
    }
}
