namespace MongoDB.API.Models
{
    /// <summary>
    /// DB settings for book store
    /// </summary>
    public class BookstoreDatabaseSettings : IBookstoreDatabaseSettings
    {
        /// <summary>
        /// Book collection name
        /// </summary>
        public string BooksCollectionName { get; set; }

        /// <summary>
        /// DB Connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// DB name
        /// </summary>
        public string DatabaseName { get; set; }
    }
}
