﻿using CoffeStore.EcommerceApp.Dtos;
using CoffeStore.EcommerceApp.ViewModels;
using CoffeStore.Models.Aggregates.CustomerAggregate;
using Isopoh.Cryptography.Argon2;

namespace CoffeStore.EcommerceApp.Adapters
{
    public class CustomerAdapter : ICustomerAdapter
    {
        public Customer ConvertToDomain(CreateCustomerRequest customerDto)
        {
            var domain = new Customer(customerDto.Name, customerDto.BirthDate.Value, customerDto.Document, new CustomerAccess(customerDto.Login.Email, Argon2.Hash(customerDto.Login.Password)));
            var newAddress = customerDto.DeliveryAddress;
            
            domain.AddAddress(new DeliveryAddress(newAddress.ZipCode, newAddress.Address, newAddress.Number, 
                newAddress.Complement, newAddress.Neighborhood, newAddress.City, newAddress.State));

            return domain;
        }

        public Customer ConvertToDomain(CreateCustomerRequest customerDto, Customer domain)
        {
            throw new NotImplementedException();
        }

        public DeliveryAddress ConvertToDomainAddress(CustomerAddressDto deliveryAddress)
        {
            throw new NotImplementedException();
        }

        public CustomerViewModel ConvertToViewModel(Customer domain)
        {
            var domainAddress = domain.DeliveryAddress.SingleOrDefault();
            return new CustomerViewModel()
            {
                Id = domain.Id,
                BirthDate = domain.BirthDate,
                Document = domain.Document,
                Email = domain.CustomerAccess.Email,
                Name = domain.FullName,
                Addresses = domain.DeliveryAddress.Select(a => new CustomerAddressDto()
                {
                    Address = a.Address,
                    City = a.City,
                    Complement = a.Complement,
                    Neighborhood = a.Neighborhood,
                    Number = a.Number,
                    State = a.State,
                    ZipCode = a.ZipCode
                }).AsEnumerable()
            };
        }
    }
}
