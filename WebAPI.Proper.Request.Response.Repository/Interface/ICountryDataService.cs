using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Proper.Request.Response.Models;
using WebAPI.Proper.Request.Response.Models.Country;

namespace WebAPI.Proper.Request.Response.Repository.Interface
{
    public interface ICountryDataService
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get Country data as list Task<IEnumerable<CountryResponseDto>>
        /// </summary>
        /// <returns>Return Organiser data as list</returns>
        Task<GenericResponse<IEnumerable<CountryResponseDto>>> Listing();

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create Student
        /// </summary>
        /// <param name="requestDto">CountryEntity requestDto</param>
        /// <returns>Returns the response message</returns>
        Task<GenericResponse<CountryResponseDto>> Create(CountryEntity requestDto);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update Country
        /// </summary>
        /// <param name="requestDto">CountryEntity requestDto</param>
        /// <returns>Returns the response message</returns>
        Task<GenericResponse<CountryResponseDto>> Update(CountryEntity requestDto);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete Country by ID
        /// </summary>
        /// <param name="Id">int Id</param>
        /// <returns>Returns the response message</returns>
        Task<GenericResponse<bool>> Delete(int Id);
    }
}
