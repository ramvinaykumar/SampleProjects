using Core6.EFDbFirst.API.Data;
using Core6.EFDbFirst.API.Entities;
using Core6.EFDbFirst.API.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Core6.EFDbFirst.API.Repository.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly BankDbContext _dbContext;

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="dbContext">BankDbContext dbContext</param>
        public BankAccountService(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get BankAccount List
        /// </summary>
        /// <returns>Returns List Of Bank Accounts</returns>
        public async Task<List<BankAccount>> GetBankAccountListAsync()
        {
            try
            {
                List<BankAccount>? bankData = new List<BankAccount>();
                var result = await _dbContext.BankAccounts.FromSqlRaw<BankAccountDto>("GetBankAccountList").ToListAsync();

                if (result != null && result.Count > 0)
                {
                    bankData = result?.Select(x => new BankAccount
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
                    }).ToList();
                }
                return bankData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Get BankAccount By Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        public async Task<IEnumerable<BankAccount>> GetBankAccountByIdAsync(int id)
        {
            var param = new SqlParameter("@ID", id);
            var banckAccount = await Task.Run(() => _dbContext.BankAccount
                            .FromSqlRaw(@"exec GetBankAccountByID @ID", param).ToListAsync());
            return banckAccount;
        }

        /// <summary>
        ///  Get BankAccount Detail By Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        public async Task<BankAccount> GetBankAccountDetailByIdAsync(int id)
        {
            BankAccount? account = new BankAccount();
            var param = new SqlParameter("@ID", id);
            var banckAccount = await _dbContext.BankAccount.FromSqlRaw<BankAccount>(@"exec GetBankAccountByID @ID", param).ToListAsync();
            
            if(banckAccount!=null && banckAccount.Count > 0)
            {
                account = banckAccount?.Select(x => new BankAccount
                {
                    BankAccountNumber = x.BankAccountNumber,
                    BankCode = x.BankCode,
                    BankName = x.BankName,
                    ID = x.ID,
                    IFSCCode = x.IFSCCode,
                    IsActive = x.IsActive,
                    PartialSumAmount = x.PartialSumAmount,
                    PaymentMode = x.PaymentMode,
                    TransactionNumber = x.TransactionNumber
                }).FirstOrDefault();
            }
            return account;
        }

        /// <summary>
        /// Add bank account
        /// </summary>
        /// <param name="addBankAccount">BankAccount addBankAccount</param>
        /// <returns></returns>
        public async Task<int> AddBankAccountAsync(BankAccount addBankAccount)
        {
            var accountNumber = StringToByteArray(addBankAccount.BankAccountNumber);

            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@TransactionNumber", Guid.NewGuid()));
            parameter.Add(new SqlParameter("@BankName", addBankAccount.BankName));
            parameter.Add(new SqlParameter("@BankCode", addBankAccount.BankCode));
            parameter.Add(new SqlParameter("@BankAccountNumber", accountNumber));
            parameter.Add(new SqlParameter("@IFSCCode", addBankAccount.IFSCCode));
            parameter.Add(new SqlParameter("@PaymentMode", addBankAccount.PaymentMode));
            parameter.Add(new SqlParameter("@PartialSumAmount", addBankAccount.PartialSumAmount));
            parameter.Add(new SqlParameter("@IsActive", addBankAccount.IsActive));

            var result = await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync(@"exec AddBankAccount @TransactionNumber, @BankName, @BankCode, @BankAccountNumber, @IFSCCode, @PaymentMode, @PartialSumAmount, @IsActive", parameter.ToArray()));
            return result;
        }

        /// <summary>
        /// Update Bank Account
        /// </summary>
        /// <param name="updateBankAccount">BankAccount updateBankAccount</param>
        /// <returns></returns>
        public async Task<int> UpdateBankAccountAsync(BankAccount updateBankAccount)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ID", updateBankAccount.ID));
            parameter.Add(new SqlParameter("@TransactionNumber", updateBankAccount.TransactionNumber));
            parameter.Add(new SqlParameter("@BankName", updateBankAccount.BankName));
            parameter.Add(new SqlParameter("@BankCode", updateBankAccount.BankCode));
            parameter.Add(new SqlParameter("@BankAccountNumber", updateBankAccount.BankAccountNumber));
            parameter.Add(new SqlParameter("@IFSCCode", updateBankAccount.IFSCCode));
            parameter.Add(new SqlParameter("@PaymentMode", updateBankAccount.PaymentMode));
            parameter.Add(new SqlParameter("@PartialSumAmount", updateBankAccount.PartialSumAmount));
            parameter.Add(new SqlParameter("@IsActive", updateBankAccount.IsActive));
            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec UpdateBankAccount @ID, @TransactionNumber, @BankName, @BankCode, @BankAccountNumber, @IFSCCode, @PaymentMode, @PartialSumAmount, @IsActive", parameter.ToArray()));
            return result;
        }

        /// <summary>
        /// Delete bank account by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        public async Task<int> DeleteBankAccountAsync(int id)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"DeleteBankAccountByID {id}"));
        }

        /// <summary>
        /// String To Byte Array
        /// </summary>
        /// <param name="bankAccountNumber">string bankAccountNumber</param>
        /// <returns></returns>
        private byte[] StringToByteArray(string bankAccountNumber)
        {
            return Encoding.Default.GetBytes(bankAccountNumber);
        }

        /// <summary>
        /// Byte Array To String
        /// </summary>
        /// <param name="bankAccountNumber">byte[] bankAccountNumber</param>
        /// <returns></returns>
        private string ByteArrayToString(byte[] bankAccountNumber)
        {
            return Encoding.Default.GetString(bankAccountNumber);
        }
    }
}
