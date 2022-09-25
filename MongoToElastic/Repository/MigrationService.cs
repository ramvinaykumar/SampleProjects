using MongoToElastic.Helpers;
using MongoToElastic.Models;
using MongoToElastic.Models.Enums;
using System.Text;

namespace MongoToElastic.Repository
{
    public class MigrationService : IMigration
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
        public MigrationService(IElasticHelper elasticHelper, IMongoHelper mongoHelper)
        {
            _mongoHelper = mongoHelper;
            _elasticHelper = elasticHelper;
        }

        #region public
        #region code removed
        /// <summary>
        /// This method is used for compare with count only 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="dateRange"></param>
        /// <returns></returns>
        public string CompareESwithMongoCount(Enviornment env, DateType dateRange)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (DateType.All == dateRange)
            {
                stringBuilder.Append("Summary:\n");
                List<FilterData> mongoFilterData = new List<FilterData>();
                List<FilterData> elasticFilterData = new List<FilterData>();
                Parallel.Invoke(() =>
                {
                    mongoFilterData = _mongoHelper.GroupByMonthAndYear();
                }, () => { elasticFilterData = _elasticHelper.GroupByMonthAndYear(); });

                mongoFilterData.ForEach(x =>
                {
                    int count = 0;
                    FilterData filterDataModel = elasticFilterData.FirstOrDefault(y => y.Month == x.Month && y.Year == x.Year);
                    if (filterDataModel != null)
                    {
                        count = filterDataModel.Count;
                    }
                    stringBuilder.AppendLine(string.Format("\tMongo data Count: {0}, Elatic Search data Count:{1} in {2}/{3}(month/year)", x.Count, count, x.Month, x.Year));
                });
            }
            else
            {
                FilterData oFilterDataModel = new FilterData();
                int lastDays = 0;
                if (DateType.Yesterday == dateRange)
                {
                    lastDays = -1;
                }
                else if (DateType.Week == dateRange)
                {
                    lastDays = -7;
                }
                oFilterDataModel.StartDate = DateTime.UtcNow.Date.AddDays(lastDays);
                oFilterDataModel.EndDate = DateTime.UtcNow.Date;
                stringBuilder.Append(string.Format("Summary Range({0} to {1}):\n", oFilterDataModel.StartDate, oFilterDataModel.EndDate));
                List<FilterData> mongoFilterData = _mongoHelper.GroupByDate(oFilterDataModel);
                List<FilterData> elasticFilterData = _elasticHelper.GroupByDate(oFilterDataModel);
                mongoFilterData.ForEach(x =>
                {
                    int count = 0;
                    FilterData filterDataModel = elasticFilterData.FirstOrDefault(y => y.StartDate == x.StartDate);
                    if (filterDataModel != null)
                    {
                        count = filterDataModel.Count;
                    }
                    stringBuilder.AppendLine(string.Format("\tMongo data Count: {0}, Elatic Search data Count:{1} on {2}", x.Count, count, x.StartDate));
                });
            }
            return stringBuilder.ToString();

        }

        /// <summary>
        /// This method is use to bulk insert data 
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public string BulkInsertMongoToES(Enviornment env)
        {
            bool result = false;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Summary:\n");
            //Get total records from mongo and  get page count
            long totalCount = _mongoHelper.GetCount<SolutionStructureCatalog>();
            int totalPage = ExtensionMethods.totalPages(totalCount, ApplicationConstants.PAGESIZE);

            //Get all data from elastic to compare 
            var elasticOutputModel = _elasticHelper.ReadElasticData<SolutionStructureCatalog>();
            if (elasticOutputModel.ConnectionSuccessful)
            {
                //Use parallel for insert data 
                Parallel.ForEach(Enumerable.Range(0, totalPage), number =>
                {
                    //get paging data from mongo
                    List<SolutionStructureCatalog> mongoSSCModel = _mongoHelper.ReadSSCModelPagingData(number, ApplicationConstants.PAGESIZE);
                    //If no elastic data then insert whole data get from paging
                    if (elasticOutputModel.ModelList.Count == 0)
                    {
                        //Insert data into elastic from mongo
                        result = _elasticHelper.BulkInsertElasticData(mongoSSCModel, stringBuilder);
                    }
                    else
                    {
                        //get filtered data from mongo and elastic
                        var modifiedAndNewlyAddedRecord = ExtensionMethods.GetModifedRecords(mongoSSCModel, elasticOutputModel.ModelList);
                        // insert it into elastic
                        if (modifiedAndNewlyAddedRecord.Count > 0 && modifiedAndNewlyAddedRecord.Any(x => x.typeOfOperation == OperationType.add))
                        {
                            var lstSolutionStructureCatalogModel = modifiedAndNewlyAddedRecord.Where(x => x.typeOfOperation == OperationType.add).ToList().Select(x => x.NewSSCModel).ToList(); ;
                            result = _elasticHelper.BulkInsertElasticData(lstSolutionStructureCatalogModel.ToList(), stringBuilder);
                        }
                        else if (modifiedAndNewlyAddedRecord.Any(x => x.typeOfOperation == OperationType.modify))
                        {
                            stringBuilder.AppendLine(string.Format("{0} records are modified and need to update in elastic. click on InsertUpdateMongoToElasticForModifedRecords", modifiedAndNewlyAddedRecord.Count(x => x.typeOfOperation == OperationType.modify)));
                        }
                    }
                });
                if (!result)
                {
                    stringBuilder.AppendLine("No new Data Present in mongo");
                }
            }
            else
            {
                stringBuilder.AppendLine(string.Format("Elastic has some connection issue ErrorMsg:{0}", elasticOutputModel.ErrorMsg));
            }
            return stringBuilder.ToString();
        }

        public string BulkInsertESToMongo(Enviornment env)
        {
            bool result = false;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Summary:\n");
            //Get total records from mongo and  get page count
            List<SolutionStructureCatalog> mongoCollection = new List<SolutionStructureCatalog>();
            ElasticOutput<SolutionStructureCatalog> elasticOutputModel = new ElasticOutput<SolutionStructureCatalog>();
            Parallel.Invoke(() =>
            {
                elasticOutputModel = _elasticHelper.ReadElasticData<SolutionStructureCatalog>();
            }, () =>
            {
                mongoCollection = _mongoHelper.GetFilterData();
            });
            int totalPage = ExtensionMethods.totalPages(elasticOutputModel.ModelList.Count);
            List<SolutionStructureCatalog> olst = new List<SolutionStructureCatalog>();
            //Get all data from elastic to compare 
            var mongoOutputModel = _mongoHelper.ReadMongoData<SolutionStructureCatalog>();
            if (elasticOutputModel.ModelList.Count > 0 && mongoOutputModel != null)
            {
                //Use parallel for insert data 
                Parallel.ForEach(Enumerable.Range(0, totalPage), number =>
                {
                    List<SolutionStructureCatalog> elasticSSCModel = elasticOutputModel.ModelList.Skip(number * ApplicationConstants.PAGESIZE).Take(ApplicationConstants.PAGESIZE).ToList();
                    //get paging data from mongo
                    //If no elastic data then insert whole data get from paging
                    if (elasticOutputModel.ConnectionSuccessful && mongoOutputModel.Count == 0)
                    {
                        //Insert data into elastic from mongo
                        result = _mongoHelper.BulkInsertElasticData(elasticSSCModel, stringBuilder);
                    }
                    else
                    {
                        //get filtered data from mongo and elastic
                        var modifiedAndNewlyAddedRecord = ExtensionMethods.GetModifedRecords(elasticOutputModel.ModelList, mongoOutputModel);
                        // insert it into elastic
                        if (modifiedAndNewlyAddedRecord.Count > 0 && modifiedAndNewlyAddedRecord.Any(x => x.typeOfOperation == OperationType.add))
                        {
                            var lstSolutionStructureCatalogModel = modifiedAndNewlyAddedRecord.Where(x => x.typeOfOperation == OperationType.add).ToList().Select(x => x.NewSSCModel).ToList(); ;
                            result = _mongoHelper.BulkInsertElasticData(lstSolutionStructureCatalogModel.ToList(), stringBuilder);
                        }
                        else if (modifiedAndNewlyAddedRecord.Any(x => x.typeOfOperation == OperationType.modify))
                        {
                            stringBuilder.AppendLine(string.Format("{0} records are modified and need to update in elastic. click on InsertUpdateMongoToElasticForModifedRecords", modifiedAndNewlyAddedRecord.Count(x => x.typeOfOperation == OperationType.modify)));
                        }
                    }
                });
                if (!result)
                {
                    stringBuilder.AppendLine("No new Data Present in mongo");
                }
            }
            else
            {
                //stringBuilder.AppendLine(string.Format("Elastic has some connection issue ErrorMsg:{0}", mongoOutputModel.ErrorMsg));
            }

            return stringBuilder.ToString();
        }
        #endregion

        /// <summary>
        /// This method is used to remove index and bulk insert
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public string RemoveAndBulkUpdateAgainMongoToES(Data dataMap)
        {
            switch (dataMap)
            {
                case Data.Group:
                    _elasticHelper.RemoveIndex<Group>();
                    return GenericBulkInsertMongoToEs<Group>();
                case Data.OrderCodesToroleGroups:
                    _elasticHelper.RemoveIndex<OrderCodesToRoleGroups>();
                    return GenericBulkInsertMongoToEs<OrderCodesToRoleGroups>();
                case Data.RightGroup:
                    _elasticHelper.RemoveIndex<RightGroup>();
                    return GenericBulkInsertMongoToEs<RightGroup>();
                case Data.RoleGroup:
                    _elasticHelper.RemoveIndex<RoleGroup>();
                    return GenericBulkInsertMongoToEs<RoleGroup>();
                case Data.VariantToGroups:
                    _elasticHelper.RemoveIndex<VariantToGroups>();
                    return GenericBulkInsertMongoToEs<VariantToGroups>();
                case Data.GiiProductAccessAdminOrderCode:
                    _elasticHelper.RemoveIndex<GiiProductAccessAdminOrderCode>();
                    return GenericBulkInsertMongoToEs<GiiProductAccessAdminOrderCode>();
                case Data.CatalogSource:
                    _elasticHelper.RemoveIndex<CatalogSource>();
                    return GenericBulkInsertMongoToEs<CatalogSource>();
                case Data.SSCM:
                    _elasticHelper.RemoveIndex<SolutionStructureCatalog>();
                    return GenericBulkInsertMongoToEs<SolutionStructureCatalog>();
                default:
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("Summary(Group):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainMongoToES(Data.Group));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(RightGroup):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainMongoToES(Data.RightGroup));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(RoleGroup):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainMongoToES(Data.RoleGroup));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(VariantToGroups):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainMongoToES(Data.VariantToGroups));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(OrderCodesToroleGroups):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainMongoToES(Data.OrderCodesToroleGroups));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(GiiProductAccessAdminOrderCode):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainMongoToES(Data.GiiProductAccessAdminOrderCode));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(CatalogSource):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainMongoToES(Data.CatalogSource));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(SolutionStructureCatalog):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainMongoToES(Data.SSCM));
                    return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// Remove And Bulk Update Again ES To Mongo
        /// </summary>
        /// <param name="dataMap"></param>
        /// <returns></returns>
        public string RemoveAndBulkUpdateAgainESToMongo(Data dataMap)
        {
            switch (dataMap)
            {
                case Data.Group:
                    _mongoHelper.DropCollection<Group>();
                    return GenericBulkInsertESToMongo<Group>();
                case Data.OrderCodesToroleGroups:
                    _mongoHelper.DropCollection<OrderCodesToRoleGroups>();
                    return GenericBulkInsertESToMongo<OrderCodesToRoleGroups>();
                case Data.RightGroup:
                    _mongoHelper.DropCollection<RightGroup>();
                    return GenericBulkInsertESToMongo<RightGroup>();
                case Data.RoleGroup:
                    _mongoHelper.DropCollection<RoleGroup>();
                    return GenericBulkInsertESToMongo<RoleGroup>();
                case Data.VariantToGroups:
                    _mongoHelper.DropCollection<VariantToGroups>();
                    return GenericBulkInsertESToMongo<VariantToGroups>();
                case Data.GiiProductAccessAdminOrderCode:
                    _mongoHelper.DropCollection<GiiProductAccessAdminOrderCode>();
                    return GenericBulkInsertESToMongo<GiiProductAccessAdminOrderCode>();
                case Data.CatalogSource:
                    _mongoHelper.DropCollection<CatalogSource>();
                    return GenericBulkInsertESToMongo<CatalogSource>();
                case Data.SSCM:
                    _mongoHelper.DropCollection<SolutionStructureCatalog>();
                    return GenericBulkInsertESToMongo<SolutionStructureCatalog>();
                default:
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("Summary(Group):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainESToMongo(Data.Group));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(RightGroup):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainESToMongo(Data.RightGroup));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(RoleGroup):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainESToMongo(Data.RoleGroup));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(VariantToGroups):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainESToMongo(Data.VariantToGroups));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(OrderCodesToroleGroups):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainESToMongo(Data.OrderCodesToroleGroups));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(GiiProductAccessAdminOrderCode):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainESToMongo(Data.GiiProductAccessAdminOrderCode));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(CatalogSource):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainESToMongo(Data.CatalogSource));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(SolutionStructureCatalog):\n");
                    stringBuilder.Append(RemoveAndBulkUpdateAgainESToMongo(Data.SSCM));
                    return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// Get And Set Modified Data ES To Mongo
        /// </summary>
        /// <param name="dateRange"></param>
        /// <param name="insertIntoElastic"></param>
        /// <returns></returns>
        public string GetModifiedDataForSSCS(ExportFrom direction)
        {
            bool result = false;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Summary:\n");
            //Get total records from mongo and  get page count
            List<SolutionStructureCatalog> mongoCollection = new List<SolutionStructureCatalog>();
            ElasticOutput<SolutionStructureCatalog> elasticOutputModel = new ElasticOutput<SolutionStructureCatalog>();
            Parallel.Invoke(() =>
            {
                elasticOutputModel = _elasticHelper.ReadElasticData<SolutionStructureCatalog>();
            }, () =>
            {
                mongoCollection = _mongoHelper.GetFilterData();
            });

            List<ModifiedOrAddedSSCM> newModifedRecords = new List<ModifiedOrAddedSSCM>();
            if (direction == ExportFrom.ElasticToMongo)
            {
                newModifedRecords = ExtensionMethods.GetModifedRecords(elasticOutputModel.ModelList, mongoCollection);
            }
            else
            {
                newModifedRecords = ExtensionMethods.GetModifedRecords(mongoCollection, elasticOutputModel.ModelList);
            }
            if (newModifedRecords.Count > 0)
            {
                stringBuilder.AppendLine(string.Format("\t Added records: {0} Modified records: {1}  Deleted records: {2}",
                    newModifedRecords.Count(x => x.typeOfOperation == OperationType.add),
                    newModifedRecords.Count(x => x.typeOfOperation == OperationType.modify),
                    newModifedRecords.Count(x => x.typeOfOperation == OperationType.delete)));
            }
            else
            {
                stringBuilder.AppendLine("All data is matched");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Generic Compare Databases Mongo To ES
        /// </summary>
        /// <param name="dateRange"></param>
        /// <param name="dataMap"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public string GenericCompareDatabases(Data dataMap, ExportFrom direction)
        {
            switch (dataMap)
            {
                case Data.Group:
                    return GenericCompareData<Group>(ExpressionBuilder.GenerateKeyForGroups(), direction: direction);
                case Data.OrderCodesToroleGroups:
                    return GenericCompareData<OrderCodesToRoleGroups>(ExpressionBuilder.GenerateKeyForOrderCodesToRoleGroups(), ExpressionBuilder.CompareOrderCodesToRoleGroups(), direction: direction);
                case Data.RightGroup:
                    return GenericCompareData<RightGroup>(ExpressionBuilder.GenerateKeyForRightGroup(), ExpressionBuilder.CompareRightGroups(), direction: direction);
                case Data.RoleGroup:
                    return GenericCompareData<RoleGroup>(ExpressionBuilder.GenerateKeyForRoleGroup(), direction: direction);
                case Data.VariantToGroups:
                    return GenericCompareData<VariantToGroups>(ExpressionBuilder.GenerateKeyForVariantToGroups(), ExpressionBuilder.CompareVariantToGroups(), direction: direction);
                case Data.GiiProductAccessAdminOrderCode:
                    return GenericCompareData<GiiProductAccessAdminOrderCode>(ExpressionBuilder.GenerateKeyForGiiProductAccessAdminOrderCode(), direction: direction);
                case Data.CatalogSource:
                    return GenericCompareData<CatalogSource>(ExpressionBuilder.GenerateKeyForCatalogSource(), ExpressionBuilder.CompareCatalogSources(), direction: direction);
                case Data.SSCM:
                    return GetModifiedDataForSSCS(direction: direction);
                default:
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("Summary(Group):\n");
                    stringBuilder.Append(GenericCompareDatabases(Data.Group, direction));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(RightGroup):\n");
                    stringBuilder.Append(GenericCompareDatabases(Data.RightGroup, direction));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(RoleGroup):\n");
                    stringBuilder.Append(GenericCompareDatabases(Data.RoleGroup, direction));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(VariantToGroups):\n");
                    stringBuilder.Append(GenericCompareDatabases(Data.VariantToGroups, direction));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(OrderCodesToroleGroups):\n");
                    stringBuilder.Append(GenericCompareDatabases(Data.OrderCodesToroleGroups, direction));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(GiiProductAccessAdminOrderCode):\n");
                    stringBuilder.Append(GenericCompareDatabases(Data.GiiProductAccessAdminOrderCode, direction));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(CatalogSource):\n");
                    stringBuilder.Append(GenericCompareDatabases(Data.CatalogSource, direction));
                    stringBuilder.AppendLine("\n");
                    stringBuilder.Append("Summary(SolutionStructureCatalog):\n");
                    stringBuilder.Append(GenericCompareDatabases(Data.SSCM, direction));
                    return stringBuilder.ToString();
            }
            return "";
        }

        #endregion

        #region private method
        /// <summary>
        /// Insert Data Into Elastic
        /// </summary>
        /// <param name="newModifedRecords"></param>
        /// <param name="stringBuilder"></param>
        /// <param name="dateRange"></param>
        private void InsertDataIntoElastic(List<ModifiedOrAddedSSCM> newModifedRecords, StringBuilder stringBuilder, DateType dateRange)
        {
            var newAdded = newModifedRecords.Where(x => x.typeOfOperation == OperationType.add).ToList();
            var modifiedRecords = newModifedRecords.Where(x => x.typeOfOperation == OperationType.modify).ToList();
            var deleteRecords = newModifedRecords.Where(x => x.typeOfOperation == OperationType.delete).ToList();
            if (newAdded.Count > 0)
            {
                bool result = _elasticHelper.BulkInsertElasticData(newAdded.Select(x => x.NewSSCModel).ToList(), stringBuilder);
                newAdded.ForEach(x => x.Response = result);
                stringBuilder.AppendLine(string.Format("\t RecordsAdded: {0} | NotAdded: {1} of {2} data",
             newAdded.Count(x => x.Response == true),
             newAdded.Count(x => x.Response == false),
             Convert.ToString(dateRange)));
            }
            if (modifiedRecords.Count > 0)
            {
                _elasticHelper.UpdateRecords(modifiedRecords);
                stringBuilder.AppendLine(string.Format("\t Modified: {0} | NotModified: {1}  of {2} data",
            modifiedRecords.Count(x => x.Response == true),
            modifiedRecords.Count(x => x.Response == false),
            Convert.ToString(dateRange)));
            }
            if (deleteRecords.Count > 0)
            {
                _elasticHelper.DeleteRecords(deleteRecords);
                stringBuilder.AppendLine(string.Format("\t RecordsAdded: {0} Modified: {1} of {2} data",
                    deleteRecords.Count(x => x.Response == true),
                    deleteRecords.Count(x => x.Response == false),
                    Convert.ToString(dateRange)));
            }


        }
        private string GenericCompareData<T>(Func<T, string> generateKeyFunc, Func<T, T, bool> comparerFunc = null, ExportFrom direction = ExportFrom.MongoToElastic) where T : class
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<T> mongoCollection = new List<T>();
            ElasticOutput<T> elasticOutputModel = new ElasticOutput<T>();
            stringBuilder.Append("Summary:\n");
            Parallel.Invoke(() =>
            {
                elasticOutputModel = _elasticHelper.ReadElasticData<T>();
            }, () =>
            {
                mongoCollection = _mongoHelper.ReadMongoData<T>();
            });
            if (elasticOutputModel.ConnectionSuccessful && mongoCollection.Count > 0)
            {
                List<GenericModifiedOrAddedModel<T>> newModifedRecords = new List<GenericModifiedOrAddedModel<T>>();
                if (ExportFrom.MongoToElastic == direction)
                {
                    newModifedRecords = ExtensionMethods.GenericCompareData<T>(mongoCollection, elasticOutputModel.ModelList, generateKeyFunc, comparerFunc);
                }
                else
                {
                    newModifedRecords = ExtensionMethods.GenericCompareData<T>(elasticOutputModel.ModelList, mongoCollection, generateKeyFunc, comparerFunc);
                }
                if (newModifedRecords.Count > 0)
                {
                    stringBuilder.AppendLine(string.Format("\t Added records: {0} Modified records: {1}  Deleted records: {2}",
                        newModifedRecords.Count(x => x.typeOfOperation == OperationType.add),
                        newModifedRecords.Count(x => x.typeOfOperation == OperationType.modify),
                        newModifedRecords.Count(x => x.typeOfOperation == OperationType.delete)));
                }
                else
                {
                    stringBuilder.AppendLine("All data is matched");
                }
            }
            else if (!elasticOutputModel.ConnectionSuccessful)
            {
                stringBuilder.AppendLine(string.Format("Elastic client not working,Error:{0}", elasticOutputModel.ErrorMsg));
            }
            else if (mongoCollection.Count == 0)
            {
                stringBuilder.AppendLine("No mongo data found");
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// This method is use to Generic BulkInsert AfterRemove
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        private string GenericBulkInsertMongoToEs<T>() where T : class
        {
            bool result = false;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Summary:\n");
            //Get total records from mongo and  get page count
            List<T> MongoData = _mongoHelper.ReadMongoData<T>();
            int totalPage = ExtensionMethods.totalPages(MongoData.Count);
            //Use parallel for insert data 
            if (MongoData.Count > 0)
            {
                Parallel.ForEach(Enumerable.Range(0, totalPage), number =>
                {
                    //get paging data from mongo
                    List<T> mongoBatch = MongoData.Skip(number * ApplicationConstants.PAGESIZE).Take(ApplicationConstants.PAGESIZE).ToList();
                    result = _elasticHelper.GenericBulkInsertElasticData<T>(mongoBatch, stringBuilder);
                });
            }
            else
            {
                stringBuilder.AppendLine("No new Data Present in mongo");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Generic Bulk Insert ES To Mongo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string GenericBulkInsertESToMongo<T>() where T : class
        {
            bool result = false;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Summary:\n");
            //Get total records from mongo and  get page count
            List<T> mongoCollection = new List<T>();
            ElasticOutput<T> elasticOutputModel = _elasticHelper.ReadElasticData<T>();
            int totalPage = ExtensionMethods.totalPages(elasticOutputModel.ModelList.Count);
            //Use parallel for batch insert data 
            if (elasticOutputModel.ModelList.Count > 0)
            {
                Parallel.ForEach(Enumerable.Range(0, totalPage), number =>
                {
                    List<T> elasticSSCModel = elasticOutputModel.ModelList.Skip(number * ApplicationConstants.PAGESIZE).Take(ApplicationConstants.PAGESIZE).ToList();
                    result = _mongoHelper.GenericBulkInsertElasticData<T>(elasticSSCModel, stringBuilder);
                });
            }
            else
            {
                stringBuilder.AppendLine("No new Data Present in Elastic");
            }
            return stringBuilder.ToString();
        }
        #endregion
    }
}
