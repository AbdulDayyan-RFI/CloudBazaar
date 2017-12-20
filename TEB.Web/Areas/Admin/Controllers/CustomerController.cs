using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Core.Mapping;
using TEB.Core.ViewModel;
using TEB.Web.Controllers;
using TEB.Core.ViewModel;
using System.Text;
using System.Web;

namespace TEB.Web.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Admin/Customer

        //public CustomerController(ICustomerService customerService)
        //{
        //    this._customerService = customerService;
        //}

        //#region Customers

        //public virtual ActionResult Index()
        //{
        //    return RedirectToAction("List");
        //}

        //[HttpPost]
        //public async Task<ActionResult> List()
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedView();

        //    //load registered customers by default
        //    var defaultRoleIds = new List<int> { _customerService.GetCustomerRoleBySystemName(SystemCustomerRoleNames.Registered).Id };
        //    var model = new CustomerListModel
        //    {
        //        UsernamesEnabled = _customerSettings.UsernamesEnabled,
        //        DateOfBirthEnabled = _customerSettings.DateOfBirthEnabled,
        //        CompanyEnabled = _customerSettings.CompanyEnabled,
        //        PhoneEnabled = _customerSettings.PhoneEnabled,
        //        ZipPostalCodeEnabled = _customerSettings.ZipPostalCodeEnabled,
        //        SearchCustomerRoleIds = defaultRoleIds,
        //    };
        //    var allRoles = _customerService.GetAllCustomerRoles(true);
        //    foreach (var role in allRoles)
        //    {
        //        model.AvailableCustomerRoles.Add(new SelectListItem
        //        {
        //            Text = role.Name,
        //            Value = role.Id.ToString(),
        //            Selected = defaultRoleIds.Any(x => x == role.Id)
        //        });
        //    }

        //    TEBApiResponse apiResponse = await Get<Customer>("/Customer/GetAllCustomersList");

        //    return View(model);
        //}

        //[HttpPost]
        //public virtual ActionResult CustomerList(DataSourceRequest command, CustomerListModel model,
        //    [ModelBinder(typeof(CommaSeparatedModelBinder))] int[] searchCustomerRoleIds)
        //{
        //    //we use own own binder for searchCustomerRoleIds property 
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedKendoGridJson();

        //    var searchDayOfBirth = 0;
        //    int searchMonthOfBirth = 0;
        //    if (!String.IsNullOrWhiteSpace(model.SearchDayOfBirth))
        //        searchDayOfBirth = Convert.ToInt32(model.SearchDayOfBirth);
        //    if (!String.IsNullOrWhiteSpace(model.SearchMonthOfBirth))
        //        searchMonthOfBirth = Convert.ToInt32(model.SearchMonthOfBirth);

        //    var customers = _customerService.GetAllCustomers(
        //        customerRoleIds: searchCustomerRoleIds,
        //        email: model.SearchEmail,
        //        username: model.SearchUsername,
        //        firstName: model.SearchFirstName,
        //        lastName: model.SearchLastName,
        //        dayOfBirth: searchDayOfBirth,
        //        monthOfBirth: searchMonthOfBirth,
        //        company: model.SearchCompany,
        //        phone: model.SearchPhone,
        //        zipPostalCode: model.SearchZipPostalCode,
        //        ipAddress: model.SearchIpAddress,
        //        loadOnlyWithShoppingCart: false,
        //        pageIndex: command.Page - 1,
        //        pageSize: command.PageSize);
        //    var gridModel = new DataSourceResult
        //    {
        //        Data = customers.Select(PrepareCustomerModelForList),
        //        Total = customers.TotalCount
        //    };

        //    return Json(gridModel);
        //}

        //public virtual ActionResult Create()
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedView();

        //    var model = new CustomerModel();
        //    PrepareCustomerModel(model, null, false);
        //    //default value
        //    model.Active = true;
        //    return View(model);
        //}

        //[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        //[FormValueRequired("save", "save-continue")]
        //[ValidateInput(false)]
        //public virtual ActionResult Create(CustomerViewModel model, bool continueEditing, FormCollection form)
        //{
        //    //if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //    //    return AccessDeniedView();

        //    if (!String.IsNullOrWhiteSpace(model.Email))
        //    {
        //        var cust2 = _customerService.GetCustomerByEmail(model.Email);
        //        if (cust2 != null)
        //            ModelState.AddModelError("", "Email is already registered");
        //    }
        //    if (!String.IsNullOrWhiteSpace(model.Username) & _customerSettings.UsernamesEnabled)
        //    {
        //        var cust2 = _customerService.GetCustomerByUsername(model.Username);
        //        if (cust2 != null)
        //            ModelState.AddModelError("", "Username is already registered");
        //    }

        //    //validate customer roles
        //    var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
        //    var newCustomerRoles = new List<CustomerRole>();
        //    foreach (var customerRole in allCustomerRoles)
        //        if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
        //            newCustomerRoles.Add(customerRole);
        //    var customerRolesError = ValidateCustomerRoles(newCustomerRoles);
        //    if (!String.IsNullOrEmpty(customerRolesError))
        //    {
        //        ModelState.AddModelError("", customerRolesError);
        //        ErrorNotification(customerRolesError, false);
        //    }

        //    // Ensure that valid email address is entered if Registered role is checked to avoid registered customers with empty email address
        //    if (newCustomerRoles.Any() && newCustomerRoles.FirstOrDefault(c => c.SystemName == SystemCustomerRoleNames.Registered) != null && !CommonHelper.IsValidEmail(model.Email))
        //    {
        //        ModelState.AddModelError("", _localizationService.GetResource("Admin.Customers.Customers.ValidEmailRequiredRegisteredRole"));
        //        ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.ValidEmailRequiredRegisteredRole"), false);
        //    }

        //    //custom customer attributes
        //    var customerAttributesXml = ParseCustomCustomerAttributes(form);
        //    if (newCustomerRoles.Any() && newCustomerRoles.FirstOrDefault(c => c.SystemName == SystemCustomerRoleNames.Registered) != null)
        //    {
        //        var customerAttributeWarnings = _customerAttributeParser.GetAttributeWarnings(customerAttributesXml);
        //        foreach (var error in customerAttributeWarnings)
        //        {
        //            ModelState.AddModelError("", error);
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var customer = new Customer
        //        {
        //            CustomerGuid = Guid.NewGuid(),
        //            Email = model.Email,
        //            Username = model.Username,
        //            VendorId = model.VendorId,
        //            AdminComment = model.AdminComment,
        //            IsTaxExempt = model.IsTaxExempt,
        //            Active = model.Active,
        //            CreatedOnUtc = DateTime.UtcNow,
        //            LastActivityDateUtc = DateTime.UtcNow,
        //            RegisteredInStoreId = _storeContext.CurrentStore.Id
        //        };
        //        _customerService.InsertCustomer(customer);

        //        //form fields
        //        if (_dateTimeSettings.AllowCustomersToSetTimeZone)
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.TimeZoneId, model.TimeZoneId);
        //        if (_customerSettings.GenderEnabled)
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Gender, model.Gender);
        //        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, model.FirstName);
        //        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, model.LastName);
        //        if (_customerSettings.DateOfBirthEnabled)
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.DateOfBirth, model.DateOfBirth);
        //        if (_customerSettings.CompanyEnabled)
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Company, model.Company);
        //        if (_customerSettings.StreetAddressEnabled)
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress, model.StreetAddress);
        //        if (_customerSettings.StreetAddress2Enabled)
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress2, model.StreetAddress2);
        //        if (_customerSettings.ZipPostalCodeEnabled)
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.ZipPostalCode, model.ZipPostalCode);
        //        if (_customerSettings.CityEnabled)
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.City, model.City);
        //        if (_customerSettings.CountryEnabled)
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CountryId, model.CountryId);
        //        if (_customerSettings.CountryEnabled && _customerSettings.StateProvinceEnabled)
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StateProvinceId, model.StateProvinceId);
        //        if (_customerSettings.PhoneEnabled)
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, model.Phone);
        //        if (_customerSettings.FaxEnabled)
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Fax, model.Fax);

        //        //custom customer attributes
        //        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CustomCustomerAttributes, customerAttributesXml);


        //        //newsletter subscriptions
        //        if (!String.IsNullOrEmpty(customer.Email))
        //        {
        //            var allStores = _storeService.GetAllStores();
        //            foreach (var store in allStores)
        //            {
        //                var newsletterSubscription = _newsLetterSubscriptionService
        //                    .GetNewsLetterSubscriptionByEmailAndStoreId(customer.Email, store.Id);
        //                if (model.SelectedNewsletterSubscriptionStoreIds != null &&
        //                    model.SelectedNewsletterSubscriptionStoreIds.Contains(store.Id))
        //                {
        //                    //subscribed
        //                    if (newsletterSubscription == null)
        //                    {
        //                        _newsLetterSubscriptionService.InsertNewsLetterSubscription(new NewsLetterSubscription
        //                        {
        //                            NewsLetterSubscriptionGuid = Guid.NewGuid(),
        //                            Email = customer.Email,
        //                            Active = true,
        //                            StoreId = store.Id,
        //                            CreatedOnUtc = DateTime.UtcNow
        //                        });
        //                    }
        //                }
        //                else
        //                {
        //                    //not subscribed
        //                    if (newsletterSubscription != null)
        //                    {
        //                        _newsLetterSubscriptionService.DeleteNewsLetterSubscription(newsletterSubscription);
        //                    }
        //                }
        //            }
        //        }

        //        //password
        //        if (!String.IsNullOrWhiteSpace(model.Password))
        //        {
        //            var changePassRequest = new ChangePasswordRequest(model.Email, false, _customerSettings.DefaultPasswordFormat, model.Password);
        //            var changePassResult = _customerRegistrationService.ChangePassword(changePassRequest);
        //            if (!changePassResult.Success)
        //            {
        //                foreach (var changePassError in changePassResult.Errors)
        //                    ErrorNotification(changePassError);
        //            }
        //        }

        //        //customer roles
        //        foreach (var customerRole in newCustomerRoles)
        //        {
        //            //ensure that the current customer cannot add to "Administrators" system role if he's not an admin himself
        //            if (customerRole.SystemName == SystemCustomerRoleNames.Administrators &&
        //                !_workContext.CurrentCustomer.IsAdmin())
        //                continue;

        //            customer.CustomerRoles.Add(customerRole);
        //        }
        //        _customerService.UpdateCustomer(customer);


        //        //ensure that a customer with a vendor associated is not in "Administrators" role
        //        //otherwise, he won't have access to other functionality in admin area
        //        if (customer.IsAdmin() && customer.VendorId > 0)
        //        {
        //            customer.VendorId = 0;
        //            _customerService.UpdateCustomer(customer);
        //            ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.AdminCouldNotbeVendor"));
        //        }

        //        //ensure that a customer in the Vendors role has a vendor account associated.
        //        //otherwise, he will have access to ALL products
        //        if (customer.IsVendor() && customer.VendorId == 0)
        //        {
        //            var vendorRole = customer
        //                .CustomerRoles
        //                .FirstOrDefault(x => x.SystemName == SystemCustomerRoleNames.Vendors);
        //            customer.CustomerRoles.Remove(vendorRole);
        //            _customerService.UpdateCustomer(customer);
        //            ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.CannotBeInVendoRoleWithoutVendorAssociated"));
        //        }

        //        //activity log
        //        _customerActivityService.InsertActivity("AddNewCustomer", _localizationService.GetResource("ActivityLog.AddNewCustomer"), customer.Id);

        //        SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.Added"));

        //        if (continueEditing)
        //        {
        //            //selected tab
        //            SaveSelectedTabName();

        //            return RedirectToAction("Edit", new { id = customer.Id });
        //        }
        //        return RedirectToAction("List");
        //    }

        //    //If we got this far, something failed, redisplay form
        //    PrepareCustomerModel(model, null, true);
        //    return View(model);
        //}

        //public virtual ActionResult Edit(int id)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedView();

        //    var customer = _customerService.GetCustomerById(id);
        //    if (customer == null || customer.Deleted)
        //        //No customer found with the specified id
        //        return RedirectToAction("List");

        //    var model = new CustomerModel();
        //    PrepareCustomerModel(model, customer, false);
        //    return View(model);
        //}

        //[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        //[FormValueRequired("save", "save-continue")]
        //[ValidateInput(false)]
        //public virtual ActionResult Edit(CustomerModel model, bool continueEditing, FormCollection form)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedView();

        //    var customer = _customerService.GetCustomerById(model.Id);
        //    if (customer == null || customer.Deleted)
        //        //No customer found with the specified id
        //        return RedirectToAction("List");

        //    //validate customer roles
        //    var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
        //    var newCustomerRoles = new List<CustomerRole>();
        //    foreach (var customerRole in allCustomerRoles)
        //        if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
        //            newCustomerRoles.Add(customerRole);
        //    var customerRolesError = ValidateCustomerRoles(newCustomerRoles);
        //    if (!String.IsNullOrEmpty(customerRolesError))
        //    {
        //        ModelState.AddModelError("", customerRolesError);
        //        ErrorNotification(customerRolesError, false);
        //    }

        //    // Ensure that valid email address is entered if Registered role is checked to avoid registered customers with empty email address
        //    if (newCustomerRoles.Any() && newCustomerRoles.FirstOrDefault(c => c.SystemName == SystemCustomerRoleNames.Registered) != null && !CommonHelper.IsValidEmail(model.Email))
        //    {
        //        ModelState.AddModelError("", _localizationService.GetResource("Admin.Customers.Customers.ValidEmailRequiredRegisteredRole"));
        //        ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.ValidEmailRequiredRegisteredRole"), false);
        //    }

        //    //custom customer attributes
        //    var customerAttributesXml = ParseCustomCustomerAttributes(form);
        //    if (newCustomerRoles.Any() && newCustomerRoles.FirstOrDefault(c => c.SystemName == SystemCustomerRoleNames.Registered) != null)
        //    {
        //        var customerAttributeWarnings = _customerAttributeParser.GetAttributeWarnings(customerAttributesXml);
        //        foreach (var error in customerAttributeWarnings)
        //        {
        //            ModelState.AddModelError("", error);
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            customer.AdminComment = model.AdminComment;
        //            customer.IsTaxExempt = model.IsTaxExempt;

        //            //prevent deactivation of the last active administrator
        //            if (!customer.IsAdmin() || model.Active || SecondAdminAccountExists(customer))
        //                customer.Active = model.Active;
        //            else
        //                ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.AdminAccountShouldExists.Deactivate"));

        //            //email
        //            if (!String.IsNullOrWhiteSpace(model.Email))
        //            {
        //                _customerRegistrationService.SetEmail(customer, model.Email, false);
        //            }
        //            else
        //            {
        //                customer.Email = model.Email;
        //            }

        //            //username
        //            if (_customerSettings.UsernamesEnabled)
        //            {
        //                if (!String.IsNullOrWhiteSpace(model.Username))
        //                {
        //                    _customerRegistrationService.SetUsername(customer, model.Username);
        //                }
        //                else
        //                {
        //                    customer.Username = model.Username;
        //                }
        //            }

        //            //VAT number
        //            if (_taxSettings.EuVatEnabled)
        //            {
        //                var prevVatNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.VatNumber);

        //                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.VatNumber, model.VatNumber);
        //                //set VAT number status
        //                if (!String.IsNullOrEmpty(model.VatNumber))
        //                {
        //                    if (!model.VatNumber.Equals(prevVatNumber, StringComparison.InvariantCultureIgnoreCase))
        //                    {
        //                        _genericAttributeService.SaveAttribute(customer,
        //                            SystemCustomerAttributeNames.VatNumberStatusId,
        //                            (int)_taxService.GetVatNumberStatus(model.VatNumber));
        //                    }
        //                }
        //                else
        //                {
        //                    _genericAttributeService.SaveAttribute(customer,
        //                        SystemCustomerAttributeNames.VatNumberStatusId,
        //                        (int)VatNumberStatus.Empty);
        //                }
        //            }

        //            //vendor
        //            customer.VendorId = model.VendorId;

        //            //form fields
        //            if (_dateTimeSettings.AllowCustomersToSetTimeZone)
        //                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.TimeZoneId, model.TimeZoneId);
        //            if (_customerSettings.GenderEnabled)
        //                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Gender, model.Gender);
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, model.FirstName);
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, model.LastName);
        //            if (_customerSettings.DateOfBirthEnabled)
        //                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.DateOfBirth, model.DateOfBirth);
        //            if (_customerSettings.CompanyEnabled)
        //                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Company, model.Company);
        //            if (_customerSettings.StreetAddressEnabled)
        //                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress, model.StreetAddress);
        //            if (_customerSettings.StreetAddress2Enabled)
        //                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress2, model.StreetAddress2);
        //            if (_customerSettings.ZipPostalCodeEnabled)
        //                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.ZipPostalCode, model.ZipPostalCode);
        //            if (_customerSettings.CityEnabled)
        //                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.City, model.City);
        //            if (_customerSettings.CountryEnabled)
        //                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CountryId, model.CountryId);
        //            if (_customerSettings.CountryEnabled && _customerSettings.StateProvinceEnabled)
        //                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StateProvinceId, model.StateProvinceId);
        //            if (_customerSettings.PhoneEnabled)
        //                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, model.Phone);
        //            if (_customerSettings.FaxEnabled)
        //                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Fax, model.Fax);

        //            //custom customer attributes
        //            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CustomCustomerAttributes, customerAttributesXml);

        //            //newsletter subscriptions
        //            if (!String.IsNullOrEmpty(customer.Email))
        //            {
        //                var allStores = _storeService.GetAllStores();
        //                foreach (var store in allStores)
        //                {
        //                    var newsletterSubscription = _newsLetterSubscriptionService
        //                        .GetNewsLetterSubscriptionByEmailAndStoreId(customer.Email, store.Id);
        //                    if (model.SelectedNewsletterSubscriptionStoreIds != null &&
        //                        model.SelectedNewsletterSubscriptionStoreIds.Contains(store.Id))
        //                    {
        //                        //subscribed
        //                        if (newsletterSubscription == null)
        //                        {
        //                            _newsLetterSubscriptionService.InsertNewsLetterSubscription(new NewsLetterSubscription
        //                            {
        //                                NewsLetterSubscriptionGuid = Guid.NewGuid(),
        //                                Email = customer.Email,
        //                                Active = true,
        //                                StoreId = store.Id,
        //                                CreatedOnUtc = DateTime.UtcNow
        //                            });
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //not subscribed
        //                        if (newsletterSubscription != null)
        //                        {
        //                            _newsLetterSubscriptionService.DeleteNewsLetterSubscription(newsletterSubscription);
        //                        }
        //                    }
        //                }
        //            }


        //            //customer roles
        //            foreach (var customerRole in allCustomerRoles)
        //            {
        //                //ensure that the current customer cannot add/remove to/from "Administrators" system role
        //                //if he's not an admin himself
        //                if (customerRole.SystemName == SystemCustomerRoleNames.Administrators &&
        //                    !_workContext.CurrentCustomer.IsAdmin())
        //                    continue;

        //                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
        //                {
        //                    //new role
        //                    if (customer.CustomerRoles.Count(cr => cr.Id == customerRole.Id) == 0)
        //                        customer.CustomerRoles.Add(customerRole);
        //                }
        //                else
        //                {
        //                    //prevent attempts to delete the administrator role from the user, if the user is the last active administrator
        //                    if (customerRole.SystemName == SystemCustomerRoleNames.Administrators && !SecondAdminAccountExists(customer))
        //                    {
        //                        ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.AdminAccountShouldExists.DeleteRole"));
        //                        continue;
        //                    }

        //                    //remove role
        //                    if (customer.CustomerRoles.Count(cr => cr.Id == customerRole.Id) > 0)
        //                        customer.CustomerRoles.Remove(customerRole);
        //                }
        //            }
        //            _customerService.UpdateCustomer(customer);


        //            //ensure that a customer with a vendor associated is not in "Administrators" role
        //            //otherwise, he won't have access to the other functionality in admin area
        //            if (customer.IsAdmin() && customer.VendorId > 0)
        //            {
        //                customer.VendorId = 0;
        //                _customerService.UpdateCustomer(customer);
        //                ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.AdminCouldNotbeVendor"));
        //            }

        //            //ensure that a customer in the Vendors role has a vendor account associated.
        //            //otherwise, he will have access to ALL products
        //            if (customer.IsVendor() && customer.VendorId == 0)
        //            {
        //                var vendorRole = customer
        //                    .CustomerRoles
        //                    .FirstOrDefault(x => x.SystemName == SystemCustomerRoleNames.Vendors);
        //                customer.CustomerRoles.Remove(vendorRole);
        //                _customerService.UpdateCustomer(customer);
        //                ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.CannotBeInVendoRoleWithoutVendorAssociated"));
        //            }


        //            //activity log
        //            _customerActivityService.InsertActivity("EditCustomer", _localizationService.GetResource("ActivityLog.EditCustomer"), customer.Id);

        //            SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.Updated"));
        //            if (continueEditing)
        //            {
        //                //selected tab
        //                SaveSelectedTabName();

        //                return RedirectToAction("Edit", new { id = customer.Id });
        //            }
        //            return RedirectToAction("List");
        //        }
        //        catch (Exception exc)
        //        {
        //            ErrorNotification(exc.Message, false);
        //        }
        //    }


        //    //If we got this far, something failed, redisplay form
        //    PrepareCustomerModel(model, customer, true);
        //    return View(model);
        //}

        //[HttpPost, ActionName("Edit")]
        //[FormValueRequired("changepassword")]
        //public virtual ActionResult ChangePassword(CustomerModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedView();

        //    var customer = _customerService.GetCustomerById(model.Id);
        //    if (customer == null)
        //        //No customer found with the specified id
        //        return RedirectToAction("List");

        //    //ensure that the current customer cannot change passwords of "Administrators" if he's not an admin himself
        //    if (customer.IsAdmin() && !_workContext.CurrentCustomer.IsAdmin())
        //    {
        //        ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.OnlyAdminCanChangePassword"));
        //        return RedirectToAction("Edit", new { id = customer.Id });
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var changePassRequest = new ChangePasswordRequest(model.Email,
        //            false, _customerSettings.DefaultPasswordFormat, model.Password);
        //        var changePassResult = _customerRegistrationService.ChangePassword(changePassRequest);
        //        if (changePassResult.Success)
        //            SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.PasswordChanged"));
        //        else
        //            foreach (var error in changePassResult.Errors)
        //                ErrorNotification(error);
        //    }

        //    return RedirectToAction("Edit", new { id = customer.Id });
        //}

        //[HttpPost, ActionName("Edit")]
        //[FormValueRequired("markVatNumberAsValid")]
        //public virtual ActionResult MarkVatNumberAsValid(CustomerModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedView();

        //    var customer = _customerService.GetCustomerById(model.Id);
        //    if (customer == null)
        //        //No customer found with the specified id
        //        return RedirectToAction("List");

        //    _genericAttributeService.SaveAttribute(customer,
        //        SystemCustomerAttributeNames.VatNumberStatusId,
        //        (int)VatNumberStatus.Valid);

        //    return RedirectToAction("Edit", new { id = customer.Id });
        //}

        //[HttpPost, ActionName("Edit")]
        //[FormValueRequired("markVatNumberAsInvalid")]
        //public virtual ActionResult MarkVatNumberAsInvalid(CustomerModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedView();

        //    var customer = _customerService.GetCustomerById(model.Id);
        //    if (customer == null)
        //        //No customer found with the specified id
        //        return RedirectToAction("List");

        //    _genericAttributeService.SaveAttribute(customer,
        //        SystemCustomerAttributeNames.VatNumberStatusId,
        //        (int)VatNumberStatus.Invalid);

        //    return RedirectToAction("Edit", new { id = customer.Id });
        //}

        //[HttpPost, ActionName("Edit")]
        //[FormValueRequired("remove-affiliate")]
        //public virtual ActionResult RemoveAffiliate(CustomerModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedView();

        //    var customer = _customerService.GetCustomerById(model.Id);
        //    if (customer == null)
        //        //No customer found with the specified id
        //        return RedirectToAction("List");

        //    customer.AffiliateId = 0;
        //    _customerService.UpdateCustomer(customer);

        //    return RedirectToAction("Edit", new { id = customer.Id });
        //}

        //[HttpPost]
        //public virtual ActionResult Delete(int id)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedView();

        //    var customer = _customerService.GetCustomerById(id);
        //    if (customer == null)
        //        //No customer found with the specified id
        //        return RedirectToAction("List");

        //    try
        //    {
        //        //prevent attempts to delete the user, if it is the last active administrator
        //        if (customer.IsAdmin() && !SecondAdminAccountExists(customer))
        //        {
        //            ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.AdminAccountShouldExists.DeleteAdministrator"));
        //            return RedirectToAction("Edit", new { id = customer.Id });
        //        }

        //        //ensure that the current customer cannot delete "Administrators" if he's not an admin himself
        //        if (customer.IsAdmin() && !_workContext.CurrentCustomer.IsAdmin())
        //        {
        //            ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.OnlyAdminCanDeleteAdmin"));
        //            return RedirectToAction("Edit", new { id = customer.Id });
        //        }

        //        //delete
        //        _customerService.DeleteCustomer(customer);

        //        //remove newsletter subscription (if exists)
        //        foreach (var store in _storeService.GetAllStores())
        //        {
        //            var subscription = _newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreId(customer.Email, store.Id);
        //            if (subscription != null)
        //                _newsLetterSubscriptionService.DeleteNewsLetterSubscription(subscription);
        //        }

        //        //activity log
        //        _customerActivityService.InsertActivity("DeleteCustomer", _localizationService.GetResource("ActivityLog.DeleteCustomer"), customer.Id);

        //        SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.Deleted"));
        //        return RedirectToAction("List");
        //    }
        //    catch (Exception exc)
        //    {
        //        ErrorNotification(exc.Message);
        //        return RedirectToAction("Edit", new { id = customer.Id });
        //    }
        //}

        //[HttpPost, ActionName("Edit")]
        //[FormValueRequired("impersonate")]
        //public virtual ActionResult Impersonate(int id)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.AllowCustomerImpersonation))
        //        return AccessDeniedView();

        //    var customer = _customerService.GetCustomerById(id);
        //    if (customer == null)
        //        //No customer found with the specified id
        //        return RedirectToAction("List");

        //    //ensure that a non-admin user cannot impersonate as an administrator
        //    //otherwise, that user can simply impersonate as an administrator and gain additional administrative privileges
        //    if (!_workContext.CurrentCustomer.IsAdmin() && customer.IsAdmin())
        //    {
        //        ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.NonAdminNotImpersonateAsAdminError"));
        //        return RedirectToAction("Edit", customer.Id);
        //    }

        //    //activity log
        //    _customerActivityService.InsertActivity("Impersonation.Started", _localizationService.GetResource("ActivityLog.Impersonation.Started.StoreOwner"), customer.Email, customer.Id);
        //    _customerActivityService.InsertActivity(customer, "Impersonation.Started", _localizationService.GetResource("ActivityLog.Impersonation.Started.Customer"), _workContext.CurrentCustomer.Email, _workContext.CurrentCustomer.Id);

        //    //ensure login is not required
        //    customer.RequireReLogin = false;
        //    _customerService.UpdateCustomer(customer);
        //    _genericAttributeService.SaveAttribute<int?>(_workContext.CurrentCustomer, SystemCustomerAttributeNames.ImpersonatedCustomerId, customer.Id);

        //    return RedirectToAction("Index", "Home", new { area = "" });
        //}

        //[HttpPost, ActionName("Edit")]
        //[FormValueRequired("send-welcome-message")]
        //public virtual ActionResult SendWelcomeMessage(CustomerModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedView();

        //    var customer = _customerService.GetCustomerById(model.Id);
        //    if (customer == null)
        //        //No customer found with the specified id
        //        return RedirectToAction("List");

        //    _workflowMessageService.SendCustomerWelcomeMessage(customer, _workContext.WorkingLanguage.Id);

        //    SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.SendWelcomeMessage.Success"));

        //    return RedirectToAction("Edit", new { id = customer.Id });
        //}

        //[HttpPost, ActionName("Edit")]
        //[FormValueRequired("resend-activation-message")]
        //public virtual ActionResult ReSendActivationMessage(CustomerModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedView();

        //    var customer = _customerService.GetCustomerById(model.Id);
        //    if (customer == null)
        //        //No customer found with the specified id
        //        return RedirectToAction("List");

        //    //email validation message
        //    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.AccountActivationToken, Guid.NewGuid().ToString());
        //    _workflowMessageService.SendCustomerEmailValidationMessage(customer, _workContext.WorkingLanguage.Id);

        //    SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.ReSendActivationMessage.Success"));

        //    return RedirectToAction("Edit", new { id = customer.Id });
        //}

        //public virtual ActionResult SendEmail(CustomerModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedView();

        //    var customer = _customerService.GetCustomerById(model.Id);
        //    if (customer == null)
        //        //No customer found with the specified id
        //        return RedirectToAction("List");

        //    try
        //    {
        //        if (String.IsNullOrWhiteSpace(customer.Email))
        //            throw new NopException("Customer email is empty");
        //        if (!CommonHelper.IsValidEmail(customer.Email))
        //            throw new NopException("Customer email is not valid");
        //        if (String.IsNullOrWhiteSpace(model.SendEmail.Subject))
        //            throw new NopException("Email subject is empty");
        //        if (String.IsNullOrWhiteSpace(model.SendEmail.Body))
        //            throw new NopException("Email body is empty");

        //        var emailAccount = _emailAccountService.GetEmailAccountById(_emailAccountSettings.DefaultEmailAccountId);
        //        if (emailAccount == null)
        //            emailAccount = _emailAccountService.GetAllEmailAccounts().FirstOrDefault();
        //        if (emailAccount == null)
        //            throw new NopException("Email account can't be loaded");
        //        var email = new QueuedEmail
        //        {
        //            Priority = QueuedEmailPriority.High,
        //            EmailAccountId = emailAccount.Id,
        //            FromName = emailAccount.DisplayName,
        //            From = emailAccount.Email,
        //            ToName = customer.GetFullName(),
        //            To = customer.Email,
        //            Subject = model.SendEmail.Subject,
        //            Body = model.SendEmail.Body,
        //            CreatedOnUtc = DateTime.UtcNow,
        //            DontSendBeforeDateUtc = (model.SendEmail.SendImmediately || !model.SendEmail.DontSendBeforeDate.HasValue) ?
        //                null : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.SendEmail.DontSendBeforeDate.Value)
        //        };
        //        _queuedEmailService.InsertQueuedEmail(email);
        //        SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.SendEmail.Queued"));
        //    }
        //    catch (Exception exc)
        //    {
        //        ErrorNotification(exc.Message);
        //    }

        //    return RedirectToAction("Edit", new { id = customer.Id });
        //}

        //public virtual ActionResult SendPm(CustomerModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
        //        return AccessDeniedView();

        //    var customer = _customerService.GetCustomerById(model.Id);
        //    if (customer == null)
        //        //No customer found with the specified id
        //        return RedirectToAction("List");

        //    try
        //    {
        //        if (!_forumSettings.AllowPrivateMessages)
        //            throw new NopException("Private messages are disabled");
        //        if (customer.IsGuest())
        //            throw new NopException("Customer should be registered");
        //        if (String.IsNullOrWhiteSpace(model.SendPm.Subject))
        //            throw new NopException("PM subject is empty");
        //        if (String.IsNullOrWhiteSpace(model.SendPm.Message))
        //            throw new NopException("PM message is empty");


        //        var privateMessage = new PrivateMessage
        //        {
        //            StoreId = _storeContext.CurrentStore.Id,
        //            ToCustomerId = customer.Id,
        //            FromCustomerId = _workContext.CurrentCustomer.Id,
        //            Subject = model.SendPm.Subject,
        //            Text = model.SendPm.Message,
        //            IsDeletedByAuthor = false,
        //            IsDeletedByRecipient = false,
        //            IsRead = false,
        //            CreatedOnUtc = DateTime.UtcNow
        //        };

        //        _forumService.InsertPrivateMessage(privateMessage);
        //        SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.SendPM.Sent"));
        //    }
        //    catch (Exception exc)
        //    {
        //        ErrorNotification(exc.Message);
        //    }

        //    return RedirectToAction("Edit", new { id = customer.Id });
        //}

        //#endregion

        #region Kareem

        public ActionResult Create()
        {
            var model = new CustomerViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CustomerViewModel model)
        {
            model.Username = model.FirstName + model.LastName;
            if (!String.IsNullOrWhiteSpace(model.Email))
            {
                TEBApiResponse apiResponse1 = await Get("/Customer/GetCustomerByEmail?email=" + model.Email.Trim());
                if (apiResponse1.Data != null)
                    ModelState.AddModelError("", "Email is already registered");
            }
            if (!String.IsNullOrWhiteSpace(model.Username))
            {
                TEBApiResponse apiResponse2 = await Get("/Customer/GetCustomerByUsername?userName=" + model.Username.Trim());
                if (apiResponse2.Data != null)
                    ModelState.AddModelError("", "Username is already registered");
            }

            if (ModelState.IsValid)
            {
                Customer customer = new Customer();
                customer = CustomerMapping.ViewToModel(model);
                if (model.Id == 0)
                {
                    TEBApiResponse apiResponse = await Post<Customer>("/Customer/InsertCustomer", customer);
                    model.Id = Convert.ToInt32(apiResponse.Data.ToString());
                }
                else {
                    TEBApiResponse apiResponse = await Post<Customer>("/Customer/UpdateCustomer", customer);
                    model.Id = 0;
                }
            }

            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            CustomerViewModel model = new CustomerViewModel();
            TEBApiResponse apiResponse = await Get("/Customer/GetCustomerById?Id=" + id);
            if (apiResponse.IsSuccess)
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>(Convert.ToString(apiResponse.Data));
                model = CustomerMapping.ModelToView(customer);

            }
            return View(model);
        }

        //[HttpPost]
        //public async Task<ActionResult> Edit(CustomerViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Customer
        //        Customer customer = new Customer();
        //        customer = CustomerMapping.ViewToModel(model);

        //        TEBApiResponse apiResponse = await Post<Customer>("/Customer/UpdateCustomer", customer);
        //        if (apiResponse.IsSuccess)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View(model);
        //}

        public async Task<ActionResult> Delete(int id)
        {
            TEBApiResponse apiResponse = await Delete("/Customer/DeleteCustomer?Id=" + id);
            return RedirectToAction("create");
        }

        #endregion
    }
}