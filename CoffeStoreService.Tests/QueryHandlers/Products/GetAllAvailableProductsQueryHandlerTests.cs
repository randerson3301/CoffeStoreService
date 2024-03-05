using CoffeStore.Modules.Products.Application.Adapters.Contracts;
using CoffeStore.Modules.Products.Application.Queries;
using CoffeStore.Modules.Products.Application.Queries.Handlers;
using CoffeStore.Modules.Products.Application.ViewModels;
using CoffeStore.Modules.Products.Domain;
using CoffeStore.Modules.Products.Domain.Contracts;
using NSubstitute;
using NUnit.Framework;

namespace CoffeStore.Tests.QueryHandlers.Products
{
    internal class GetAllAvailableProductsQueryHandlerTests
    {
        private GetProductsByFiltersQueryHandler _queryHandler;
        private IProductRepository _repository;
        private IProductAdapter _adapter;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IProductRepository>();
            _adapter = Substitute.For<IProductAdapter>();
            _queryHandler = new GetProductsByFiltersQueryHandler(_repository, _adapter);
        }

        [Test]
        public async Task Get_OnlyAvailableProducts_Returns_Success()
        {
            var availableProduct = new Product(string.Empty, string.Empty, 0, string.Empty, Guid.NewGuid());
            var unavailableProduct = new Product(string.Empty, string.Empty, 0, string.Empty, Guid.NewGuid());
            var products = new List<Product>();
            var request = new GetProductsByFiltersQuery(true);

            unavailableProduct.Disable();

            products.Add(availableProduct);
            products.Add(unavailableProduct);

            var availableProducts = products.Where(p => p.IsAvailable).ToList();

            _repository.GetProductsByFiltersAsync(request).ReturnsForAnyArgs(availableProducts);

            _adapter.ConvertToViewModel(availableProducts).Returns(availableProducts.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Description = x.Description,
                ImagePath = x.ImagePath,
                Title = x.ProductName
            }).ToList().AsReadOnly());

            IReadOnlyCollection<ProductViewModel> result = await _queryHandler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.Any());
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public async Task Get_OnlyAvailableProducts_Returns_NoProducts()
        {
            var product = new Product(string.Empty, string.Empty, 0, string.Empty, Guid.NewGuid());
            var product2 = new Product(string.Empty, string.Empty, 0, string.Empty, Guid.NewGuid());
            var products = new List<Product>();
            var request = new GetProductsByFiltersQuery(true);

            product.Disable();
            product2.Disable();

            products.Add(product);
            products.Add(product2);

            var availableProducts = products.Where(p => p.IsAvailable).ToList();

            _repository.GetProductsByFiltersAsync(request).ReturnsForAnyArgs(availableProducts);

            _adapter.ConvertToViewModel(availableProducts).Returns(availableProducts.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Description = x.Description,
                ImagePath = x.ImagePath,
                Title = x.ProductName
            }).ToList().AsReadOnly());

            IReadOnlyCollection<ProductViewModel> result = await _queryHandler.Handle(request, CancellationToken.None);

            Assert.IsFalse(result.Any());
        }
    }
}
