using MongoToElastic.Models.Enums;

namespace MongoToElastic.Repository
{
    /// <summary>
    /// Interface for Change Infra
    /// </summary>
    public interface IChangeInfrastructure
    {
        /// <summary>
        /// UpdateServices
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        bool UpdateServices(Enviornment env);

        /// <summary>
        /// GetServiceURL
        /// </summary>
        /// <param name="path"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        string GetServiceURL(string path, Enviornment env);

        /// <summary>
        /// GetMongoDataFromInfra
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string GetMongoDataFromInfra(string path);

        /// <summary>
        /// GetESDataFromInfra
        /// </summary>
        /// <returns></returns>
        string GetESDataFromInfra();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        Enviornment GetAndSetEnvironment(Microsoft.AspNetCore.Http.HttpContext httpContext);

    }
}
