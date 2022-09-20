using MongoToElastic.Models.Enums;

namespace MongoToElastic.Repository
{
    public interface IMigration
    {
        /// <summary>
        /// GenericCompareDatabases
        /// </summary>
        /// <param name="dateRange"></param>
        /// <param name="dataMap"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public string GenericCompareDatabases(Data dataMap, ExportFrom direction);

        /// <summary>
        /// RemoveAndBulkUpdateAgainESToMongo
        /// </summary>
        /// <param name="dataMap">dataMap</param>
        /// <returns></returns>
        public string RemoveAndBulkUpdateAgainESToMongo(Data dataMap);

        /// <summary>
        /// RemoveAndBulkUpdateAgain
        /// </summary>
        /// <returns></returns>
        public string RemoveAndBulkUpdateAgainMongoToES(Data dataMap);
    }
}
