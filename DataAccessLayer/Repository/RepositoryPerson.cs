using DataAccessLayer.DBContext;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Repository Person
    /// </summary>
    public class RepositoryPerson : IRepository<Person>
    {
        /// <summary>
        /// Application DbContext
        /// </summary>
        ApplicationDbContext _dbContext;

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="applicationDbContext"></param>
        public RepositoryPerson(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        /// <summary>
        /// Create new record
        /// </summary>
        /// <param name="_object">Person person object to add person data</param>
        /// <returns></returns>
        public async Task<Person> Create(Person _object)
        {
            var obj = await _dbContext.Persons.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        /// <summary>
        /// Delete person record
        /// </summary>
        /// <param name="_object">Person person object to add person data</param>
        public void Delete(Person _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Get all person details
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Person> GetAll()
        {
            try
            {
                return _dbContext.Persons.Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get person detail by userEmail
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public Person GetPersonByUserEmail(string userEmail)
        {
            return _dbContext.Persons.Where(x => x.IsDeleted == false && x.UserEmail == userEmail).FirstOrDefault();
        }

        /// <summary>
        /// Update the existing record
        /// </summary>
        /// <param name="person">Person person object to add person data</param>
        public void Update(Person person)
        {
            _dbContext.Persons.Update(person);
            _dbContext.SaveChanges();
        }

    }
}
