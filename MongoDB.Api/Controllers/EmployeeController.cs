using Microsoft.AspNetCore.Mvc;
using MongoDB.API.Models;
using MongoDB.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDB.API.Controllers
{
    /// <summary>
    /// Employee Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="employeeService">EmployeeService employeeService</param>
        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Add new record to Employee
        /// </summary>
        /// <param name="employee">Employee employee</param>
        /// <returns></returns>
        [Route("Insert")]
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            return await _employeeService.Create(employee);
        }

        /// <summary>
        /// Update the existing record of employee
        /// </summary>
        /// <param name="employee">Employee employee</param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPut]
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            return await _employeeService.Update(employee);
        }

        /// <summary>
        /// Get all the employee data
        /// </summary>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            return await _employeeService.GetAll();
        }

        /// <summary>
        /// Get employee data by Id
        /// </summary>
        /// <param name="id">string id</param>
        /// <returns></returns>
        [Route("GetById")]
        [HttpGet]
        public async Task<Employee> GetEmployeeById(string id)
        {
            return await _employeeService.GetById(id);
        }

        /// <summary>
        /// Delete an employee by Id
        /// </summary>
        /// <param name="id">string id</param>
        [Route("Delete")]
        [HttpDelete]
        public async Task<bool> Delete(string id)
        {
            return await _employeeService.RemoveById(id);
        }
    }
}
