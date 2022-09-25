using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoToElastic.Models.Enums;
using MongoToElastic.Repository;

namespace MongoToElastic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoToElasticController : Controller
    {
        private readonly IMongoToElastic _mongoToElastic;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mongoToElastic"></param>
        public MongoToElasticController(IMongoToElastic mongoToElastic)
        {
            _mongoToElastic = mongoToElastic;
        }

        /// <summary>
        /// This method is to read data from mongo
        /// </summary>
        /// <param name="dataMap">DataMap</param>
        /// <param name="env">Enviornment</param>
        /// <returns></returns>
        [HttpGet("GetMongoClientData")]
        public ActionResult<string> GetMongoClientData(Data dataMap, Enviornment env)
        {
            return _mongoToElastic.ReadMongoData(dataMap, env);
        }

        /// <summary>
        /// This method is to read data from elastic
        /// </summary>
        /// <param name="dataMap">DataMap</param>
        /// <param name="env">Enviornment</param>
        /// <returns></returns>
        [HttpGet("GetElasticClientData")]
        public ActionResult<string> GetElasticClientData(Data dataMap, Enviornment env)
        {
            return _mongoToElastic.ReadElasticData(dataMap, env);
        }

        /// <summary>
        /// This method is used to data migrate from mongo to elastic
        /// </summary>
        /// <param name="dataMap">DataMap</param>
        /// <param name="env">Enviornment</param>
        /// <returns></returns>
        [HttpGet("SaveMongoToElasticdata")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult<string> DataTransferMongoToElastic(Data dataMap, Enviornment env)
        {
            return _mongoToElastic.InsertDataFromMongoToElastic(dataMap, env);
        }
        /// <summary>
        /// This method is used to Compare Elastic with Mongo(SSCS only)
        /// </summary>
        /// <param name="env">Enviornment</param>
        /// <returns></returns>
        [HttpGet("CheckInstance")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult<string> CheckInstance(Enviornment env)
        {
            return _mongoToElastic.CheckInstance();
        }
    }
}
