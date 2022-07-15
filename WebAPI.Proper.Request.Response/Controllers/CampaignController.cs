using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Proper.Request.Response.Models;
using WebAPI.Proper.Request.Response.Models.Campaign;
using WebAPI.Proper.Request.Response.Repository.Interface;

namespace WebAPI.Proper.Request.Response.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        /// <summary>
        /// Campaign service repository for DI
        /// </summary>
        private readonly ICampaignService _campaignServices;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="campaignServices">ICampaignService campaignServices</param>
        public CampaignController(ICampaignService campaignServices)
        {
            _campaignServices = campaignServices;
        }

        /// <summary>
        /// Get all the student data as list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GenericResponse<IEnumerable<Campaigns>>> GetStudents()
        {
            return await _campaignServices.Listing();
        }

        /// <summary>
        /// Add new student data
        /// </summary>
        /// <param name="addStudent">AddStudentDTO addStudent</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GenericResponse<Campaigns>> AddNewStudent([FromBody] AddCampaignDTO addCampaign)
        {
            return await _campaignServices.Create(addCampaign);
        }

        /// <summary>
        /// Update existing student data by Id
        /// </summary>
        /// <param name="updateStudent">UpdateStudentDTO updateStudent</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<GenericResponse<Campaigns>> UpdateStudent([FromBody] UpdateCampaignDTO updateCampaign)
        {
            return await _campaignServices.Update(updateCampaign);
        }

        /// <summary>
        /// Delete student data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<GenericResponse<bool>> DeleteStudent(int id)
        {
            return await _campaignServices.Delete(id);
        }

        /// <summary>
        /// Activate student data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        [HttpGet("Activate")]
        public async Task<GenericResponse<bool>> ActivateStudent(int id)
        {
            return await _campaignServices.Activate(id);
        }

        /// <summary>
        /// Deactivate student data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        [HttpGet("Deactivate")]
        public async Task<GenericResponse<bool>> DeactivateStudent(int id)
        {
            return await _campaignServices.Deactivate(id);
        }
    }
}
