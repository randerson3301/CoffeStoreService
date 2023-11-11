using CoffeStore.Infra.Settings;
using CoffeStore.Models.Aggregates.CustomerAggregate;
using CoffeStore.Models.Aggregates.EmployeeAggregate;
using CoffeStore.Models.Aggregates.OrderAggregate;
using CoffeStore.Models.Aggregates.ProductAggregate;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStore.Infra.Context
{
    public class CoffeStoreDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly CoffeStoreDatabaseSettings _settings;

        public CoffeStoreDbContext(IOptions<CoffeStoreDatabaseSettings> options, MongoClient coffeStoreClient)
        {
            _settings = options.Value;

            _database = coffeStoreClient.GetDatabase(_settings.DatabaseName);
        }

        public IMongoCollection<Customer> Customers => _database.GetCollection<Customer>(_settings.CustomersCollectionName);
        public IMongoCollection<Order> Orders => _database.GetCollection<Order>(_settings.OrdersCollectionName);
        public IMongoCollection<Product> Products => _database.GetCollection<Product>(_settings.ProductsCollectionName);
        public IMongoCollection<Employee> Employees => _database.GetCollection<Employee>(_settings.EmployeesCollectionName);
    }
}
