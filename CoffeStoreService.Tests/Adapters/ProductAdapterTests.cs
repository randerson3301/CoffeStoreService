using CoffeStore.Modules.Products.Application.Adapters;
using CoffeStore.Modules.Products.Application.Adapters.Contracts;
using CoffeStore.Modules.Products.Application.ViewModels;
using CoffeStore.Modules.Products.Domain;
using CoffeStore.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeStore.Tests.Adapters
{
    internal class ProductAdapterTests
    {
        private IProductAdapter _adapter;

        [SetUp]
        public void SetUp()
        {
            _adapter = new ProductAdapter();
        }

        [Test]
        public void Adapter_ConvertCommandToDomain_ReturnDomainInstance()
        {
            var command = ProductMock.GetCommand();

            var domain = _adapter.ConvertToDomain(command);

            Assert.IsInstanceOf<Product>(domain);
            Assert.AreEqual(command.Name, domain.ProductName);
            Assert.AreEqual(command.Description, domain.Description);
            Assert.AreEqual(command.Price, domain.Price);
        }

        [Test]
        public void Adapter_ConvertDomainToViewModel_ReturnViewModelInstance()
        {
            var domain = _adapter.ConvertToDomain(ProductMock.GetCommand());

            var viewModel = _adapter.ConvertToViewModel(domain);

            Assert.IsInstanceOf<ProductViewModel>(viewModel);
            Assert.AreEqual(domain.ProductName, viewModel.Title);
            Assert.AreEqual(domain.Description, viewModel.Description);
            Assert.AreEqual(domain.Price, viewModel.Price);
            Assert.AreEqual(domain.AverageRate, viewModel.RateNumber);
        }

        [Test]
        public void Adapter_ConvertDomainToViewModels_ReturnViewModelCollectionInstance()
        {
            var domain = _adapter.ConvertToDomain(ProductMock.GetCommand());
            var domain2 = _adapter.ConvertToDomain(ProductMock.GetCommand());

            var domains = new List<Product>();
            domains.Add(domain);
            domains.Add(domain2);

            var viewModels = _adapter.ConvertToViewModel(domains);

            Assert.IsInstanceOf<ICollection<ProductViewModel>>(viewModels);
            Assert.IsTrue(domains.Any());
        }
    }
}
