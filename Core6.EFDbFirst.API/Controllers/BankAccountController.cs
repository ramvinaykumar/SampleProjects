using Core6.EFDbFirst.API.Entities;
using Core6.EFDbFirst.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Core6.EFDbFirst.API.Controllers
{
    /// <summary>
    /// Bank account controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        #region Variables

        private readonly IBankAccountService _bankAccountService;
        private readonly ILogger<BankAccountController> _logger;

        #endregion

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="bankAccountService">IBankAccountService bankAccountService</param>
        /// <param name="logger">ILogger<BankAccountController> logger</param>
        public BankAccountController(IBankAccountService bankAccountService, ILogger<BankAccountController> logger) 
        {
            _bankAccountService = bankAccountService;
            _logger = logger;
        }

        #region API End Points

        /// <summary>
        /// Bank Account List
        /// </summary>
        /// <returns></returns>
        [HttpGet("banklist")]
        public async Task<List<BankAccount>> GetBankAccountListAsync()
        {
            try
            {
                _logger.LogInformation("Fetching bank account detail as list");
                return await _bankAccountService.GetBankAccountListAsync();
            }
            catch
            {
                _logger.Log(LogLevel.Error, "something went wrong");
                throw;
            }
        }

        /// <summary>
        /// Get Bank Account By Id
        /// </summary>
        /// <param name="id">int Id</param>
        /// <returns></returns>
        [HttpGet("bankbyid")]
        public async Task<IEnumerable<BankAccount>> GetBankAccountByIdAsync(int id)
        {
            try
            {
                var response = await _bankAccountService.GetBankAccountByIdAsync(id);
                if (response == null)
                {
                    return null;
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Bank Account Detail By Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        [HttpGet("accountbyid")]
        public async Task<BankAccount> GetBankAccountDetailByIdAsync(int id)
        {
            try
            {
                var response = await _bankAccountService.GetBankAccountDetailByIdAsync(id);
                if (response == null)
                {
                    return null;
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add bank account
        /// </summary>
        /// <param name="addBankAccount">BankAccount addBankAccount</param>
        /// <returns></returns>
        [HttpPost("addbankaccount")]
        public async Task<IActionResult> AddBankAccountAsync(BankAccount addBankAccount)
        {
            if (addBankAccount == null)
            {
                return BadRequest();
            }
            try
            {
                var response = await _bankAccountService.AddBankAccountAsync(addBankAccount);
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Update Bank Account
        /// </summary>
        /// <param name="updateBankAccount">BankAccount updateBankAccount</param>
        /// <returns></returns>
        [HttpPut("updatebankaccount")]
        public async Task<IActionResult> UpdateBankAccountAsync(BankAccount updateBankAccount)
        {
            if (updateBankAccount == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _bankAccountService.UpdateBankAccountAsync(updateBankAccount);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete bank account by Id
        /// </summary>
        /// <param name="id">int Id</param>
        /// <returns></returns>
        [HttpDelete("deletebyid")]
        public async Task<int> DeleteBankAccountAsync(int id)
        {
            try
            {
                var response = await _bankAccountService.DeleteBankAccountAsync(id);
                return response;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
