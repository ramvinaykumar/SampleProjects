using Core7.Dapper.SQL.API.Entities;

namespace Core7.Dapper.SQL.API.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
        Task Create(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}
