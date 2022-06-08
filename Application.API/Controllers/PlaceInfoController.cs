using Application.DTO;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Application.API.Controllers
{
    /// <summary>
    /// API Controller for PlaceInfo
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceInfoController : ControllerBase
    {
        /// <summary>
        /// Readonly variable of IPlaceInfoService
        /// </summary>
        private readonly IPlaceInfoService _placeInfoService;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="placeInfoService">IPlaceInfoService placeInfoService</param>
        public PlaceInfoController(IPlaceInfoService placeInfoService)
        {
            _placeInfoService = placeInfoService;
        }

        /// <summary>
        /// Get Place Info
        /// </summary>
        /// <returns>IEnumerable PlaceInfo </returns>
        // GET api/placeinfo  
        [HttpGet]
        public IEnumerable<PlaceInfo> GetPlaceInfos() => _placeInfoService.GetAll();

        /// <summary>
        /// Get Place Info By Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        // GET api/placeinfo/id  
        [HttpGet("{id}", Name = nameof(GetPlaceInfoById))]
        public IActionResult GetPlaceInfoById(int id)
        {
            PlaceInfo placeInfo = _placeInfoService.Find(id);
            if (placeInfo == null)
                return NotFound();
            else
                return new ObjectResult(placeInfo);
        }

        /// <summary>
        /// Post Place Info
        /// </summary>
        /// <param name="placeinfo">PlaceInfo placeinfo</param>
        /// <returns></returns>
        // POST api/placeinfo  
        [HttpPost]
        public IActionResult PostPlaceInfo([FromBody] PlaceInfo placeinfo)
        {
            if (placeinfo == null) return BadRequest();
            int retVal = _placeInfoService.Add(placeinfo);
            if (retVal > 0) return Ok(); else return NotFound();
        }

        /// <summary>
        /// Put Place Info
        /// </summary>
        /// <param name="id">int id</param>
        /// <param name="placeinfo">PlaceInfo placeinfo</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult PutPlaceInfo(int id, [FromBody] PlaceInfo placeinfo)
        {
            if (placeinfo == null || id != placeinfo.Id) return BadRequest();
            if (_placeInfoService.Find(id) == null) return NotFound();
            int retVal = _placeInfoService.Update(placeinfo);
            if (retVal > 0) return Ok(); else return NotFound();
        }

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        // DELETE api/placeinfo/5  
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int retVal = _placeInfoService.Remove(id);
            if (retVal > 0) return Ok(); else return NotFound();
        }
    }
}
