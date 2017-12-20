using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.ViewModel
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public Guid CustomerGuid { get; set; }
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        public string EmailToRevaildate { get; set; }
        public string AdminComment { get; set; }
        public bool IsTaxExempt { get; set; }
        public int AffiliateId { get; set; }
        public int VendorId { get; set; }
        public bool HasShoppingCartItems { get; set; }
        public bool RequireReLogin { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime? CannotLoginUntilDate { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public bool IsSystemAccount { get; set; }
        public string SystemName { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public int RegisteredInStoreId { get; set; }
        public int? BillingAddress_Id { get; set; }
        public int? ShippingAddress_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConformPassword { get; set; }
        public string Gender { get; set; }
    }
}
