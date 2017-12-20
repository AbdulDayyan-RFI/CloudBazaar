using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.ViewModel;
using TEB.Core.Enumerations;
using TEB.Data;
using System.Net.Mail;
using System.Configuration;
using System.Security.Cryptography;

namespace TEB.Service
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IGenericRepository<CustomerPassword> _custPasswordRepository;
        private readonly IGenericRepository<Address> _AddressRepository;
        private readonly IGenericRepository<CustomerAddresses> _CustAddressRepository;

        public UserService(IGenericRepository<Customer> customerRepository, IGenericRepository<CustomerPassword> custPasswordRepository,
            IGenericRepository<Address> AddressRepository, IGenericRepository<CustomerAddresses> CustAddressRepository)
        {
            _customerRepository = customerRepository;
            _custPasswordRepository = custPasswordRepository;
            _AddressRepository = AddressRepository;
            _CustAddressRepository = CustAddressRepository;
        }

        public Customer GetUserDetails(string UserName, string password)
        {
            var cust = GetAccountDetails(UserName);
            if (PasswordsMatch(GetCurrentPassword(cust.Id), password))
            {
                return cust;
            }
            return null;
        }

        public Customer RegisterUser(CustomerViewModel Model)
        {
            string query = @"INSERT INTO [dbo].[Customer]([CustomerGuid],[Username],[Email],[EmailToRevalidate],[AdminComment],[IsTaxExempt],[AffiliateId],
                            [VendorId],[HasShoppingCartItems],[RequireReLogin],[FailedLoginAttempts],[CannotLoginUntilDateUtc],[Active],[Deleted],[IsSystemAccount],[SystemName],
                            [LastIpAddress],[CreatedOnUtc],[LastLoginDateUtc],[LastActivityDateUtc],[RegisteredInStoreId],[BillingAddress_Id],[ShippingAddress_Id])
                            VALUES   
                            (@customerguid, @username, @email, @emailtorevalidate, @admincomment, @istaxexempt,@affiliated,@vendorid,@hasshoppingitem,@requirerelogin,@failedloginattempts,
                            @cannotloginunitdateutc,@active,@deleted,@issystemaccount,@systemname,@lastipaddtress,@createdonutc,@lastlogindateutc,@lastactivitydateutc,@registeredinstoreid,
                            @billingaddressid,@shippingaddressid) SELECT CAST(SCOPE_IDENTITY() as int)";
            var param = new
            {
                customerguid = Model.CustomerGuid,
                username = Model.Username,
                email = Model.Email,
                emailtorevalidate = Model.EmailToRevaildate,
                admincomment = Model.AdminComment,
                istaxexempt = Model.IsTaxExempt,
                affiliated = Model.AffiliateId,
                vendorid = Model.VendorId,
                hasshoppingitem = Model.HasShoppingCartItems,
                requirerelogin = Model.RequireReLogin,
                failedloginattempts = Model.FailedLoginAttempts,
                cannotloginunitdateutc = Model.CannotLoginUntilDate,
                active = Model.Active,
                deleted = Model.Deleted,
                issystemaccount = Model.IsSystemAccount,
                systemname = Model.SystemName,
                lastipaddtress = Model.LastIpAddress,
                createdonutc = Model.CreatedDate,
                lastlogindateutc = Model.LastLoginDate,
                lastactivitydateutc = Model.LastActivityDate,
                registeredinstoreid = Model.RegisteredInStoreId,
                billingaddressid = Model.BillingAddress_Id,
                shippingaddressid = Model.ShippingAddress_Id
            };

            var savedCustomer = _customerRepository.Add(query, param);
            Model.Id = (int)savedCustomer;

            //Encrypt Password
            var saltKey = CreateSaltKey(5);
            string PasswordSalt = saltKey;
            string encodedData = CreatePasswordHash(Model.Password, saltKey);

            string query1 = @"INSERT INTO [dbo].[CustomerPassword]([CustomerId],[Password],[PasswordFormatId],[PasswordSalt],[CreatedOnUtc])
                            VALUES
                            (@customerid, @password,@formatid,@passwordsalt,@createdonutc)";

            var parameters = new
            {
                customerid = savedCustomer,
                password = encodedData,
                formatid = 1,
                passwordsalt = PasswordSalt,
                createdonutc = DateTime.Now
            };
            _custPasswordRepository.Add(query1, parameters);
            return GetUserDetails(Model.Username, Model.Password);
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

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                //Encrypt Password
                var saltKey = CreateSaltKey(5);
                string PasswordSalt = saltKey;
                string encodedData = CreatePasswordHash(model.Password, saltKey);

                string query1 = @"update [dbo].[CustomerPassword] set [Password]=@password,[PasswordFormatId]=@formatid,[PasswordSalt]=@passwordsalt,[CreatedOnUtc]=@createdonutc
                                    where [CustomerId]=@customerid";

                var parameters = new
                {
                    customerid = model.Id,
                    password = encodedData,
                    formatid = 1,
                    passwordsalt = PasswordSalt,
                    createdonutc = DateTime.Now
                };
                _custPasswordRepository.Update(query1, parameters);
            }

            return model.Id;
        }


        public string ForgetPassword(string Email)
        {
            string Query = @"select * from Customer where Email = @email ";
            var param = new { email = Email };
            var Customer = _customerRepository.Get(Query, param);
            if (Customer != null)
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                string FromEmail = ConfigurationManager.AppSettings["FromEmailAddresss"];
                string Password = ConfigurationManager.AppSettings["Password"];

                mail.From = new MailAddress(FromEmail);
                mail.To.Add(Email);
                mail.Subject = "Password Recovery Maail";
                mail.Body = "";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(FromEmail, Password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                return "Email Has Been Sent";
            }
            else
            {
                return "Email Not Found";
            }
        }

        public Customer GetAccountDetails(string UserName)
        {
            string query = @"select * from Customer Where Username=@username";
            var param = new { username = UserName };
            var Data = _customerRepository.Get(query, param);
            return Data;
        }

        public List<AddressViewModel> GetShippingOrBillingAddress(int CustomerID)
        {
            string Query = @"select Address.City,Address.Address1,Address.Address2,Address.ZipPostalCode from Address 
                             join Customer on Customer.ShippingAddress_Id = Address.Id where Customer.Id = @id";
            var param = new { id = CustomerID };
            var Data = _AddressRepository.Get<AddressViewModel>(Query, param).ToList();
            return Data;
        }


        public bool PasswordsMatch(CustomerPassword customerPassword, string enteredPassword)
        {
            if (customerPassword == null || string.IsNullOrEmpty(enteredPassword))
                return false;

            var savedPassword = string.Empty;
            switch ((PasswordFormat)customerPassword.PasswordFormatId)
            {
                case PasswordFormat.Clear:
                    savedPassword = enteredPassword;
                    break;
                case PasswordFormat.Encrypted:
                    //savedPassword = _encryptionService.EncryptText(enteredPassword);
                    break;
                case PasswordFormat.Hashed:
                    savedPassword = CreatePasswordHash(enteredPassword, customerPassword.PasswordSalt);
                    break;
            }
            return customerPassword.Password.Equals(savedPassword);
        }

        public CustomerPassword GetCurrentPassword(int customerId)
        {
            if (customerId == 0)
                return null;

            return GetCustomerPasswords(customerId, passwordsToReturn: 1).FirstOrDefault();
        }

        public virtual IList<CustomerPassword> GetCustomerPasswords(int? customerId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null)
        {
            string query = @"select * from customerpassword where customerid=@customerid";
            var param = new { customerid = customerId };
            return _custPasswordRepository.GetAll(query, param).ToList();
        }

        public List<AddressViewModel> GetAddress(int CustomerID)
        {
            List<AddressViewModel> AddressModel = new List<AddressViewModel>();
            string Query = @"select Address_Id from CustomerAddresses where Customer_Id = @custid";
            var param = new { custid = CustomerID };
            var Addrseeid = _customerRepository.Get<int>(Query, param);

            string AddressQuery = @"select sp.Name as State,c.Name as Country,a.* from Address a
                                    join StateProvince sp on a.StateProvinceId = sp.Id
                                    join Country c on sp.CountryId = c.Id
                                    where a.id in @addressid";
            var parameters = new { addressid = Addrseeid };
            AddressModel = _AddressRepository.Get<AddressViewModel>(AddressQuery, parameters).ToList();
            return AddressModel;
        }

        public AddressViewModel GetSingleAddress(int AddressID)
        {
            string AddressQuery = @"select * from Address where id= @addressid";
            var parameters = new { addressid = AddressID };
            return  _AddressRepository.Get<AddressViewModel>(AddressQuery, parameters).FirstOrDefault();             
        }

        public object AddAddress(AddressViewModel Model)
        {
            string Query = @"INSERT INTO [dbo].[Address]([FirstName],[LastName],[Email],[Company],[CountryId],[StateProvinceId],[City],[Address1],[Address2],
                           [ZipPostalCode],[PhoneNumber],[FaxNumber],[CreatedOnUtc])
                           VALUES
                           (@firstname,@lastname,@email,@company,@countryid,@state,@city,@address1,@address2,@zipcode,@phonenumber,@faxnumber,getdate());
                           SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var Parameters = new
            {
                firstname = Model.FirstName,
                lastname = Model.LastName,
                email = Model.Email,
                company = Model.Company,
                countryid = Model.CountryId,
                state = Model.StateprovinceId,
                city = Model.City,
                address1 = Model.Address1,
                address2 = Model.Address2,
                zipcode = Model.ZipPostalCode,
                phonenumber = Model.PhoneNumber,
                faxnumber = Model.FaxNumber,
            };

            var Addressid =  _AddressRepository.Add(Query, Parameters);

            string Query1 = @"INSERT INTO [dbo].[CustomerAddresses]([Customer_Id],[Address_Id]) VALUES (@custid,@addressid)";
            var param = new
            {
                custid= Model.CustomerID,
                addressid = Addressid
            };
            return _CustAddressRepository.Add(Query1,param);
        }

        public object UpdateAddress(AddressViewModel Model)
        {

            string Query = @"update Address set FirstName = @firstname,LastName = @lastname , Email= @email,Company=@company,CountryId = @countryid ,StateProvinceId = @state ,
                           City =@city,Address1 = @address1,Address2= @address2,ZipPostalCode= @zipcode,PhoneNumber = @phonenumber,FaxNumber= @faxnumber where Id = @addressid";
            var Parameters = new
            {
                firstname = Model.FirstName,
                lastname = Model.LastName,
                email = Model.Email,
                company = Model.Company,
                countryid = Model.CountryId,
                state = Model.StateprovinceId,
                city = Model.City,
                address1 = Model.Address1,
                address2 = Model.Address2,
                zipcode = Model.ZipPostalCode,
                phonenumber = Model.PhoneNumber,
                faxnumber = Model.FaxNumber,
                addressid = Model.Id
            };
            return _AddressRepository.Update(Query, Parameters);
        }

        public object DeleteAddress(int AddressID)
        {
            string Query = @"delete from Address where ID = @addid";
            var pram = new { addid = AddressID };
            return _AddressRepository.Delete(Query, pram);
        }

        #region Encrypt Password
        public virtual string CreateSaltKey(int size)
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                var buff = new byte[size];
                provider.GetBytes(buff);

                return Convert.ToBase64String(buff);
            }
        }

        public virtual string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        {
            return CreateHash(Encoding.UTF8.GetBytes(String.Concat(password, saltkey)), passwordFormat);
        }

        public virtual string CreateHash(byte[] data, string hashAlgorithm = "SHA1")
        {
            if (String.IsNullOrEmpty(hashAlgorithm))
                hashAlgorithm = "SHA1";

            var algorithm = HashAlgorithm.Create(hashAlgorithm);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name");

            var hashByteArray = algorithm.ComputeHash(data);
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }
        #endregion

    }
}

