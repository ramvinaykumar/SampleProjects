using System.Text.RegularExpressions;
using System.Text;
using MongoToElastic.Models;
using Nest;
using Group = MongoToElastic.Models.Group;

namespace MongoToElastic.Repository
{
    public interface IElasticHelper
    {
        public ElasticOutput<T> ReadElasticData<T>(int size = 10, bool getAllRecords = true, int from = 0, Func<SortDescriptor<T>, IPromise<IList<ISort>>> SortBy = null) where T : class;
        public bool SaveToElastic<T>(T data) where T : class;
        public string UpdateGroupMongoToElastic(List<Group> mongoData);
        public string UpdateRoleGroupMongoToElastic(List<RoleGroup> mongoData);
        public string UpdateCatalogSourceMongoToElastic(List<CatalogSource> mongoData);
        public string UpdateGiiProductAccessAdminOrderCodeMongoToElastic(List<GiiProductAccessAdminOrderCode> mongoData);
        public string UpdateVariantToGroupsMongoToElastic(List<VariantToGroups> mongoData);
        public string UpdateRightGroupMongoToElastic(List<RightGroup> mongoData);
        public string UpdateOrderCodesToRoleGroupsMongoToElastic(List<OrderCodesToRoleGroups> mongoData);
        public List<FilterData> GroupByDate(FilterData oFilterDataModel);
        public List<FilterData> GroupByMonthAndYear();
        public bool RemoveIndex<T>();
        public string UpdateRecords(List<ModifiedOrAddedSSCM> modifiedOrAddedSSCM);
        public bool BulkInsertElasticData(List<SolutionStructureCatalog> documents, StringBuilder result);
        public void DeleteRecords(List<ModifiedOrAddedSSCM> modifiedOrAddedSSCM);
        public IElasticClient GetClient();
        public int GetCount<T>(Func<QueryContainerDescriptor<T>, QueryContainer> queryDiscriptor = null) where T : class;
        public ElasticOutput<SolutionStructureCatalog> ReadSSCModelPagingData(int pageNo, int pageSize = ApplicationConstants.PAGESIZE);
        public bool GenericBulkInsertElasticData<T>(List<T> documents, StringBuilder sb) where T : class;
    }
}
