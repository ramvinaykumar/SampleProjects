using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Proper.Request.Response.Models;
using WebAPI.Proper.Request.Response.Models.Country;
using WebAPI.Proper.Request.Response.Repository.Interface;

namespace WebAPI.Proper.Request.Response.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        /// <summary>
        /// Country service repository for DI
        /// </summary>
        private readonly ICountryDataService _countryDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="countryDataService"></param>
        public CountryController(ICountryDataService countryDataService)
        {
            _countryDataService = countryDataService;
        }

        /// <summary>
        /// Get all the Country data as list
        /// </summary>
        /// <returns></returns>
        [HttpGet("Countries")]
        public async Task<GenericResponse<IEnumerable<CountryResponseDto>>> GetCountries()
        {
            return await _countryDataService.Listing();
        }

        /// <summary>
        /// Add new Country data
        /// </summary>
        /// <param name="requestDto">CountryEntity requestDto</param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<GenericResponse<CountryResponseDto>> AddNew([FromBody] CountryEntity requestDto)
        {
            return await _countryDataService.Create(requestDto);
        }

        /// <summary>
        /// Update existing Country data by Id
        /// </summary>
        /// <param name="requestDto">CountryEntity requestDto</param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<GenericResponse<CountryResponseDto>> UpdateCountry([FromBody] CountryEntity requestDto)
        {
            return await _countryDataService.Update(requestDto);
        }

        /// <summary>
        /// Delete Country data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<GenericResponse<bool>> DeleteStudent(int id)
        {
            return await _countryDataService.Delete(id);
        }
    }
}
