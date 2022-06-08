using ElasticSearch.Model;
using Nest;
using System;

namespace ElasticSearch.DBSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started creating Database in Elastic search");
            CreateIndex();
            CreateMappings();
            Console.WriteLine("Database created.");
        }

        /// <summary>
        /// 
        /// </summary>
        public static void CreateIndex()
        {
            ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            settings.DefaultIndex("employeestore");
            ElasticClient client = new ElasticClient(settings);
            client.Indices.Delete(Indices.Index("employeestore"));
            var indexSettings = client.Indices.Exists("employeestore");
            if (!indexSettings.Exists)
            {
                IndexName indexName = "employeestore";
                //indexName. = "employeestore";
                var response = client.Indices.Create(indexName);
            }

            if (indexSettings.Exists)
            {
                Console.WriteLine("Created");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public static void CreateMappings()
        {
            ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            settings.DefaultIndex("employeestore");
            ElasticClient esClient = new ElasticClient(settings);
            esClient.Map<Employee>(m =>
            {
                var putMappingDescriptor = m.Index(Indices.Index("employeestore")).AutoMap();
                return putMappingDescriptor;
            });
        }
    }
}
