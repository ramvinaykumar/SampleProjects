using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Proper.Request.Response.Models;
using WebAPI.Proper.Request.Response.Models.Students;
using WebAPI.Proper.Request.Response.Repository.Interface;

namespace WebAPI.Proper.Request.Response.Controllers
{
    /// <summary>
    /// Students Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        /// <summary>
        /// Student service repository for DI
        /// </summary>
        private readonly IStudentServices _studentServices;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="studentServices"></param>
        public StudentsController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        /// <summary>
        /// Get all the student data as list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GenericResponse<IEnumerable<Students>>> GetStudents()
        {
            return await _studentServices.Listing();
        }

        /// <summary>
        /// Add new student data
        /// </summary>
        /// <param name="addStudent">AddStudentDTO addStudent</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenericResponse<Students>> AddNewStudent([FromBody] AddStudentDTO addStudent)
        {
            return await _studentServices.Create(addStudent);
        }

        /// <summary>
        /// Update existing student data by Id
        /// </summary>
        /// <param name="updateStudent">UpdateStudentDTO updateStudent</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<GenericResponse<Students>> UpdateStudent([FromBody] UpdateStudentDTO updateStudent)
        {
            return await _studentServices.Update(updateStudent);
        }

        /// <summary>
        /// Delete student data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<GenericResponse<bool>> DeleteStudent(int id)
        {
            return await _studentServices.Delete(id);
        }

        /// <summary>
        /// Activate student data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        [HttpGet("Activate")]
        public async Task<GenericResponse<bool>> ActivateStudent(int id)
        {
            return await _studentServices.Activate(id);
        }

        /// <summary>
        /// Deactivate student data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        [HttpGet("Deactivate")]
        public async Task<GenericResponse<bool>> DeactivateStudent(int id)
        {
            return await _studentServices.Deactivate(id);
        }
    }
}
