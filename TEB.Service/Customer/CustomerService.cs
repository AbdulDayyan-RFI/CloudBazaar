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
    public class CustomerService : ICustomerService
    {

        private readonly IGenericRepository<Customer> _customerRepository;
        public CustomerService(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<IEnumerable<Customer>> GetAllCustomersList()
        {
            var query = @" select * from Customer ";
            var allCustomersList = _customerRepository.Get<Customer>(query, null).ToList();
            return Task.FromResult<IEnumerable<Customer>>(allCustomersList);
        }

        public object InsertCustomer(Customer model)
        {
            var query = @"INSERT [dbo].[CustomerEComm] ( [CustomerGuid], [Username], [Email], [EmailToRevaildate], [AdminComment], 
                        [IsTaxExempt], [AffiliateId], [VendorId], [HasShoppingCartItems], [RequireReLogin], [FailedLoginAttempts],
                        [CannotLoginUntilDate], [Active],
                        [Deleted], [IsSystemAccount], [SystemName], [LastIpAddress], 
                        [CreatedDate], [UpdatedDate], [LastLoginDate], 
                        [LastActivityDate],[RegisteredInStoreId], [BillingAddress_Id], [ShippingAddress_Id]
                        ) 
                        values(@CustomerGuid, @Username, @Email, @EmailToRevaildate, @AdminComment, 
                        @IsTaxExempt, @AffiliateId, @VendorId, @HasShoppingCartItems, @RequireReLogin, @FailedLoginAttempts, GETUTCDATE(), @Active,
                        @Deleted, @IsSystemAccount, @SystemName, @LastIpAddress, GETUTCDATE(), GETUTCDATE(),GETUTCDATE()
                        ,GETUTCDATE(), @RegisteredInStoreId, @BillingAddress_Id, @ShippingAddress_Id ); SELECT SCOPE_IDENTITY()";

            var param = new
            {
                CustomerGuid = model.CustomerGuid,
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
                ShippingAddress_Id = model.ShippingAddress_Id,

            };
            return _customerRepository.Update(query, param);

        }

        public Customer GetCustomerById(int Id)
        {
            string query = "select * from Customer where Id = @Id ";
            var param = new { Id = Id };
            var customer = _customerRepository.Get(query, param);
            return customer;
        }

        public int UpdateCustomer(CustomerViewModel model)
        {
            var query = @"UPDATE [dbo].[Customer] SET CustomerGuid=@CustomerGuid,Username=@Username,
                        Email=@Email,EmailToRevalidate=@EmailToRevalidate,AdminComment=@AdminComment,IsTaxExempt=@IsTaxExempt,AffiliateId=@AffiliateId,
                        VendorId=@VendorId,HasShoppingCartItems=@HasShoppingCartItems,RequireReLogin=@RequireReLogin,FailedLoginAttempts=@FailedLoginAttempts,CannotLoginUntilDateUtc=GETUTCDATE(),
                        Active=@Active,Deleted=@Deleted,IsSystemAccount=@IsSystemAccount,SystemName=@SystemName,
                        LastIpAddress=@LastIpAddress,CreatedOnUtc=GETUTCDATE(),
                        LastLoginDateUtc=GETUTCDATE(),LastActivityDateUtc=GETUTCDATE(),RegisteredInStoreId=@RegisteredInStoreId,
                        BillingAddress_Id=@BillingAddress_Id,ShippingAddress_Id=@ShippingAddress_Id WHERE Id = @Id; SELECT SCOPE_IDENTITY();";

            var param = new
            {
                Id = model.Id,
                CustomerGuid = model.CustomerGuid,
                Username = model.Username,
                Email = model.Email,
                EmailToRevalidate = model.EmailToRevaildate,
                AdminComment = model.AdminComment,
                IsTaxExempt = model.IsTaxExempt,
                AffiliateId = model.AffiliateId,
                VendorId = model.VendorId,
                HasShoppingCartItems = model.HasShoppingCartItems,
                RequireReLogin = model.RequireReLogin,
                FailedLoginAttempts = model.FailedLoginAttempts,
                CannotLoginUntilDateUtc = model.CannotLoginUntilDate,
                Active = model.Active,
                Deleted = model.Deleted,
                IsSystemAccount = model.IsSystemAccount,
                SystemName = model.SystemName,
                LastIpAddress = model.LastIpAddress,
                CreatedOnUtc = model.CreatedDate,
                LastLoginDateUtc = model.LastLoginDate,
                LastActivityDateUtc = model.LastActivityDate,
                RegisteredInStoreId = model.RegisteredInStoreId,
                BillingAddress_Id = model.BillingAddress_Id,
                ShippingAddress_Id = model.ShippingAddress_Id,
            };
            _customerRepository.Update(query, param);
            return model.Id;
        }

        public int DeleteCustomer(int Id)
        {
            var query = @"UPDATE [dbo].[Customer] SET Active=0,UpdatedDate=GETUTCDATE() WHERE Id = @Id";
            var param = new { Id = Id };
            return _customerRepository.Delete(query, param);
        }

        public Customer GetCustomerByUsername(string userName)
        {
            string query = "select * from Customer where Username='" + userName + "' ";
            var param = new { Username = userName };
            var customer = _customerRepository.Get(query, param);
            return customer;
        }

        public Customer GetCustomerByEmail(string email)
        {
            string query = "select * from Customer where Email='" + email + "' ";
            var param = new { Email = email };
            var customer = _customerRepository.Get(query, param);
            return customer;
        }


    }
}
