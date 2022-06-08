using MongoDB.API.Interface;

namespace MongoDB.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeDBSettings : IEmployeeDBSettings
    {
        /// <summary>
        /// Collection Name
        /// </summary>
        public string EmployeesCollectionName { get; set; }

        /// <summary>
        /// Connection string name
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Database name
        /// </summary>
        public string DatabaseName { get; set; }
    }
}
