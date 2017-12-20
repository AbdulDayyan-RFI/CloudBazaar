using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Domain;
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

    }
}
