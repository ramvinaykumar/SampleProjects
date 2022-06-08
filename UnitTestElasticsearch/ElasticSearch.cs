using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTestElasticsearch.Model;

namespace UnitTestElasticsearch
{
    public class ElasticSearch
    {
        ElasticClient client = null;

        public ElasticSearch()
        {
            var uri = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(uri);
            client = new ElasticClient(settings);
            settings.DefaultIndex("city");
        }

        public List<City> GetResult()
        {
            if (client.Indices.Exists("city").Exists)
            {
                var response = client.Search<City>();
                return response.Documents.ToList();
            }
            return null;
        }

        public List<City> GetResult(string condition)
        {
            if (client.Indices.Exists("city").Exists)
            {
                var query = condition;

                return client.SearchAsync<City>(s => s
                .From(0)
                .Take(10)
                .Query(qry => qry
                    .Bool(b => b
                        .Must(m => m
                            .QueryString(qs => qs
                                .DefaultField("_all")
                                .Query(query)))))).Result.Documents.ToList();
            }
            return null;
        }

        public void AddNewIndex(City model)
        {
            client.IndexAsync<City>(model, null);
        }
    }
}
