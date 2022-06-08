using DotNet5.CRUD.API.Entities;
using DotNet5.CRUD.API.Models.Users;
using System.Collections.Generic;

namespace DotNet5.CRUD.API.Repository
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Create(CreateRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }
}
