using Core6.EFDbFirst.API.Entities;
using Core6.EFDbFirst.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Core6.EFDbFirst.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly ILogger<BankAccountController> _logger;

        public  BankAccountController(IBankAccountService bankAccountService, ILogger<BankAccountController> logger) 
        {
            _bankAccountService = bankAccountService;
            _logger = logger;
        }

        [HttpGet("getbanklist")]
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

        [HttpGet("getbyid")]
        public async Task<IEnumerable<BankAccount>> GetBankAccountByIdAsync(int Id)
        {
            try
            {
                var response = await _bankAccountService.GetBankAccountByIdAsync(Id);
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

        [HttpPost("addbankaccount")]
        public async Task<IActionResult> AddBankAccountAsync(BankAccount bankAccount)
        {
            if (bankAccount == null)
            {
                return BadRequest();
            }
            try
            {
                var response = await _bankAccountService.AddBankAccountAsync(bankAccount);
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("updatebankaccount")]
        public async Task<IActionResult> UpdateBankAccountAsync(BankAccount bankAccount)
        {
            if (bankAccount == null)
            {
                return BadRequest();
            }
            try
            {
                var result = await _bankAccountService.UpdateBankAccountAsync(bankAccount);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("deletebyid")]
        public async Task<int> DeleteBankAccountAsync(int Id)
        {
            try
            {
                var response = await _bankAccountService.DeleteBankAccountAsync(Id);
                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
