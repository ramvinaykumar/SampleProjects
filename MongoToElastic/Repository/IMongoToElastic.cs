using MongoToElastic.Models.Enums;

namespace MongoToElastic.Repository
{
    /// <summary>
    /// This interface is used to read and set elastic data from mongo
    /// </summary>
    public interface IMongoToElastic
    {
        /// <summary>
        /// ReadMongoData
        /// </summary>
        /// <param name="dataMap"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        string ReadMongoData(Data dataMap, Enviornment env);

        /// <summary>
        /// ReadElasticData
        /// </summary>
        /// <param name="dataMap"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        string ReadElasticData(Data dataMap, Enviornment env);

        /// <summary>
        /// InsertDataFromMongoToElastic
        /// </summary>
        /// <param name="dataMap"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        string InsertDataFromMongoToElastic(Data dataMap, Enviornment env);

        /// <summary>
        /// CheckInstance
        /// </summary>
        /// <returns></returns>
        string CheckInstance();

    }
}
