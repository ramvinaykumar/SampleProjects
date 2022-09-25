using MongoDB.Bson;
using MongoDB.Driver;
using MongoToElastic.Helpers;
using MongoToElastic.Models;
using MongoToElastic.Models.Enums;
using System.Text;

namespace MongoToElastic.Repository
{
    public class MongoService : IMongoHelper
    {
        private IMongoDatabase _IMongoDatabase;
        private readonly IChangeInfrastructure _ChangeInfrastructure;

        public MongoService(IChangeInfrastructure changeInfrastructure, IHttpContextAccessor httpContext)
        {
            _ChangeInfrastructure = changeInfrastructure;
            SetMongoClientEnvironment(_ChangeInfrastructure.GetAndSetEnvironment(httpContext.HttpContext));
        }

        private IMongoDatabase GetInstance(String connectionString, string database)
        {
            var client = new MongoClient(connectionString);
            return client.GetDatabase(database);
        }

        private void SetMongoClientEnvironment(Enviornment env)
        {
            if (_ChangeInfrastructure.UpdateServices(env))
            {
                _IMongoDatabase = GetInstance(_ChangeInfrastructure.GetServiceURL(ApplicationConstants.MONGOSOLUTIONCONNECTIONSTRING, env), _ChangeInfrastructure.GetServiceURL(ApplicationConstants.MONGOSOLUTIONDATABASE, env));
            }
            else
            {
                _IMongoDatabase = GetInstance(_ChangeInfrastructure.GetMongoDataFromInfra(ApplicationConstants.INFRAMONGOCONNECTIONURL), _ChangeInfrastructure.GetMongoDataFromInfra(ApplicationConstants.INFRAMONGOSOLUTIONDATABASEPATH));
            }
        }

        /// <summary>
        /// Get data by create date  from mongo
        /// </summary>
        /// <param name="pagingData"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public List<SolutionStructureCatalog> GetFilterData(FilterData pagingData = null, string columnName = "")
        {
            if (pagingData != null)
            {
                var filter = Builders<SolutionStructureCatalog>.Filter.Gte(ExpressionBuilder.BuildExpressionForUpdate(columnName), pagingData.StartDate.Date)
                               & Builders<SolutionStructureCatalog>.Filter.Lte(ExpressionBuilder.BuildExpressionForUpdate(columnName), pagingData.EndDate.Date);
                return GetCollection<SolutionStructureCatalog>().Find(filter).ToList();
            }
            else
            {
                return ReadMongoData<SolutionStructureCatalog>();
            }
        }

        /// <summary>
        /// GetMongoDataMonthAndYear
        /// </summary>
        /// <param name="env"></param>
        /// <param name="mongoDatabase"></param>
        /// <returns></returns>
        public List<FilterData> GroupByMonthAndYear()
        {
            List<FilterData> filterDataModelList = new List<FilterData>();
            var filterBuilder = Builders<SolutionStructureCatalog>.Filter;
            var GroupDocument = new BsonDocument
                    {{ "_id",new BsonDocument {{ "month",new BsonDocument("$month", "$ModifiedDate") }, { "year", new BsonDocument("$year", "$ModifiedDate") } } } ,
                { "count",new BsonDocument { { "$sum", 1 } } }
            };
            var GroupCollection = GetCollection<SolutionStructureCatalog>().Aggregate().
                Group(GroupDocument).
                Sort(Builders<BsonDocument>.Sort.Ascending("_id.year").Ascending("_id.month")).ToList();

            GroupCollection.ForEach(x =>
            {
                filterDataModelList.Add(new FilterData(Convert.ToInt32(x["_id"]["month"]), Convert.ToInt32(x["_id"]["year"]), Convert.ToInt32(x["count"])));
            });
            return filterDataModelList;
        }

        public List<FilterData> GroupByDate(FilterData pagingData)
        {
            List<FilterData> filterDataModelList = new List<FilterData>();
            var filterBuilder = Builders<SolutionStructureCatalog>.Filter;
            var GroupDocument = new BsonDocument
                    {{ "_id",new BsonDocument {{ "day",new BsonDocument("$dayOfMonth", "$ModifiedDate") },
                     { "month", new BsonDocument("$month", "$ModifiedDate") }
                    ,{ "year",new BsonDocument("$year", "$ModifiedDate") }
                    } } ,
                { "count",new BsonDocument { { "$sum", 1 } } }
            };
            var filter = Builders<SolutionStructureCatalog>.Filter.Gte(x => x.ModifiedDate, pagingData.StartDate.Date)
                   & Builders<SolutionStructureCatalog>.Filter.Lte(x => x.ModifiedDate, pagingData.EndDate.Date);

            var GroupCollection = GetCollection<SolutionStructureCatalog>().Aggregate().Match(filter).
                Group(GroupDocument).
                Sort(Builders<BsonDocument>.Sort.Descending("_id.year").Descending("_id.month").Descending("_id.day")).ToList();

            GroupCollection.ForEach(x =>
            {
                filterDataModelList.Add(new FilterData() { StartDate = new DateTime(Convert.ToInt32(x["_id"]["year"]), Convert.ToInt32(x["_id"]["month"]), Convert.ToInt32(x["_id"]["day"])), Count = Convert.ToInt32(x["count"]) });
            });
            return filterDataModelList;
        }

        /// <summary>
        /// This function is used for Read Mongo client data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> ReadMongoData<T>() where T : class => GetCollection<T>().Find<T>(FilterDefinition<T>.Empty).ToList();

        /// <summary>
        /// This function is used for Read Mongo client data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<SolutionStructureCatalog> ReadSSCModelPagingData(int pageNo, int pageSize = ApplicationConstants.PAGESIZE)
        {
            return GetCollection<SolutionStructureCatalog>().Find(FilterDefinition<SolutionStructureCatalog>.Empty).SortBy(x => x.ModifiedDate).Skip(pageNo * pageSize).Limit(pageSize).ToList();
        }

        /// <summary>
        /// Get Count
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public long GetCount<T>() => GetCollection<T>().Find(FilterDefinition<T>.Empty).CountDocuments();
        
        public IMongoDatabase GetClient() => _IMongoDatabase;

        public bool BulkInsertElasticData(List<SolutionStructureCatalog> elasticSSCModel, StringBuilder stringBuilder)
        {
            var listWrites = new List<WriteModel<SolutionStructureCatalog>>();
            elasticSSCModel.ForEach(x =>
            {
                listWrites.Add(new InsertOneModel<SolutionStructureCatalog>(x));
            });

            var resultWrites = GetCollection<SolutionStructureCatalog>().BulkWrite(listWrites, new BulkWriteOptions
            {
                IsOrdered = false
            });
            stringBuilder.Append(string.Format("\t elastic to mongo bulk insert {0}{1}", resultWrites.RequestCount, resultWrites.InsertedCount));
            return resultWrites.IsAcknowledged;
        }

        public bool GenericBulkInsertElasticData<T>(List<T> elasticSSCModel, StringBuilder stringBuilder)
        {
            var listWrites = new List<WriteModel<T>>();
            elasticSSCModel.ForEach(x =>
            {
                listWrites.Add(new InsertOneModel<T>(x));
            });

            var resultWrites = GetCollection<T>().BulkWrite(listWrites, new BulkWriteOptions
            {
                IsOrdered = false
            });
            stringBuilder.Append(string.Format("\t elastic to mongo bulk insert RequestCount:{0} InsertedCount:{1}", resultWrites.RequestCount, resultWrites.InsertedCount));
            return resultWrites.IsAcknowledged;
        }

        public void DropCollection<T>() { _IMongoDatabase.DropCollection(typeof(T).Name); }

        public IMongoCollection<T> GetCollection<T>() { return _IMongoDatabase.GetCollection<T>(typeof(T).Name); }
    }
}
