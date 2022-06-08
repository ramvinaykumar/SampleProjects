using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    /// <summary>
    /// Repository Interface 
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Create new record
        /// </summary>
        /// <param name="_object">T _object</param>
        /// <returns></returns>
        public Task<T> Create(T _object);

        /// <summary>
        /// Update existing record
        /// </summary>
        /// <param name="_object">T _object</param>
        public void Update(T _object);

        /// <summary>
        /// Get all person details
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll();

        /// <summary>
        /// Get person detail by userEmail
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public T GetPersonByUserEmail(string userEmail);

        /// <summary>
        /// Delete the record
        /// </summary>
        /// <param name="_object">T _object</param>
        public void Delete(T _object);
    }
}
