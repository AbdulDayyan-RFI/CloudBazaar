using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Domain;

namespace TEB.Service
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAllAddressByCustomerId(int id);
        Task<Address> GetAddressById(int id);
    }
}
