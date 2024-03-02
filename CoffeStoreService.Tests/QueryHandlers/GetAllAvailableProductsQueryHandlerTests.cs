using CoffeStore.Modules.Products.Application.Adapters.Contracts;
using CoffeStore.Modules.Products.Application.Queries;
using CoffeStore.Modules.Products.Application.Queries.Handlers;
using CoffeStore.Modules.Products.Application.ViewModels;
using CoffeStore.Modules.Products.Domain;
using CoffeStore.Modules.Products.Domain.Contracts;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStore.Tests.QueryHandlers
{
    internal class GetAllAvailableProductsQueryHandlerTests
    {
        private GetAllAvailableProductsQueryHandler _queryHandler;
        private IProductRepository _repository;
        private IProductAdapter _adapter;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IProductRepository>();
            _adapter = Substitute.For<IProductAdapter>();
            _queryHandler = new GetAllAvailableProductsQueryHandler(_repository, _adapter);
        }

        [Test]
        public async Task Get_AvailableProducts_Returns_Success()
        {
            var domain = new Product(string.Empty, string.Empty, 0, string.Empty, Guid.NewGuid());
            ICollection<Product> productList = new List<Product>();
            productList.Add(domain);

            var request = new GetAllAvailableProductsQuery();
            _repository.GetAvailableProductsAsync().ReturnsForAnyArgs(productList);
            _adapter.ConvertToViewModel(productList).Returns(productList.Select(x => new ProductViewModel() { Id = domain.Id, 
                Description = domain.Description, ImagePath = domain.ImagePath, Title = domain.ProductName}).ToList().AsReadOnly());

            IReadOnlyCollection<ProductViewModel> result = await _queryHandler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.Any());
        }
    }
}
