using Microsoft.AspNetCore.Mvc;
using MongoToElastic.Models;
using MongoToElastic.Models.Enums;
using MongoToElastic.Repository;
using System.ComponentModel.DataAnnotations;

namespace MongoToElastic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MigrationController : ControllerBase
    {
        private readonly IMigration _DataMigrationRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mongoToESRepository"></param>
        public MigrationController(IMigration IDataMigrationRepository)
        {
            _DataMigrationRepository = IDataMigrationRepository;
        }

        /// <summary>
        /// This method is used to Compare Data
        /// </summary>
        /// <param name="env">Enviornment</param>
        /// <param name="dateRange">DateRange</param>
        /// <param name="dataMap">dataMap</param>
        /// <param name="direction">direction</param>
        /// <returns></returns>
        [HttpGet("CompareData")]
        public ActionResult<string> CompareData(Enviornment env, Data dataMap, ExportFrom direction)
        {
            return _DataMigrationRepository.GenericCompareDatabases(dataMap, direction);
        }

        /// <summary>
        /// This method is used to Remove And Bulk Update Again
        /// </summary>
        /// <param name="env">Enviornment</param>
        /// <param name="dataMap"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        [HttpGet("RemoveAndBulkUpdateAgain")]
        public ActionResult<string> RemoveAndBulkUpdateAgain(Enviornment env, ExportFrom direction, Data dataMap, [Required] string AccessCode)
        {
            if (AccessCode == ApplicationConstants.ACCESSCODE)
            {
                if (direction == ExportFrom.ElasticToMongo)
                {
                    return _DataMigrationRepository.RemoveAndBulkUpdateAgainESToMongo(dataMap);
                }
                else
                {
                    return _DataMigrationRepository.RemoveAndBulkUpdateAgainMongoToES(dataMap);
                }
            }
            else
            {
                return "Please enter correct access code to use this API.";
            }
        }

        #region IgnoreApi = true

        /// <summary>
        /// This method is used to data bulk migrate from mongo to elastic
        /// </summary>
        /// <param name="env">Enviornment</param>
        /// <returns></returns>
        //[HttpGet("BulkInsert")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //public ActionResult<string> BulkInsert(Enviornment env, Direction direction)
        //{
        //    if (direction == Direction.ElasticToMongo)
        //    {
        //        return _DataMigrationRepository.BulkInsertESToMongo(env);
        //    }
        //    else
        //    {
        //        return _DataMigrationRepository.BulkInsertMongoToES(env);
        //    }
        //}

        /// <summary>
        /// This method is used to Compare Elastic with Mongo based on date
        /// </summary>
        /// <param name="env">Enviornment</param>
        /// <param name="dateRange">DateRange</param>
        /// <returns></returns>
        //[HttpGet("CompareESwithMongoCount")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //public ActionResult<string> CompareESwithMongoCount(Enviornment env, DateRange dateRange)
        //{
        //    return _DataMigrationRepository.CompareESwithMongoCount(env, dateRange);
        //}


        /// <summary>
        /// This method is used to Insert Update Mongo To Elastic (SSCS only)
        /// </summary>
        /// <param name="env">Enviornment</param>
        /// <param name="dateRange">dateRange</param>
        /// <returns></returns>
        //[HttpGet("InsertUpdateModifedRecordsForSSCS")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //public ActionResult<string> InsertUpdateMongoToESForModifedRecordsForSSCS(Enviornment env, DateRange dateRange, Direction direction)
        //{
        //    if (direction == Direction.MongoToElastic)
        //    {
        //        return _DataMigrationRepository.GetAndSetModifiedDataForSSCSMongoToES(dateRange, true);
        //    }
        //    else
        //    {
        //        return _DataMigrationRepository.GetAndSetModifiedDataForSSCSESToMongo(dateRange, true);
        //    }
        //}

        #endregion
    }
}
