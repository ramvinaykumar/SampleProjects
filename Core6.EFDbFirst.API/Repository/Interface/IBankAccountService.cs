using Core6.EFDbFirst.API.Entities;

namespace Core6.EFDbFirst.API.Repository.Interface
{
    /// <summary>
    /// Interface for Bank Account
    /// </summary>
    public interface IBankAccountService
    {
        /// <summary>
        /// Bank Account List
        /// </summary>
        /// <returns></returns>
        public Task<List<BankAccount>> GetBankAccountListAsync();

        /// <summary>
        /// Get Bank Account By Id
        /// </summary>
        /// <param name="Id">int Id</param>
        /// <returns></returns>
        public Task<IEnumerable<BankAccount>> GetBankAccountByIdAsync(int Id);

        /// <summary>
        /// Get Bank Account Detail By Id
        /// </summary>
        /// <param name="Id">int Id</param>
        /// <returns></returns>
        public Task<BankAccount> GetBankAccountDetailByIdAsync(int Id);

        /// <summary>
        /// Add bank account
        /// </summary>
        /// <param name="bankAccount">BankAccount bankAccount</param>
        /// <returns></returns>
        public Task<int> AddBankAccountAsync(BankAccount product);

        /// <summary>
        /// Update Bank Account
        /// </summary>
        /// <param name="bankAccount">BankAccount bankAccount</param>
        /// <returns></returns>
        public Task<int> UpdateBankAccountAsync(BankAccount product);

        /// <summary>
        /// Delete bank account by Id
        /// </summary>
        /// <param name="Id">int Id</param>
        /// <returns></returns>
        public Task<int> DeleteBankAccountAsync(int Id);
    }
}
