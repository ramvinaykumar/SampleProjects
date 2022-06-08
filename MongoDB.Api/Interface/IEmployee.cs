using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDB.API.Interface
{
    /// <summary>
    /// Interface for Employee
    /// </summary>
    /// <typeparam name="T">T Type</typeparam>
    public interface IEmployee<T>
    {
        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Get data by Id
        /// </summary>
        /// <param name="id">string id</param>
        /// <returns></returns>
        Task<T> GetById(string id);

        /// <summary>
        /// Create new data
        /// </summary>
        /// <param name="_object">T _object</param>
        /// <returns></returns>
        Task<T> Create(T _object);

        /// <summary>
        /// Update existing data
        /// </summary>
        /// <param name="_object">T _object</param>
        Task<T> Update(T _object);

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="_object">T _object</param>
        Task<T> Delete(T _object);

        /// <summary>
        /// Remove data by Id
        /// </summary>
        /// <param name="id">string id</param>
        Task<bool> RemoveById(string id);
    }
}
