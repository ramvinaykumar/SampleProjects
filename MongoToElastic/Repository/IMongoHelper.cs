using MongoDB.Driver;
using MongoToElastic.Models;
using System.Text;

namespace MongoToElastic.Repository
{
    public interface IMongoHelper
    {
        public List<FilterData> GroupByMonthAndYear();
        public List<T> ReadMongoData<T>() where T : class;
        public List<SolutionStructureCatalog> GetFilterData(FilterData pagingData = null, string columnName = "");
        public List<FilterData> GroupByDate(FilterData oFilterDataModel);
        public List<SolutionStructureCatalog> ReadSSCModelPagingData(int pageNo, int pageSize = ApplicationConstants.PAGESIZE);
        public long GetCount<T>();
        public IMongoDatabase GetClient();
        bool BulkInsertElasticData(List<SolutionStructureCatalog> elasticSSCModel, StringBuilder stringBuilder);
        public void DropCollection<T>();
        public bool GenericBulkInsertElasticData<T>(List<T> elasticSSCModel, StringBuilder stringBuilder);
    }
}
