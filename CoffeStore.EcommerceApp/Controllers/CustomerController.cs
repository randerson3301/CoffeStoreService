using CoffeStore.EcommerceApp.Adapters;
using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.Validators;
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
        private IValidator<CustomerDto> _validator;
        private IValidator<CustomerAddressDto> _validatorAddress;

        public CustomerController(ICustomerRepository repository, ICustomerAdapter adapter, ILogger<CustomerController> logger, IValidator<CustomerDto> validator, IValidator<CustomerAddressDto> validatorAddress)
        {
            _repository = repository;
            _adapter = adapter;
            _logger = logger;
            _validator = validator;
            _validatorAddress = validatorAddress;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerDto dto)
        {
            var result = await _validator.ValidateAsync(dto);
            var resultAddress = await _validatorAddress.ValidateAsync(dto.DeliveryAddress);

            if (!result.IsValid || !resultAddress.IsValid)
            {
                result.Errors.AddRange(resultAddress.Errors);
                return BadRequest(result);
            }

            var domain = _adapter.ConvertToDomain(dto);

            try
            {
                var viewModel = _adapter.ConvertToViewModel(await _repository.AddAsync(domain));
                return CreatedAtAction(nameof(CustomerController.Add), viewModel);
            }
            catch (Exception error)
            {
                _logger.LogError(error, ErrorMessages.ADD_CUSTOMER_FAILED);
                return BadRequest(ErrorMessages.ADD_CUSTOMER_FAILED);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerDto dto)
        {
            var result = await _validator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            var domain = await _repository.GetByIdAsync(dto.Id);

            if (domain == null)
            {
                return NotFound();
            }

            domain = _adapter.ConvertToDomain(dto, domain);

            if (dto.DeliveryAddress != null)
            {
                var newAddress = _adapter.ConvertToDomainAddress(dto.DeliveryAddress); //new DeliveryAddress(newAddress.ZipCode, newAddress.Address, newAddress.Number, newAddress.Complement, newAddress.Neighborhood)
                domain.AddAddress(newAddress);
            }

            try
            {
                await _repository.UpdateAsync(domain);
                return NoContent();
            }
            catch (Exception error)
            {
                _logger.LogError(error, ErrorMessages.UPDATE_CUSTOMER_FAILED);
                return BadRequest(ErrorMessages.UPDATE_CUSTOMER_FAILED);
            }
        }

        [HttpDelete("address")]
        public async Task<IActionResult> DeleteAddress(Guid customerId, CustomerAddressDto addressToRemove)
        {           
            var result = await _validatorAddress.ValidateAsync(addressToRemove);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            var domain = await _repository.GetByIdAsync(customerId);

            if (domain == null)
            {
                return NotFound();
            }

            domain.RemoveAddress(_adapter.ConvertToDomainAddress(addressToRemove));

            try
            {
                await _repository.UpdateAsync(domain);
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