using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Domain;
using TEB.Core.ViewModel;
using TEB.Data;

namespace TEB.Service
{
    public class AddressService : IAddressService
    {
        private readonly IGenericRepository<Address> _addressRepository;

        public AddressService(IGenericRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public Task<IEnumerable<Address>> GetAllAddressByCustomerId(int id)
        {
            var query = @"select a.[Id],a.[FirstName],a.[LastName],a.[Email],a.[Company],a.[CountryId],a.[StateProvinceId],a.[Address1],a.[Address2],a.[ZipPostalCode],a.[PhoneNumber],a.[FaxNumber],a.[CustomAttributes],a.[CreatedOnUtc] 
                        from CustomerAddresses ca 
                        join [address] a on a.id=ca.[Address_Id] where Customer_Id=@customerId";
            var param = new { customerId = id };
            var allCustomerAddressList = _addressRepository.Get<Address>(query, param).ToList();
            return Task.FromResult<IEnumerable<Address>>(allCustomerAddressList);
        }

        public Task<Address> GetAddressById(int id)
        {
            var query = @"Select * from [dbo].[Address] where Id=@id";
            var param = new { id = id };
            var address = _addressRepository.Get(query, param);
            return Task.FromResult<Address>(address);
        }

        public object AddCustomerAddress(AddressViewModel address)
        {
            var addressInserted = InsertAddress(address);
            return InsertCustomerAddresses(address.CustomerID, Convert.ToInt32(addressInserted));
        }

        public object InsertCustomerAddresses(int customerId, int addressId)
        {
            string query = @"INSERT INTO [dbo].[CustomerAddresses] ([Customer_Id] ,[Address_Id]) VALUES (@Customer_Id, @Address_Id)";
            var param = new
            {
                Customer_Id = customerId,
                Address_Id = addressId
            };
            return _addressRepository.Add(query, param);

        }

        public object InsertAddress(AddressViewModel address)
        {
            string query = @"INSERT INTO [dbo].[Address] ([FirstName] ,[LastName] ,[Email] ,
                            [Company] ,[CountryId] ,[StateProvinceId] ,[City] ,[Address1] ,[Address2] ,
                            [ZipPostalCode] ,[PhoneNumber] ,[FaxNumber] ,[CustomAttributes] ,[CreatedOnUtc]) VALUES (@FirstName, @LastName, 
                            @Email, @Company, @CountryId, @StateProvinceId, @City, @Address1, @Address2, @ZipPostalCode, @PhoneNumber, 
                            @FaxNumber, @CustomAttributes, GETUTCDATE()); SELECT SCOPE_IDENTITY()";
            var param = new
            {
                FirstName = address.FirstName,
                LastName = address.LastName,
                Email = address.Email,
                Company = address.Company,
                CountryId = address.CountryId,
                StateProvinceId = address.StateProvinceId,
                City = address.City,
                Address1 = address.Address1,
                Address2 = address.Address2,
                ZipPostalCode = address.ZipPostalCode,
                PhoneNumber = address.PhoneNumber,
                FaxNumber = address.FaxNumber,
                CustomAttributes = address.CustomAttributes
            };
            return _addressRepository.Add(query, param);

        }

    }
}
