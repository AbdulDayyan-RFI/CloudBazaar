using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Domain;
using TEB.Core.ViewModel;

namespace TEB.Core.Mapping
{
    public static class AddressMapping
    {
        public static Address ViewToModel(AddressViewModel viewModel)
        {
            Address address = new Address
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Company = viewModel.Company,
                CountryId = viewModel.CountryId,
                StateprovinceId = viewModel.StateprovinceId,
                City = viewModel.City,
                Address1 = viewModel.Address1,
                Address2 = viewModel.Address2,
                ZipPostalCode = viewModel.ZipPostalCode,
                PhoneNumber = viewModel.PhoneNumber,
                FaxNumber = viewModel.FaxNumber,
                CustomAttributes = viewModel.CustomAttributes,
                CreatedOnUtc = viewModel.CreatedOnUtc

            };
            return address;
        }

        public static AddressViewModel ModelToViewModel(Address address)
        {
            AddressViewModel viewModel = new AddressViewModel
            {
                Id = address.Id,
                FirstName = address.FirstName,
                LastName = address.LastName,
                Email = address.Email,
                Company = address.Company,
                CountryId = address.CountryId,
                StateprovinceId = address.StateprovinceId,
                City = address.City,
                Address1 = address.Address1,
                Address2 = address.Address2,
                ZipPostalCode = address.ZipPostalCode,
                PhoneNumber = address.PhoneNumber,
                FaxNumber = address.FaxNumber,
                CustomAttributes = address.CustomAttributes,
                CreatedOnUtc = address.CreatedOnUtc
            };
            return viewModel;
        }
    }
}
