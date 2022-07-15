using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Proper.Request.Response.Models;
using WebAPI.Proper.Request.Response.Models.Campaign;

namespace WebAPI.Proper.Request.Response.Repository.Interface
{
    public interface ICampaignService
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get Campaign data as list Task<IEnumerable<CampaignList>>
        /// </summary>
        /// <returns>Return Campaign data as list</returns>
        Task<GenericResponse<IEnumerable<Campaigns>>> Listing();

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create Campaign
        /// </summary>
        /// <param name="addCampaign">AddCampaignDTO addCampaign</param>
        /// <returns>Returns the response message</returns>
        Task<GenericResponse<Campaigns>> Create(AddCampaignDTO addCampaign);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update Campaign
        /// </summary>
        /// <param name="updateCampaign">UpdateCampaignDTO updateCampaign</param>
        /// <returns>Returns the response message</returns>
        Task<GenericResponse<Campaigns>> Update(UpdateCampaignDTO updateCampaign);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete Campaign by ID
        /// </summary>
        /// <param name="Id">int Id</param>
        /// <returns>Returns the response message</returns>
        Task<GenericResponse<bool>> Delete(int Id);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Activate Campaign by ID
        /// </summary>
        /// <param name="Id">int Id</param>
        Task<GenericResponse<bool>> Activate(int Id);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Deactivate Campaign by ID
        /// </summary>
        /// <param name="Id">int Id</param>
        Task<GenericResponse<bool>> Deactivate(int Id);
    }
}
