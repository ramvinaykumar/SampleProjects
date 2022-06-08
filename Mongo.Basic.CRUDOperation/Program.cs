using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace Mongo.Basic.CRUDOperation
{
    /// <summary>
    /// Program Class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">string[] args</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            InsertOperation();

            Console.WriteLine("Press Enter");
            Console.ReadLine();
        }

        /// <summary>
        /// Insert data into Mongo DB
        /// </summary>
        private static void InsertOperation()
        {
            var mongoDbConnection = "mongodb://localhost";
            var client = new MongoClient(mongoDbConnection);
            var db = client.GetDatabase("Employee");
            var collection = db.GetCollection<BsonDocument>("EmployeeDetails ");

            for (int i = 1; i <= 5; i++)
            {
                var name = RandomWordGeneration(i);
                var city = RandomWordGeneration(i);
                var age = 20 + i;

                var emp = new BsonDocument
                {
                    {"Name", name},
                    {"City", city},
                    {"Age", age},
                    {"Department","Software Development"},
                    {"Technology","Dot Net"}
                };
                collection.InsertOneAsync(emp);
            }
        }

        /// <summary>
        /// Generate the random string based on given length
        /// </summary>
        /// <param name="requestedLength">int requestedLength</param>
        /// <returns>Returns Random generated word</returns>
        private static string RandomWordGeneration(int requestedLength)
        {
            Random rnd = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };
            string[] vowels = { "a", "e", "i", "o", "u" };

            string word = "";

            if (requestedLength == 1)
            {
                word = GetRandomLetter(rnd, vowels);
            }
            else
            {
                for (int i = 0; i < requestedLength; i += 2)
                {
                    word += GetRandomLetter(rnd, consonants) + GetRandomLetter(rnd, vowels);
                }

                word = word.Replace("q", "qu").Substring(0, requestedLength); // We may generate a string longer than requested length, but it doesn't matter if cut off the excess.
            }

            return word;
        }

        /// <summary>
        /// Get random letter
        /// </summary>
        /// <param name="rnd">Random rnd</param>
        /// <param name="letters">string[] letters</param>
        /// <returns>Returns random letters</returns>
        private static string GetRandomLetter(Random rnd, string[] letters)
        {
            return letters[rnd.Next(0, letters.Length - 1)];
        }
    }
}
