using Core7.Dapper.SQL.API.Entities;
using Core7.Dapper.SQL.API.Models.Users;

namespace Core7.Dapper.SQL.API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task Create(CreateRequest model);
        Task Update(int id, UpdateRequest model);
        Task Delete(int id);
    }
}
