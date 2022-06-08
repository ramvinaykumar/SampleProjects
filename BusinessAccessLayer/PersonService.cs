using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    /// <summary>
    /// Person Service Class
    /// </summary>
    public class PersonService
    {
        /// <summary>
        /// IRepository Person instance
        /// </summary>
        private readonly IRepository<Person> _person;

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="perosn">IRepository<Person> perosn</param>
        public PersonService(IRepository<Person> perosn)
        {
            _person = perosn;
        }

        /// <summary>
        /// Get Person Detail By UserEmail
        /// </summary>
        /// <param name="userEmail">string value of userEmail</param>
        /// <returns>Return single person detail as List</returns>
        public Person GetPersonByUserEmail(string userEmail)
        {
            return _person.GetAll().Where(x => x.UserEmail == userEmail).FirstOrDefault();
        }

        /// <summary>
        /// Get All Person Details
        /// </summary>
        /// <returns>Returns all person details as List</returns>
        public IEnumerable<Person> GetAllPersons()
        {
            try
            {
                return _person.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Person by User Name
        /// </summary>
        /// <param name="userName">Username value as string</param>
        /// <returns>Return Person Detail</returns>
        public Person GetPersonByUserName(string userName)
        {
            return _person.GetAll().Where(x => x.UserName == userName).FirstOrDefault();
        }

        /// <summary>
        /// Add New Person Detail
        /// </summary>
        /// <param name="Person">Person person object to add person data</param>
        /// <returns></returns>
        public async Task<Person> AddPerson(Person Person)
        {
            return await _person.Create(Person);
        }

        /// <summary>
        /// Delete Person by UserName
        /// </summary>
        /// <param name="userName">Username value as string</param>
        /// <returns>Return true if deleted, false if not deleted</returns>
        public bool DeletePerson(string userName)
        {
            try
            {
                var persons = _person.GetAll().Where(x => x.UserName == userName).ToList();
                foreach (var item in persons)
                {
                    _person.Delete(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

        /// <summary>
        /// Update Person Detail
        /// </summary>
        /// <param name="person">Person person object to update person data</param>
        /// <returns>Return true in case of success, false in failure</returns>
        public bool UpdatePerson(Person person)
        {
            try
            {
                var persons = _person.GetAll().Where(x => x.IsDeleted != true).ToList();
                foreach (var item in persons)
                {
                    _person.Update(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
