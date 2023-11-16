using CoffeStore.EcommerceApp.Adapters;
using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.Services;
using CoffeStore.EcommerceApp.Validators;
using CoffeStore.EcommerceApp.ViewModels;
using CoffeStore.Models.Contracts.Repositories;
using CoffeStore.Models.Resources;
using FluentValidation;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;

namespace CoffeStore.EcommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly ICustomerAdapter _adapter;
        private readonly ILogger<CustomerController> _logger;
        private readonly IValidator<CreateCustomerRequest> _validator;
        private readonly IValidator<CustomerAddressDto> _validatorAddress;
        private readonly TokenService _tokenService;

        public CustomerController(ICustomerRepository repository, ICustomerAdapter adapter, ILogger<CustomerController> logger, IValidator<CreateCustomerRequest> validator, IValidator<CustomerAddressDto> validatorAddress, TokenService tokenService)
        {
            _repository = repository;
            _adapter = adapter;
            _logger = logger;
            _validator = validator;
            _validatorAddress = validatorAddress;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCustomerRequest dto)
        {
            var result = await _validator.ValidateAsync(dto);
            var resultAddress = await _validatorAddress.ValidateAsync(dto.DeliveryAddress);

            if (!result.IsValid || !resultAddress.IsValid)
            {
                result.Errors.AddRange(resultAddress.Errors);

                var errorResponse = new ErrorResponseViewModel()
                {
                    IsValid = result.IsValid,
                    ErrorMessages = result.Errors.Select(e => e.ErrorMessage).ToArray()
                };

                return BadRequest(errorResponse);
            }

            var domain = _adapter.ConvertToDomain(dto);

            try
            {
                await _repository.AddAsync(domain);

                var viewModel = _adapter.ConvertToViewModel(domain);

                return CreatedAtAction(nameof(CustomerController.Add), viewModel);
            }
            catch (Exception error)
            {
                _logger.LogError(error, ErrorMessages.ADD_CUSTOMER_FAILED);
                return BadRequest(ErrorMessages.ADD_CUSTOMER_FAILED);
            }
        }

        //[HttpPut]
        //public async Task<IActionResult> Update(CreateCustomerRequest dto)
        //{
        //    var result = await _validator.ValidateAsync(dto);

        //    if (!result.IsValid)
        //    {
        //        return BadRequest(result);
        //    }

        //    var domain = await _repository.GetByIdAsync(dto.Id);

        //    if (domain == null)
        //    {
        //        return NotFound();
        //    }

        //    domain = _adapter.ConvertToDomain(dto, domain);

        //    if (dto.DeliveryAddress != null)
        //    {
        //        var newAddress = _adapter.ConvertToDomainAddress(dto.DeliveryAddress); //new DeliveryAddress(newAddress.ZipCode, newAddress.Address, newAddress.Number, newAddress.Complement, newAddress.Neighborhood)
        //        domain.AddAddress(newAddress);
        //    }

        //    try
        //    {
        //        await _repository.UpdateAsync(domain);
        //        return NoContent();
        //    }
        //    catch (Exception error)
        //    {
        //        _logger.LogError(error, ErrorMessages.UPDATE_CUSTOMER_FAILED);
        //        return BadRequest(ErrorMessages.UPDATE_CUSTOMER_FAILED);
        //    }
        //}

        [HttpDelete("address")]
        public async Task<IActionResult> DeleteAddress(string customerId, CustomerAddressDto addressToRemove)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if(customer == null)
            {
                return NotFound();
            }

            return Ok(_adapter.ConvertToViewModel(customer));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerLoginDto loginDto)
        {          
            var domain = await _repository.GetByEmail(loginDto.Email);

            var errorResponse = new ErrorResponseViewModel()
            {
                IsValid = false,
                ErrorMessages = new string[] { "Usuário/senha incorretos" }
            };

            if (domain == null)
            {
                return Unauthorized(errorResponse);
            }

            if(!Argon2.Verify(domain.CustomerAccess.Password, loginDto.Password))
            {
                return Unauthorized(errorResponse);
            }

            var loginViewModel = new LoginViewModel
            {
                Name = domain.FullName.Split(" ")[0],
                Token = _tokenService.GenerateToken(domain.Id)
            };

            //cria o login response contendo nome e token do cliente e retorna Ok
            return Ok(loginViewModel);
        }
    }
}