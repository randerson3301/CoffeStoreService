using CoffeStore.EcommerceApp.Adapters.Contracts;
using CoffeStore.EcommerceApp.Dtos.Products;
using CoffeStore.EcommerceApp.ViewModels;
using CoffeStore.Models.Aggregates.ProductAggregate;
using CoffeStore.Models.Contracts.Repositories;
using CoffeStore.Models.Resources;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoffeStore.EcommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IProductAdapter _adapter;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository repository, IProductAdapter adapter, ILogger<ProductController> logger)
        {
            _repository = repository;
            _adapter = adapter;
            _logger = logger;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var viewModels = _adapter.ConvertToViewModel(await _repository.GetAvailableProductsAsync());
                return Ok(viewModels);
            }
            catch (Exception error)
            {
                _logger.LogError(error, ErrorMessages.QUERYING_PRODUCTS_FAILED);

                return BadRequest(ErrorMessages.QUERYING_PRODUCTS_FAILED);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                Product result = await _repository.GetProductByIdAsync(id);

                if(result == null)
                {
                    return NotFound();
                }

                ProductViewModel viewModel = _adapter.ConvertToViewModel(result);
                return Ok(viewModel);
            }
            catch (Exception error)
            {
                _logger.LogError(error, ErrorMessages.QUERYING_PRODUCTS_FAILED);

                return BadRequest(ErrorMessages.QUERYING_PRODUCTS_FAILED);
            }
        }

        [HttpGet("featured")]
        public IActionResult GetFeaturedProductImages()
        {
            try
            {
                return Ok(_repository.GetFeaturedProductsImages());
            }
            catch (Exception error)
            {
                _logger.LogError(error, ErrorMessages.QUERYING_PRODUCTS_FAILED);

                return BadRequest(ErrorMessages.QUERYING_PRODUCTS_FAILED);
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
        {
            var domain = _adapter.ConvertToDomain(request);

            try
            {
                await _repository.AddAsync(domain);
                return CreatedAtAction(nameof(ProductController.Post), domain);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Adding new product has failed");
                return BadRequest("Adding new product has failed");
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
