using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.ViewModel
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Company { get; set; }
        [Required]
        public int? CountryId { get; set; }
        public int? StateProvinceId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string ZipPostalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string CustomAttributes { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CustomerID { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
