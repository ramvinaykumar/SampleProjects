using Elasticsearch.Net;
using MongoToElastic.Models;
using MongoToElastic.Models.Enums;
using MongoToElastic.Repository;
using Nest;
using System.Text;

namespace MongoToElastic.Helpers
{
    public class ElasticHelper : IElasticHelper
    {
        private IElasticClient _ElasticClient;
        private readonly IChangeInfrastructure _ChangeInfrastructure;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="changeInfrastructure"></param>
        public ElasticHelper(IChangeInfrastructure changeInfrastructure, IHttpContextAccessor httpContext)
        {
            _ChangeInfrastructure = changeInfrastructure;
            SetElasticClientEnviornment(_ChangeInfrastructure.GetAndSetEnvironment(httpContext.HttpContext));
        }

        /// <summary>
        /// This function is to get elastic client based on connection string 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        ///// 
        //   .PrettyJson().DisableDirectStreaming()
        //             .OnRequestCompleted(details=> Debug.WriteLine(Encoding.UTF8.GetString(details.RequestBodyInBytes)));
        private ElasticClient GetElasticClient(string connectionString)
        {
#if DEBUG


            return new ElasticClient(new ConnectionSettings(new Uri(connectionString))
                            .DisableDirectStreaming().EnableDebugMode().OnRequestCompleted(x =>
                            {
                                try
                                {
                                    var request = Encoding.UTF8.GetString(x.RequestBodyInBytes);

                                }
                                catch (Exception)
                                {

                                }

                            }));

#else

            return new ElasticClient(new ConnectionSettings(new Uri(connectionString)));
#endif
        }

        /// <summary>
        /// Set Elastic Client Enviornment
        /// </summary>
        /// <param name="env"></param>
        private void SetElasticClientEnviornment(Enviornment env)
        {

            if (_ChangeInfrastructure.UpdateServices(env))
            {
                _ElasticClient = GetElasticClient(_ChangeInfrastructure.GetServiceURL(ApplicationConstants.ELASTICCONNECTIONSTRING, env));
            }
            else
            {
                _ElasticClient = GetElasticClient(_ChangeInfrastructure.GetESDataFromInfra());
            }

        }

        private void ExtandSizeOfElasticClient<T>(int size = 30000) => _ElasticClient.Indices.UpdateSettingsAsync(typeof(T).Name.ToLower(), s => s.IndexSettings(i => i.Setting(UpdatableIndexSettings.MaxResultWindow, size))).Wait();
        private void RevertExtendSizeOfElasticClient<T>() => _ElasticClient.Indices.UpdateSettingsAsync(typeof(T).Name.ToLower(), s => s.IndexSettings(i => i.Setting(UpdatableIndexSettings.MaxResultWindow, ApplicationConstants.PAGESIZE)));

        /// <summary>
        /// This function is used for Save To Elastic data
        /// </summary>
        /// <returns></returns>
        public bool SaveToElastic<T>(T data) where T : class
        {
            return Result.Created == _ElasticClient.Index<T>(data, s => s.Index(typeof(T).Name.ToLower())).Result;
        }
        #region class wise update

        /// <summary>
        /// This function is used to upgrade group to mongo to elastic  
        /// </summary>
        /// <param name="mongoData"></param>
        /// <returns></returns>
        public string UpdateGroupMongoToElastic(List<Group> mongoData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Summary(Group):\n");
            var elasticOutputModel = ReadElasticData<Group>(mongoData.Count, false);
            if (!elasticOutputModel.ConnectionSuccessful)
            {
                stringBuilder.Append(string.Format("\t{Elastic Client Message:{1}\n", elasticOutputModel.ErrorMsg));
                return stringBuilder.ToString();
            }

            List<Group> elasticData = elasticOutputModel.ModelList;
            foreach (var item in mongoData)
            {
                if (!elasticData.Any(x => x.Id == item.Id && x.Name == item.Name))
                {
                    bool result = SaveToElastic<Group>(item);
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), result ? "Created" : "Try Again"));

                }
                else
                {
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), "Already added"));
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// This function is used to upgrade OrderCodesToRoleGroups to mongo to elastic  
        /// </summary>
        /// <param name="mongoData"></param>
        /// <returns></returns>
        public string UpdateOrderCodesToRoleGroupsMongoToElastic(List<OrderCodesToRoleGroups> mongoData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Summary(OrderCodesToRoleGroups):\n");
            var elasticOutputModel = ReadElasticData<OrderCodesToRoleGroups>(mongoData.Count, false);
            if (!elasticOutputModel.ConnectionSuccessful)
            {
                stringBuilder.Append(string.Format("\t{Elastic Client Message:{1}\n", elasticOutputModel.ErrorMsg));
                return stringBuilder.ToString();
            }

            List<OrderCodesToRoleGroups> elasticData = elasticOutputModel.ModelList;
            foreach (var item in mongoData)
            {
                if (!elasticData.Any(x => x.OrderCodeId == item.OrderCodeId && x.Region == item.Region))
                {
                    bool result = SaveToElastic<OrderCodesToRoleGroups>(item);
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), result ? "Created" : "Try Again"));
                }
                else
                {
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), "Already added"));
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// This function is used to upgrade RightGroup to mongo to elastic 
        /// </summary>
        /// <param name="mongoData"></param>
        /// <returns></returns>
        public string UpdateRightGroupMongoToElastic(List<RightGroup> mongoData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Summary(RightGroup):\n");
            var elasticOutputModel = ReadElasticData<RightGroup>(mongoData.Count, false);
            if (!elasticOutputModel.ConnectionSuccessful)
            {
                stringBuilder.Append(string.Format("\t{Elastic Client Message:{1}\n", elasticOutputModel.ErrorMsg));
                return stringBuilder.ToString();
            }

            List<RightGroup> elasticData = elasticOutputModel.ModelList;
            foreach (var item in mongoData)
            {
                if (!elasticData.Any(x => x.Id == item.Id && x.Name == item.Name))
                {
                    bool result = SaveToElastic<RightGroup>(item);
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), result ? "Created" : "Try Again"));
                }
                else
                {
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), "Already added"));
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// This function is used to upgrade RoleGroup to mongo to elastic 
        /// </summary>
        /// <param name="mongoData"></param>
        /// <returns></returns>
        public string UpdateRoleGroupMongoToElastic(List<RoleGroup> mongoData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Summary(RoleGroup):\n");
            var elasticOutputModel = ReadElasticData<RoleGroup>(mongoData.Count, false);
            if (!elasticOutputModel.ConnectionSuccessful)
            {
                stringBuilder.Append(string.Format("\t{Elastic Client Message:{1}\n", elasticOutputModel.ErrorMsg));
                return stringBuilder.ToString();
            }

            List<RoleGroup> elasticData = elasticOutputModel.ModelList;
            foreach (var item in mongoData)
            {
                if (!elasticData.Any(x => x.RoleId == item.RoleId && x.Region == item.Region))
                {
                    bool result = SaveToElastic<RoleGroup>(item);
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), result ? "Created" : "Try Again"));
                }
                else
                {
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), "Already added"));
                }
            }
            return stringBuilder.ToString();

        }

        /// <summary>
        /// This function is used to update VariantToGroups to mongo to elastic 
        /// </summary>
        /// <param name="mongoData"></param>
        /// <returns></returns>
        public string UpdateVariantToGroupsMongoToElastic(List<VariantToGroups> mongoData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Summary(VariantToGroups):\n");
            var elasticOutputModel = ReadElasticData<VariantToGroups>(mongoData.Count, false);
            if (!elasticOutputModel.ConnectionSuccessful)
            {
                stringBuilder.Append(string.Format("\t{Elastic Client Message:{1}\n", elasticOutputModel.ErrorMsg));
                return stringBuilder.ToString();
            }

            List<VariantToGroups> elasticData = elasticOutputModel.ModelList;
            foreach (var item in mongoData)
            {
                if (!elasticData.Any(x => x.Id == item.Id && x.Region == item.Region && x.State == item.State))
                {
                    bool result = SaveToElastic<VariantToGroups>(item);
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), result ? "Created" : "Try Again"));
                }
                else
                {
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), "Already added"));
                }
            }
            return stringBuilder.ToString();

        }

        /// <summary>
        /// This function is used to update GiiProductAccessAdminOrderCode to mongo to elastic 
        /// </summary>
        /// <param name="mongoData"></param>
        /// <returns></returns>
        public string UpdateGiiProductAccessAdminOrderCodeMongoToElastic(List<GiiProductAccessAdminOrderCode> mongoData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Summary(GiiProductAccessAdminOrderCode):\n");
            var elasticOutputModel = ReadElasticData<GiiProductAccessAdminOrderCode>(mongoData.Count, false);
            if (!elasticOutputModel.ConnectionSuccessful)
            {
                stringBuilder.Append(string.Format("\t{Elastic Client Message:{1}\n", elasticOutputModel.ErrorMsg));
                return stringBuilder.ToString();
            }

            List<GiiProductAccessAdminOrderCode> elasticData = elasticOutputModel.ModelList;
            foreach (var item in mongoData)
            {
                if (!elasticData.Any(x => x.OrderCodeId == item.OrderCodeId && x.Region == item.Region))
                {
                    bool result = SaveToElastic<GiiProductAccessAdminOrderCode>(item);
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), result ? "Created" : "Try Again"));
                }
                else
                {
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), "Already added"));
                }
            }
            return stringBuilder.ToString();

        }

        /// <summary>
        /// UpdateCatalogSource Mongo To Elastic
        /// </summary>
        /// <param name="mongoData"></param>
        /// <returns></returns>
        public string UpdateCatalogSourceMongoToElastic(List<CatalogSource> mongoData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Summary(CatalogSourceDto):\n");
            var elasticOutputModel = ReadElasticData<CatalogSource>(mongoData.Count, false);
            if (!elasticOutputModel.ConnectionSuccessful)
            {
                stringBuilder.Append(string.Format("\t{Elastic Client Message:{1}\n", elasticOutputModel.ErrorMsg));
                return stringBuilder.ToString();
            }

            List<CatalogSource> elasticData = elasticOutputModel.ModelList;
            foreach (var item in mongoData)
            {
                if (!elasticData.Any(x => x.CustomerSet == item.CustomerSet))
                {
                    bool result = SaveToElastic<CatalogSource>(item);
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), result ? "Created" : "Try Again"));
                }
                else
                {
                    stringBuilder.Append(string.Format("\t{0}:{1}\n", Newtonsoft.Json.JsonConvert.SerializeObject(item), "Already added"));
                }
            }
            return stringBuilder.ToString();

        }
        #endregion

        /// <summary>
        /// This function is to get elastic client based on connection string 
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="sb"></param>
        /// <returns></returns>
        public bool BulkInsertElasticData(List<SolutionStructureCatalog> documents, StringBuilder sb)
        {
            bool result = false;
            Func<List<SolutionStructureCatalog>, StringBuilder, bool> bulkEntry = (x, y) =>
            {
                bool response = false;
                var waitHandle = new CountdownEvent(1);
                var bulkAll = _ElasticClient.BulkAll(documents, b => b
                    .Index("solutionstructurecatalogmodel") /* index */
                    .BackOffRetries(5)
                    .BackOffTime("30s")
                    .RefreshOnCompleted(true)
                    .MaxDegreeOfParallelism(4)
                    .Size(documents.Count).BulkResponseCallback(x =>
                    {
                        if (x.Errors) { sb.AppendLine(string.Format("\t Data not inserted Total Count:{0} ex:{1}", documents.Count, Newtonsoft.Json.JsonConvert.SerializeObject(x))); }
                    })
                );
                bulkAll.Subscribe(new BulkAllObserver(
                     onNext: response =>
                     {
                     },
                     onError: ex =>
                     {
                         response = false;
                         sb.AppendLine(string.Format("\t Data not inserted Total Count:{0} ex:{1}", documents.Count, ex.Message));
                     },
                     onCompleted: () =>
                     {
                         response = true;
                         sb.AppendLine(string.Format("\t Data inserted successfully Total Count:{0}", documents.Count));
                         waitHandle.Signal();
                     })
                    );
                waitHandle.Wait();
                return response;
            };

            result = bulkEntry(documents, sb);
            return result;
        }

        /// <summary>
        /// This function is to get elastic client based on connection string 
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="sb"></param>
        /// <returns></returns>
        public bool GenericBulkInsertElasticData<T>(List<T> documents, StringBuilder sb) where T : class
        {
            bool response = false;
            var waitHandle = new CountdownEvent(1);
            var bulkAll = _ElasticClient.BulkAll(documents, b => b
                .Index(typeof(T).Name.ToLower())
                .BackOffRetries(5)
                .BackOffTime("30s")
                .RefreshOnCompleted(true)
                .MaxDegreeOfParallelism(4)
                .Size(documents.Count).BulkResponseCallback(x =>
                {
                    if (x.Errors) { sb.AppendLine(string.Format("\t Data not inserted Total Count:{0} ex:{1}", documents.Count, Newtonsoft.Json.JsonConvert.SerializeObject(x))); }
                })
            );
            bulkAll.Subscribe(new BulkAllObserver(
                 onNext: response =>
                 {
                 },
                 onError: ex =>
                 {
                     response = false;
                     sb.AppendLine(string.Format("\t Data not inserted Total Count:{0} ex:{1}", documents.Count, ex.Message));
                 },
                 onCompleted: () =>
                 {
                     response = true;
                     sb.AppendLine(string.Format("\t Data inserted successfully Total Count:{0}", documents.Count));
                     waitHandle.Signal();
                 })
                );
            waitHandle.Wait();
            return response;
        }





        public bool RemoveIndex<T>()
        {
            DeleteIndexResponse deleteResponse = _ElasticClient.Indices.Delete(typeof(T).Name.ToLower());
            return deleteResponse.Acknowledged;

        }


        /// <summary>
        /// Update Records
        /// </summary>
        /// <param name="modifiedOrAddedSSCM"></param>
        /// <returns></returns>
        public string UpdateRecords(List<ModifiedOrAddedSSCM> modifiedOrAddedSSCM)
        {
            foreach (var item in modifiedOrAddedSSCM)
            {
                SolutionStructureCatalog oldSSCModel = item.OldSSCModel;
                SolutionStructureCatalog newSSCModel = item.NewSSCModel;

                var scriptParams = new Dictionary<string, object>
                                {
                                    { "Name",newSSCModel.Name},
                                    { "OnlineCatalog",newSSCModel.OnlineCatalog},
                                    { "OfflineCatalog",newSSCModel.OfflineCatalog},
                                    { "WorkFlowType",newSSCModel.WorkFlowType},
                                    { "SolutionStructureType",newSSCModel.SolutionStructureType},
                                    { "TechSpecOrderCode",newSSCModel.TechSpecOrderCode},
                                    { "ModifiedDate",newSSCModel.ModifiedDate}
                                };
                UpdateByQueryResponse updateByQueryResponse = _ElasticClient.UpdateByQuery<SolutionStructureCatalog>(u => u.Index(typeof(SolutionStructureCatalog).Name.ToLower())
                 .Query(ExpressionBuilder.UpdateQueryFilter(oldSSCModel))
                 .Script(script =>
                     script.Source(
                         $"ctx._source.name = params.Name;" +
                         $"ctx._source.onlineCatalog  = params.OnlineCatalog ;" +
                           $"ctx._source.offlineCatalog = params.OfflineCatalog;" +
                         $"ctx._source.workFlowType  = params.WorkFlowType ;" +
                           $"ctx._source.solutionStructureType = params.SolutionStructureType;" +
                         $"ctx._source.techSpecOrderCode  = params.TechSpecOrderCode ;" +
                           $"ctx._source.modifiedDate = params.ModifiedDate;"
                     )
                 .Params(scriptParams))
                 .Conflicts(Conflicts.Proceed)
                 .Refresh(true)
                      );
                if (updateByQueryResponse.Updated > 0)
                {
                    item.Response = true;
                }
            }
            return "";
        }

        /// <summary>
        /// DeleteRecords from elastic
        /// </summary>
        /// <param name="modifiedOrAddedSSCM"></param>
        /// <returns></returns>
        public void DeleteRecords(List<ModifiedOrAddedSSCM> modifiedOrAddedSSCM)
        {
            foreach (var item in modifiedOrAddedSSCM)
            {
                DeleteByQueryResponse deleteByQueryResponse = _ElasticClient.DeleteByQuery<SolutionStructureCatalog>(u => u.Index(typeof(SolutionStructureCatalog).Name.ToLower())
                 .Query(ExpressionBuilder.UpdateQueryFilter(item.OldSSCModel)).Conflicts(Conflicts.Proceed).Refresh(true)
                      );
                if (deleteByQueryResponse.Deleted > 0)
                {
                    item.Response = true;
                }
            }

        }

        /// <summary>
        /// Group By Month And Year
        /// </summary>
        /// <returns></returns>
        public List<FilterData> GroupByMonthAndYear()
        {
            List<FilterData> filterDataModelList = new List<FilterData>();
            var ElasticResult = _ElasticClient.Search<SolutionStructureCatalog>(s => s.Index("solutionstructurecatalogmodel")
            .Aggregations(a => a.DateHistogram("projects_started_per_month", date => date.Field(x => x.ModifiedDate).CalendarInterval(DateInterval.Month))
            ));
            if (ElasticResult.IsValid)
            {
                var results = ((BucketAggregate)ElasticResult.Aggregations["projects_started_per_month"]).Items.ToList();
                foreach (DateHistogramBucket item in results)
                {
                    filterDataModelList.Add(new FilterData(item.Date.Month, item.Date.Year, Convert.ToInt32(item.DocCount)));
                }
            }
            return filterDataModelList;
        }

        /// <summary>
        /// Group By Date
        /// </summary>
        /// <param name="oFilterDataModel"></param>
        /// <returns></returns>
        public List<FilterData> GroupByDate(FilterData oFilterDataModel)
        {
            List<FilterData> filterDataModelList = new List<FilterData>();
            var ElasticResult = _ElasticClient.Search<SolutionStructureCatalog>(s => s.Index("solutionstructurecatalogmodel")
            .Query(q => q.Bool(b => b.Filter(f => f.DateRange(dr => dr.Field("modifiedDate").GreaterThanOrEquals(oFilterDataModel.StartDate)),
              f => f.DateRange(dr => dr.Field("modifiedDate").LessThanOrEquals(oFilterDataModel.EndDate)))))
            .Aggregations(a => a.DateHistogram("projects_started_per_month", date => date.Field(x => x.ModifiedDate).CalendarInterval(DateInterval.Day))
            ));
            if (ElasticResult.IsValid)
            {
                var results = ((BucketAggregate)ElasticResult.Aggregations["projects_started_per_month"]).Items.ToList();
                foreach (DateHistogramBucket item in results)
                {
                    filterDataModelList.Add(new FilterData() { StartDate = item.Date.Date, Count = Convert.ToInt32(item.DocCount) });
                }
            }
            return filterDataModelList;
        }

        /// <summary>
        /// Read Elastic Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="size"></param>
        /// <param name="getAllRecords"></param>
        /// <returns></returns>
        public ElasticOutput<T> ReadElasticData<T>(int size = 10,
            bool getAllRecords = true,
            int from = 0,
          Func<SortDescriptor<T>, IPromise<IList<ISort>>> SortByQuery = null) where T : class
        {
            if (getAllRecords)
            {
                size = GetCount<T>();
                if (size > ApplicationConstants.PAGESIZE)
                {
                    ExtandSizeOfElasticClient<T>(Convert.ToInt32(size));
                }
            }
            ISearchResponse<T> output = null;
            ElasticOutput<T> elasticOutputModel = new ElasticOutput<T>();
            if (from == 0)
            {
                output = _ElasticClient.Search<T>(s => s.Index(typeof(T).Name.ToLower()).Size(size));
            }
            else
            {
                output = _ElasticClient.Search<T>(s => s.Index(typeof(T).Name.ToLower()).From(from).Size(size).Sort(SortByQuery));
            }
            elasticOutputModel.ConnectionSuccessful = output.ApiCall.Success;
            //should not exceed greter then int max size else need to implement paging using scroll api
            if (!elasticOutputModel.ConnectionSuccessful)
            {
                elasticOutputModel.ErrorMsg = output.ApiCall.OriginalException.Message;
            }
            else
            {
                elasticOutputModel.ModelList = (List<T>)output.Documents;
            }
            if (getAllRecords)
            {
                size = GetCount<T>();
                if (size > ApplicationConstants.PAGESIZE)
                {
                    RevertExtendSizeOfElasticClient<T>();
                }
            }
            return elasticOutputModel;
        }
        public IElasticClient GetClient() => _ElasticClient;

        public int GetCount<T>(Func<QueryContainerDescriptor<T>, QueryContainer> queryDiscriptor = null) where T : class
        {
            CountResponse countResponse = null;
            if (queryDiscriptor != null)
            {
                countResponse = _ElasticClient.Count<T>(s => s.Index(typeof(T).Name.ToLower()).Query(queryDiscriptor));

            }
            else
            {
                countResponse = _ElasticClient.Count<T>(s => s.Index(typeof(T).Name.ToLower()));
            }
            return Convert.ToInt32(countResponse.Count);
        }

        public ElasticOutput<SolutionStructureCatalog> ReadSSCModelPagingData(int pageNo, int pageSize = ApplicationConstants.PAGESIZE)
        {
            ElasticOutput<SolutionStructureCatalog> olst = new ElasticOutput<SolutionStructureCatalog>();
            var output = _ElasticClient.Search<SolutionStructureCatalog>(s => s.Index(typeof(SolutionStructureCatalog).Name.ToLower()).From(pageNo * pageSize).Size(pageSize).Sort(x => x.Ascending(SortSpecialField.DocumentIndexOrder)));
            olst.ModelList = (List<SolutionStructureCatalog>)output.Documents;
            return olst;
        }
    }
}
