using MongoToElastic.Models;
using MongoToElastic.Models.Enums;
using System.Text;

namespace MongoToElastic.Repository
{
    public class MongoToElasticService : IMongoToElastic
    {
        #region class level variable

        private readonly IMongoHelper _mongoHelper;
        private readonly IElasticHelper _elasticHelper;

        #endregion 

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="elasticHelper"></param>
        /// <param name="mongoHelper"></param>
        public MongoToElasticService(IElasticHelper elasticHelper, IMongoHelper mongoHelper)
        {
            _mongoHelper = mongoHelper;
            _elasticHelper = elasticHelper;
        }

        #region public method

        #region single insert
        /// <summary>
        /// This function is used to Read Elastic Data
        /// </summary>
        /// <param name="dataMap"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public string ReadElasticData(Data dataMap, Enviornment env)
        {
            switch (dataMap)
            {
                case Data.Group:
                    return Newtonsoft.Json.JsonConvert.SerializeObject(_elasticHelper.ReadElasticData<Group>());
                case Data.OrderCodesToroleGroups:
                    return Newtonsoft.Json.JsonConvert.SerializeObject(_elasticHelper.ReadElasticData<OrderCodesToRoleGroups>());
                case Data.RightGroup:
                    return Newtonsoft.Json.JsonConvert.SerializeObject(_elasticHelper.ReadElasticData<RightGroup>());
                case Data.RoleGroup:
                    return Newtonsoft.Json.JsonConvert.SerializeObject(_elasticHelper.ReadElasticData<RoleGroup>());
                case Data.VariantToGroups:
                    return Newtonsoft.Json.JsonConvert.SerializeObject(_elasticHelper.ReadElasticData<VariantToGroups>());
                case Data.GiiProductAccessAdminOrderCode:
                    return Newtonsoft.Json.JsonConvert.SerializeObject(_elasticHelper.ReadElasticData<GiiProductAccessAdminOrderCode>());
                case Data.CatalogSource:
                    return Newtonsoft.Json.JsonConvert.SerializeObject(_elasticHelper.ReadElasticData<CatalogSource>());
                default:
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("Summary(Group):\n");
                    stringBuilder.Append(ReadElasticData(Data.Group, env));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(RightGroup):\n");
                    stringBuilder.Append(ReadElasticData(Data.RightGroup, env));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(RoleGroup):\n");
                    stringBuilder.Append(ReadElasticData(Data.RoleGroup, env));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(VariantToGroups):\n");
                    stringBuilder.Append(ReadElasticData(Data.VariantToGroups, env));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(OrderCodesToroleGroups):\n");
                    stringBuilder.Append(ReadElasticData(Data.OrderCodesToroleGroups, env));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(GiiProductAccessAdminOrderCode):\n");
                    stringBuilder.Append(ReadElasticData(Data.GiiProductAccessAdminOrderCode, env));
                    return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// This function is used to Read Mongo Data
        /// </summary>
        /// <param name="dataMap"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public string ReadMongoData(Data dataMap, Enviornment env)
        {
            dynamic result = null;
            switch (dataMap)
            {

                case Data.Group:
                    result = _mongoHelper.ReadMongoData<Group>();
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                case Data.OrderCodesToroleGroups:
                    result = _mongoHelper.ReadMongoData<OrderCodesToRoleGroups>();
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                case Data.RightGroup:
                    result = _mongoHelper.ReadMongoData<RightGroup>();
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                case Data.RoleGroup:
                    result = _mongoHelper.ReadMongoData<RoleGroup>();
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                case Data.VariantToGroups:
                    result = _mongoHelper.ReadMongoData<VariantToGroups>();
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                case Data.GiiProductAccessAdminOrderCode:
                    result = _mongoHelper.ReadMongoData<GiiProductAccessAdminOrderCode>();
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                case Data.CatalogSource:
                    result = _mongoHelper.ReadMongoData<CatalogSource>();
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                default:
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("Summary(Group):\n");
                    stringBuilder.Append(ReadMongoData(Data.Group, env));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(RightGroup):\n");
                    stringBuilder.Append(ReadMongoData(Data.RightGroup, env));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(RoleGroup):\n");
                    stringBuilder.Append(ReadMongoData(Data.RoleGroup, env));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(VariantToGroups):\n");
                    stringBuilder.Append(ReadMongoData(Data.VariantToGroups, env));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(OrderCodesToroleGroups):\n");
                    stringBuilder.Append(ReadMongoData(Data.OrderCodesToroleGroups, env));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(GiiProductAccessAdminOrderCode):\n");
                    stringBuilder.Append(ReadMongoData(Data.GiiProductAccessAdminOrderCode, env));
                    return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// This function is used to Insert Data From Mongo To Elastic
        /// </summary>
        /// <param name="dataMap"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public string InsertDataFromMongoToElastic(Data dataMap, Enviornment env)
        {
            switch (dataMap)
            {
                case Data.All:
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(InsertDataFromMongoToElastic(Data.Group, env));
                    stringBuilder.Append("\n");
                    stringBuilder.Append(InsertDataFromMongoToElastic(Data.GiiProductAccessAdminOrderCode, env));
                    stringBuilder.Append("\n");
                    stringBuilder.Append(InsertDataFromMongoToElastic(Data.OrderCodesToroleGroups, env));
                    stringBuilder.Append("\n");
                    stringBuilder.Append(InsertDataFromMongoToElastic(Data.RightGroup, env));
                    stringBuilder.Append("\n");
                    stringBuilder.Append(InsertDataFromMongoToElastic(Data.RoleGroup, env));
                    stringBuilder.Append("\n");
                    stringBuilder.Append(InsertDataFromMongoToElastic(Data.VariantToGroups, env));
                    return stringBuilder.ToString();
                case Data.Group:
                    List<Group> mongoData = _mongoHelper.ReadMongoData<Group>();
                    return _elasticHelper.UpdateGroupMongoToElastic(mongoData);
                case Data.OrderCodesToroleGroups:
                    List<OrderCodesToRoleGroups> orderCodesToRoleGroups = _mongoHelper.ReadMongoData<OrderCodesToRoleGroups>();
                    return _elasticHelper.UpdateOrderCodesToRoleGroupsMongoToElastic(orderCodesToRoleGroups);
                case Data.RightGroup:
                    List<RightGroup> rightGroup = _mongoHelper.ReadMongoData<RightGroup>();
                    return _elasticHelper.UpdateRightGroupMongoToElastic(rightGroup);
                case Data.RoleGroup:
                    List<RoleGroup> roleGroup = _mongoHelper.ReadMongoData<RoleGroup>();
                    return _elasticHelper.UpdateRoleGroupMongoToElastic(roleGroup);
                case Data.VariantToGroups:
                    List<VariantToGroups> variantToGroups = _mongoHelper.ReadMongoData<VariantToGroups>();
                    return _elasticHelper.UpdateVariantToGroupsMongoToElastic(variantToGroups);
                case Data.GiiProductAccessAdminOrderCode:
                    List<GiiProductAccessAdminOrderCode> giiProductAccessAdminOrderCode = _mongoHelper.ReadMongoData<GiiProductAccessAdminOrderCode>();
                    return _elasticHelper.UpdateGiiProductAccessAdminOrderCodeMongoToElastic(giiProductAccessAdminOrderCode);
                case Data.CatalogSource:
                    List<CatalogSource> catalogSource = _mongoHelper.ReadMongoData<CatalogSource>();
                    return _elasticHelper.UpdateCatalogSourceMongoToElastic(catalogSource);
                default:
                    return "Invalid Input";
            }
        }

        public string CheckInstance()
        {
            StringBuilder stringBuilder = new StringBuilder();
            var mongoClient = _mongoHelper.GetClient();
            var endpoints = ((MongoDB.Driver.Core.Clusters.ICluster)mongoClient.Client.Cluster).Settings.EndPoints;
            foreach (var item in endpoints)
            {
                stringBuilder.AppendLine(string.Format("{0} {1}", "Mongo Client", ((System.Net.DnsEndPoint)item).Host));

            }
            var _ElasticClient = _elasticHelper.GetClient();
            var nodes = _ElasticClient.ConnectionSettings.ConnectionPool.Nodes;
            foreach (var item in nodes)
            {
                stringBuilder.AppendLine(string.Format("{0} {1}", "Elastic Client database", Newtonsoft.Json.JsonConvert.SerializeObject(item.Uri.OriginalString)));
            }
            return stringBuilder.ToString();
        }
        #endregion


        #endregion
    }
}
