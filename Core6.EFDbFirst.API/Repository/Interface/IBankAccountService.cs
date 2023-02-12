using Core6.EFDbFirst.API.Entities;

namespace Core6.EFDbFirst.API.Repository.Interface
{
    public interface IBankAccountService
    {
        public Task<List<BankAccount>> GetBankAccountListAsync();
        public Task<IEnumerable<BankAccount>> GetBankAccountByIdAsync(int Id);
        public Task<int> AddBankAccountAsync(BankAccount product);
        public Task<int> UpdateBankAccountAsync(BankAccount product);
        public Task<int> DeleteBankAccountAsync(int Id);
    }
}
