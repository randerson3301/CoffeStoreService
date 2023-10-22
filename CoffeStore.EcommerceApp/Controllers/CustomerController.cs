using CoffeStore.EcommerceApp.Adapters;
using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.Models.Contracts.Repositories;
using CoffeStore.Models.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CoffeStore.EcommerceApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _repository;
        private ICustomerAdapter _adapter;
        private ILogger<CustomerController> _logger;
        private IValidator<BaseCustomerDto> _validator;

        public CustomerController(ICustomerRepository repository, ICustomerAdapter adapter, ILogger<CustomerController> logger, IValidator<BaseCustomerDto> validator)
        {
            _repository = repository;
            _adapter = adapter;
            _logger = logger;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerSignUpDto dto)
        {
            var result = await _validator.ValidateAsync(dto);

            if (!result.IsValid)
            { 
                return BadRequest(result);
            }

            var domain = _adapter.ConvertToDomain(dto);

            try
            {
                var viewModel = _adapter.ConvertToViewModel(await _repository.Add(domain));
                return CreatedAtAction(nameof(CustomerController.Add), viewModel);
            }
            catch (Exception error)
            {
                _logger.LogError(error, ErrorMessages.ADD_CUSTOMER_FAILED);
                return BadRequest(ErrorMessages.ADD_CUSTOMER_FAILED);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerUpdateDto dto)
        {
            var result = await _validator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            var domain = _adapter.ConvertToDomain(dto);

            try
            {
                await _repository.Update(domain);
                return NoContent();
            }
            catch (Exception error)
            {
                _logger.LogError(error, ErrorMessages.UPDATE_CUSTOMER_FAILED);
                return BadRequest(ErrorMessages.UPDATE_CUSTOMER_FAILED);
            }
        }
    }
}