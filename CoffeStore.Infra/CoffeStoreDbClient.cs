using CoffeStore.Infra.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStore.Infra
{
    public class CoffeStoreDbClient : MongoClient
    {
        public CoffeStoreDbClient(IOptions<CoffeStoreDatabaseSettings> options): base(options.Value.ConnectionString) { }
    }
}
