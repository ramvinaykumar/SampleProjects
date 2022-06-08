using MongoDB.API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace MongoDB.API.Services
{
    /// <summary>
    /// Book service class
    /// </summary>
    public class BookService
    {
        /// <summary>
        /// Read only variable of IMongoCollection
        /// </summary>
        private readonly IMongoCollection<Book> _books;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="settings">IBookstoreDatabaseSettings settings</param>
        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        /// <summary>
        /// Get the list of book
        /// </summary>
        /// <returns></returns>
        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        /// <summary>
        /// Get book data by book id
        /// </summary>
        /// <param name="id">string id</param>
        /// <returns></returns>
        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        /// <summary>
        /// Create new record for book
        /// </summary>
        /// <param name="book">Book object</param>
        /// <returns></returns>
        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">string id</param>
        /// <param name="bookIn"></param>
        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        /// <summary>
        /// Remove data by book object
        /// </summary>
        /// <param name="bookIn">Book object</param>
        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        /// <summary>
        /// Remove data by id
        /// </summary>
        /// <param name="id">string id</param>
        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}
