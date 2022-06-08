using MongoDB.API.Interface;
using MongoDB.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDB.API.Services
{
    /// <summary>
    /// Employee Service Class
    /// </summary>
    public class EmployeeService : IEmployee<Employee>
    {
        /// <summary>
        /// IMongoCollection variable
        /// </summary>
        private readonly IMongoCollection<Employee> _employee;

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="employeeDBSettings">IEmployeeDBSettings employeeDBSettings</param>
        public EmployeeService(IEmployeeDBSettings employeeDBSettings)
        {
            var client = new MongoClient(employeeDBSettings.ConnectionString);
            var database = client.GetDatabase(employeeDBSettings.DatabaseName);

            _employee = database.GetCollection<Employee>(employeeDBSettings.EmployeesCollectionName);
        }

        /// <summary>
        /// Get all employees data
        /// </summary>
        /// <returns>Return employee list</returns>
        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _employee.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Get employee data by Id
        /// </summary>
        /// <param name="id">string id</param>
        /// <returns>Returns Employee data</returns>
        public async Task<Employee> GetById(string id)
        {
            return await _employee.Find<Employee>(f => f.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Create new Employee data
        /// </summary>
        /// <param name="employee">Employee employee</param>
        /// <returns>Return new Employee data</returns>
        public async Task<Employee> Create(Employee employee)
        {
            await _employee.InsertOneAsync(employee);
            return employee;
        }

        /// <summary>
        /// Update existing employee record
        /// </summary>
        /// <param name="employee">Employee employee</param>
        public async Task<Employee> Update(Employee employee)
        {
            var options = new UpdateOptions()
            {
                IsUpsert = true
            };

            var queryBuilder = Builders<Employee>.Filter;
            var query = queryBuilder.Eq(q => q.Id, employee.Id);
            var empData = _employee.Find(query).ToList();
            var data = empData.FirstOrDefault();

            UpdateDefinition<Employee> updateDefForObject = new BsonDocument("$set", data.ToBsonDocument());
            Expression<Func<Employee, bool>> exprItemQuery = x => x.Id == employee.Id;

            // _employee.ReplaceOne(r => r.Id == id, employee);
            await _employee.UpdateOneAsync<Employee>(exprItemQuery, updateDefForObject, options);
            return employee;
        }

        /// <summary>
        /// Delete an employee data
        /// </summary>
        /// <param name="employee">Employee employee</param>
        public async Task<Employee> Delete(Employee employee)
        {
            await _employee.DeleteOneAsync(d => d.Id == employee.Id);
            return employee;
        }

        /// <summary>
        /// Delete an employee data by Id
        /// </summary>
        /// <param name="id">string id</param>
        public async Task<bool> RemoveById(string id)
        {
            await _employee.DeleteOneAsync(d => d.Id == id);
            return true;
        }
    }
}
