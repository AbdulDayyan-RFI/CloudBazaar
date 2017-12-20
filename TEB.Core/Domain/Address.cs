using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public int? CountryId { get; set; }
        public int? StateprovinceId { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipPostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string CustomAttributes { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
    }
}
