using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Proper.Request.Response.Models;
using WebAPI.Proper.Request.Response.Models.Students;

namespace WebAPI.Proper.Request.Response.Repository.Interface
{
    public interface IStudentServices
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get Students data as list Task<IEnumerable<CampaignEntity>>
        /// </summary>
        /// <returns>Return Organiser data as list</returns>
        Task<GenericResponse<IEnumerable<Students>>> Listing();

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create Student
        /// </summary>
        /// <param name="addStudent">AddStudentDTO addStudent</param>
        /// <returns>Returns the response message</returns>
        Task<GenericResponse<Students>> Create(AddStudentDTO addStudent);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update Student
        /// </summary>
        /// <param name="updateStudent">UpdateStudentDTO updateStudent</param>
        /// <returns>Returns the response message</returns>
        Task<GenericResponse<Students>> Update(UpdateStudentDTO updateStudent);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete Student by ID
        /// </summary>
        /// <param name="Id">int Id</param>
        /// <returns>Returns the response message</returns>
        Task<GenericResponse<bool>> Delete(int Id);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Activate Student by ID
        /// </summary>
        /// <param name="Id">int Id</param>
        Task<GenericResponse<bool>> Activate(int Id);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Deactivate Student by ID
        /// </summary>
        /// <param name="Id">int Id</param>
        Task<GenericResponse<bool>> Deactivate(int Id);
    }
}
