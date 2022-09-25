using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Application.API.Controllers
{
    /// <summary>
    /// Store Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        /// <summary>
        /// CountryCode Header Name
        /// </summary>
        public const string CountryCodeHeaderName = "x-test-country-code";

        private readonly IRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="repository">IRepository repository</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor httpContextAccessor</param>
        public StoreController(IRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// GetStores
        /// </summary>
        /// <returns></returns>
        public IActionResult GetStores()
        {
            IActionResult actionResult = NoContent();
            var header = _httpContextAccessor.HttpContext.Request.Headers[CountryCodeHeaderName];
            if (string.IsNullOrWhiteSpace(header))
                actionResult = Unauthorized();
            else
                actionResult = (IActionResult)_repository.GetStores(x => x.CountryCode == header).ToList();

            return actionResult;
        }

        /// <summary>
        /// GetStore by Id
        /// </summary>
        /// <param name="storeId">int storeId</param>
        /// <param name="includeCustomers">bool includeCustomer = false</param>
        /// <returns></returns>
        public IActionResult GetStore(int storeId, bool includeCustomers = false)
        {
            IActionResult actionResult = NoContent();
            var header = _httpContextAccessor.HttpContext.Request.Headers[CountryCodeHeaderName];
            if (includeCustomers)
            {
                var customer = _repository.GetCustomers(storeId).ToList();
                if (customer != null && customer.Count > 0)
                    actionResult = Ok(customer);
                else
                    actionResult = NotFound();
            }
            if (storeId > 0)
            {
                var storeData = _repository.GetStores(x => x.StoreId == storeId).ToList();
                if (storeData == null)
                    actionResult = NotFound();
                else
                    actionResult = Ok(storeData.ToList());
            }
            if (!string.IsNullOrWhiteSpace(header))
            {
                var data = _repository.GetStores(x => x.CountryCode != header).ToList();
                if (data != null && data.Count > 0)
                    actionResult = Ok(data);
                else
                    actionResult = NotFound();
            }
            return actionResult;
        }

        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="customer">Customer customer</param>
        /// <returns></returns>
        public IActionResult CreateCustomer(Customer customer)
        {
            IActionResult actionResult = NoContent();
            var header = _httpContextAccessor.HttpContext.Request.Headers[CountryCodeHeaderName];
            if (customer == null)
                actionResult = BadRequest();
            else if (string.IsNullOrWhiteSpace(header))
                actionResult = Unauthorized();
            else
            {
                var addCustomer = _repository.AddCustomer(customer);
                actionResult = Ok(addCustomer);
            }
            return actionResult;
        }
    }

    /// <summary>
    /// IRepository
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// GetStores
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="includeCustomers">bool includeCustomers = false</param>
        /// <returns></returns>
        ICollection<Store> GetStores(Func<Store, bool> filter, bool includeCustomers = false);

        /// <summary>
        /// Get Customers
        /// </summary>
        /// <param name="storeId">int storeId</param>
        /// <returns></returns>
        ICollection<Customer> GetCustomers(int storeId);

        /// <summary>
        /// Add Customer
        /// </summary>
        /// <param name="customer">Customer customer</param>
        /// <returns></returns>
        Customer AddCustomer(Customer customer);
    }

    /// <summary>
    /// 
    /// </summary>
    public class Store
    {
        /// <summary>
        /// StoreId
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// CountryCode
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Customers
        /// </summary>
        public ICollection<Customer> Customers { get; set; }

    }

    /// <summary>
    /// Customer class
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// CustomerId
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// StoreId
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
