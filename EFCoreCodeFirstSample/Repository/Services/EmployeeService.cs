using EFCoreCodeFirstSample.Entity;
using EFCoreCodeFirstSample.Models;
using EFCoreCodeFirstSample.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstSample.Repository.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EFCoreCodeContext _dbContext;

        public EmployeeService(EFCoreCodeContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public ResponseModel DeleteEmployee(long employeeId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                EmployeeEntity _temp = GetEmployeeDetailsById(employeeId);
                if (_temp != null)
                {
                    _dbContext.Remove<EmployeeEntity>(_temp);
                    _dbContext.SaveChanges();
                    model.IsSuccess = true;
                    model.Messsage = "Employee Deleted Successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "Employee Not Found";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        /// <summary>
        /// get employee details by employee id
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public EmployeeEntity GetEmployeeDetailsById(long empId)
        {
            EmployeeEntity emp;
            try
            {
               emp = _dbContext.Employees.Where(x => x.EmployeeId == empId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            return emp;
        }

        public List<EmployeeEntity> GetEmployeesList()
        {
            List<EmployeeEntity> empList;
            try
            {
                empList = _dbContext.Employees.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return empList;
        }

        public ResponseModel SaveEmployee(EmployeeEntity employeeModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                EmployeeEntity _temp = GetEmployeeDetailsById(employeeModel.EmployeeId);
                if (_temp != null)
                {
                    _temp.Email = employeeModel.Email;
                    _temp.FirstName = employeeModel.FirstName;
                    _temp.LastName = employeeModel.LastName;
                    _temp.PhoneNumber = employeeModel.PhoneNumber;
                    _temp.DateOfBirth = employeeModel.DateOfBirth;

                    _dbContext.Update<EmployeeEntity>(_temp);
                    model.Messsage = "Employee Update Successfully";
                }
                else
                {
                    _dbContext.Add<EmployeeEntity>(employeeModel);
                    model.Messsage = "Employee Inserted Successfully";
                }
                _dbContext.SaveChanges();
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }
    }
}
