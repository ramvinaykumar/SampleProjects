namespace MongoDB.API.Interface
{
    /// <summary>
    /// DB Setting Interface
    /// </summary>
    public interface IEmployeeDBSettings
    {
        /// <summary>
        /// Collection Name
        /// </summary>
        string EmployeesCollectionName { get; set; }

        /// <summary>
        /// Connection string name
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Database name
        /// </summary>
        string DatabaseName { get; set; }
    }
}
