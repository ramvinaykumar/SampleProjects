namespace MongoDB.API.Models
{
    /// <summary>
    /// Interface for book store settings
    /// </summary>
    public interface IBookstoreDatabaseSettings
    {
        /// <summary>
        /// Book collection name
        /// </summary>
        string BooksCollectionName { get; set; }

        /// <summary>
        /// DB Connection string
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// DB name
        /// </summary>
        string DatabaseName { get; set; }
    }
}
