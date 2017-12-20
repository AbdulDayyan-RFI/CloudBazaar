using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Domain;
using TEB.Core.ViewModel;

namespace TEB.Core.Mapping
{
    public static class CustomerMapping
    {
        public static Customer ViewToModel(CustomerViewModel model)
        {
            Customer Customer = new Customer
            {
                Id = model.Id,
                CustomerGuid = Guid.NewGuid(),
                Username = model.Username,
                Email = model.Email,
                EmailToRevaildate = model.EmailToRevaildate,
                AdminComment = model.AdminComment,
                IsTaxExempt = model.IsTaxExempt,
                AffiliateId = model.AffiliateId,
                VendorId = model.VendorId,
                HasShoppingCartItems = model.HasShoppingCartItems,
                RequireReLogin = model.RequireReLogin,
                FailedLoginAttempts = model.FailedLoginAttempts,
                CannotLoginUntilDate = model.CannotLoginUntilDate.Value,
                Active = model.Active,
                Deleted = model.Deleted,
                IsSystemAccount = model.IsSystemAccount,
                SystemName = model.SystemName,
                LastIpAddress = model.LastIpAddress,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                LastLoginDate = model.LastLoginDate.Value,
                LastActivityDate = model.LastActivityDate,
                RegisteredInStoreId = model.RegisteredInStoreId,
                BillingAddress_Id = model.BillingAddress_Id.Value,
                ShippingAddress_Id = model.ShippingAddress_Id.Value

            };
            return Customer;
        }
        public static CustomerViewModel ModelToView(Customer model)
        {
            CustomerViewModel CustomerViewModel = new CustomerViewModel
            {
                Id = model.Id,
                CustomerGuid = Guid.NewGuid(),
                Username = model.Username,
                Email = model.Email,
                EmailToRevaildate = model.EmailToRevaildate,
                AdminComment = model.AdminComment,
                IsTaxExempt = model.IsTaxExempt,
                AffiliateId = model.AffiliateId,
                VendorId = model.VendorId,
                HasShoppingCartItems = model.HasShoppingCartItems,
                RequireReLogin = model.RequireReLogin,
                FailedLoginAttempts = model.FailedLoginAttempts,
                CannotLoginUntilDate = model.CannotLoginUntilDate,
                Active = model.Active,
                Deleted = model.Deleted,
                IsSystemAccount = model.IsSystemAccount,
                SystemName = model.SystemName,
                LastIpAddress = model.LastIpAddress,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                LastLoginDate = model.LastLoginDate,
                LastActivityDate = model.LastActivityDate,
                RegisteredInStoreId = model.RegisteredInStoreId,
                BillingAddress_Id = model.BillingAddress_Id,
                ShippingAddress_Id = model.ShippingAddress_Id
            };
            return CustomerViewModel;
        }
    }
}
