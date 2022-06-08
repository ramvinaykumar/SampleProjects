using BusinessAccessLayer;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Core5APIWebService.Controllers
{
    /// <summary>
    /// Person Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        #region Variables

        /// <summary>
        /// Person Service Instance
        /// </summary>
        private readonly PersonService _personService;

        /// <summary>
        /// IRepository Instance
        /// </summary>
        private readonly IRepository<Person> _person;

        #endregion

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="Person">IRepository<Person> person</param>
        /// <param name="ProductService">PersonService productService</param>
        public PersonController(IRepository<Person> person, PersonService productService)
        {
            _personService = productService;
            _person = person;
        }

        #region Public Actions

        /// <summary>
        /// Add Person Data
        /// </summary>
        /// <param name="person">Person person object to add person data</param>
        [HttpPost("AddPerson")]
        public async Task<Object> AddPerson([FromBody] Person person)
        {
            try
            {
                await _personService.AddPerson(person);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete Person by UserName
        /// </summary>
        /// <param name="userName">Username value as string</param>
        [HttpDelete("DeletePerson")]
        public bool DeletePerson(string userName)
        {
            try
            {
                _personService.DeletePerson(userName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Update Person
        /// </summary>
        /// <param name="person">Person person object to update person data</param>
        [HttpPut("UpdatePerson")]
        public bool UpdatePerson(Person person)
        {
            try
            {
                _personService.UpdatePerson(person);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Get Person by UserName
        /// </summary>
        /// <param name="userName">Username value as string</param>
        [HttpGet("GetAllPersonByName")]
        public Object GetAllPersonByName(string userName)
        {
            var data = _personService.GetPersonByUserName(userName);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return json;
        }

        /// <summary>
        /// Get Person detail by UserEmail
        /// </summary>
        /// <param name="userEmail">userEmail value as string</param>
        [HttpGet("GetPersonByUserEmail")]
        public Object GetPersonByUserEmail(string userEmail)
        {
            var data = _personService.GetPersonByUserEmail(userEmail);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return json;
        }

        /// <summary>
        /// Get All Person
        /// </summary>
        [HttpGet("GetAllPersons")]
        public Object GetAllPersons()
        {
            var data = _personService.GetAllPersons();
            var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                        }
            );
            return json;
        }

        #endregion
    }
}
