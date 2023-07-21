using Core6.EFDbFirst.API.Data;
using Core6.EFDbFirst.API.Entities;
using Core6.EFDbFirst.API.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Text;

namespace Core6.EFDbFirst.API.Repository.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly BankDbContext _dbContext;

        public BankAccountService(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BankAccount>> GetBankAccountListAsync()
        {
            return await _dbContext.BankAccounts
                .FromSqlRaw<BankAccountDto>("GetBankAccountList")
                .Select(x => new BankAccount
                {
                    BankAccountNumber = ByteArrayToString(x.BankAccountNumber),
                    BankCode = x.BankCode,
                    BankName = x.BankName,
                    ID = x.ID,
                    IFSCCode = x.IFSCCode,
                    IsActive = x.IsActive,
                    PartialSumAmount = x.PartialSumAmount,
                    PaymentMode = x.PaymentMode,
                    TransactionNumber = x.TransactionNumber
                }).ToListAsync();
        }

        public async Task<IEnumerable<BankAccount>> GetBankAccountByIdAsync(int id)
        {
            var param = new SqlParameter("@ID", id);
            var banckAccount = await Task.Run(() => _dbContext.BankAccount
                            .FromSqlRaw(@"exec GetBankAccountByID @ID", param).ToListAsync());
            return banckAccount;
        }

        public async Task<int> AddBankAccountAsync(BankAccount bankAccount)
        {
            var accountNumber = StringToByteArray(bankAccount.BankAccountNumber);

            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@TransactionNumber", Guid.NewGuid()));
            parameter.Add(new SqlParameter("@BankName", bankAccount.BankName));
            parameter.Add(new SqlParameter("@BankCode", bankAccount.BankCode));
            parameter.Add(new SqlParameter("@BankAccountNumber", accountNumber));
            parameter.Add(new SqlParameter("@IFSCCode", bankAccount.IFSCCode));
            parameter.Add(new SqlParameter("@PaymentMode", bankAccount.PaymentMode));
            parameter.Add(new SqlParameter("@PartialSumAmount", bankAccount.PartialSumAmount));
            parameter.Add(new SqlParameter("@IsActive", bankAccount.IsActive));

            var result = await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync(@"exec AddBankAccount @TransactionNumber, @BankName, @BankCode, @BankAccountNumber, @IFSCCode, @PaymentMode, @PartialSumAmount, @IsActive", parameter.ToArray()));
            return result;
        }

        public async Task<int> UpdateBankAccountAsync(BankAccount bankAccount)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ID", bankAccount.ID));
            parameter.Add(new SqlParameter("@TransactionNumber", bankAccount.TransactionNumber));
            parameter.Add(new SqlParameter("@BankName", bankAccount.BankName));
            parameter.Add(new SqlParameter("@BankCode", bankAccount.BankCode));
            parameter.Add(new SqlParameter("@BankAccountNumber", bankAccount.BankAccountNumber));
            parameter.Add(new SqlParameter("@IFSCCode", bankAccount.IFSCCode));
            parameter.Add(new SqlParameter("@PaymentMode", bankAccount.PaymentMode));
            parameter.Add(new SqlParameter("@PartialSumAmount", bankAccount.PartialSumAmount));
            parameter.Add(new SqlParameter("@IsActive", bankAccount.IsActive));
            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec UpdateBankAccount @ID, @TransactionNumber, @BankName, @BankCode, @BankAccountNumber, @IFSCCode, @PaymentMode, @PartialSumAmount, @IsActive", parameter.ToArray()));
            return result;
        }

        public async Task<int> DeleteBankAccountAsync(int ProductId)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"DeleteBankAccountByID {ProductId}"));
        }

        private byte[] StringToByteArray(string plainText)
        {
            //var bytesText = Encoding.UTF8.GetBytes(plainText);
            //var encodedText = Convert.ToBase64String(bytesText);
            //return Encoding.UTF8.GetBytes(encodedText);
            return Encoding.Default.GetBytes(plainText);
        }

        private string ByteArrayToString(byte[] accountNumber)
        {
            //string base64EncodedData = Encoding.UTF8.GetString(accountNumber);
            //var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            //return Encoding.UTF8.GetString(base64EncodedBytes);
            return Encoding.Default.GetString(accountNumber);
        }

    }
}
