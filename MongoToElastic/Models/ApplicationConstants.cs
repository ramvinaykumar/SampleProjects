namespace MongoToElastic.Models
{
    public static class ApplicationConstants
    {
        public const string AppName = "Mongo-To-Elastic";
        public const string ApiDescription = "MongoToElastic";
        public const string SwaggerEndpoint = "/swagger/v1/swagger.json";
        public const int PAGESIZE = 10000;
        public const string MONGOSOLUTIONCONNECTIONSTRING = "infrastructure:Mongo:Solution";
        public const string ELASTICCONNECTIONSTRING = "infrastructure:ElasticSearch:MDC";
        public const string MONGOSOLUTIONDATABASE = "infrastructure:Mongo:Solutions:Database";
        public const string INFRAMONGOSOLUTIONDATABASEPATH = "Solutions:Database";
        public const string INFRAMONGOCONNECTIONURL = "Solution";
        public const string MONGO_CREATEDDATE = "CreatedDate";
        public const string MONGO_MODIFIEDDATE = "ModifiedDate";
        public const string ELASTIC_CREATEDDATE = "createdDate";
        public const string ELASTIC_MODIFIEDDATE = "modifiedDate";
        public const string ACCESSCODE = "484CF186-FB87-4B0E-8A92-59FC1E5048A4";
    }
}
