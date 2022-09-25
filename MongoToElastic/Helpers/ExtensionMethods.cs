using MongoToElastic.Models;
using MongoToElastic.Models.Enums;

namespace MongoToElastic.Helpers
{
    public static class ExtensionMethods
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Get Modifed Records
        /// </summary>
        /// <param name="primaryDatabase"></param>
        /// <param name="secondaryDatabase"></param>
        /// <returns></returns>
        public static List<ModifiedOrAddedSSCM> GetModifedRecords(List<SolutionStructureCatalog> primaryDatabase, List<SolutionStructureCatalog> secondaryDatabase)
        {
            //elastic data convert into dictionary based on createddate,SolutionStructureId,CustomerContext.CountryCode,CustomerSetId,LanguageCode,Region
            //* there should be no duplicacy 
            //if mongo data found not found in elastic then we need to add it else 
            //else if found check mondified date is diffrent or not if yes then modifed it


            List<ModifiedOrAddedSSCM> modifiedOrAddedSSCM = new List<ModifiedOrAddedSSCM>();

            Dictionary<string, SolutionStructureCatalog> secondaryData = new Dictionary<string, SolutionStructureCatalog>();
            Dictionary<string, SolutionStructureCatalog> primaryData = new Dictionary<string, SolutionStructureCatalog>();
            var generateKey = ExpressionBuilder.GenerateKeyForSolutionStructureCatalogModel();
            secondaryDatabase.ForEach(x =>
            {
                try
                {
                    secondaryData.Add(generateKey(x), x);
                }
                catch (Exception)
                {
                    //CustomLogger(string.Format("Duplicate data found :{0}", Newtonsoft.Json.JsonConvert.SerializeObject(x))).Wait();
                }
            });
            primaryDatabase.ForEach(x =>
            {
                try
                {
                    primaryData.Add(generateKey(x), x);
                }
                catch (Exception)
                {
                    //CustomLogger(string.Format("Duplicate data found :{0}", Newtonsoft.Json.JsonConvert.SerializeObject(x))).Wait();
                }
                SolutionStructureCatalog solutionStructureCatalogModel = null;
                secondaryData.TryGetValue(generateKey(x), out solutionStructureCatalogModel);
                if (solutionStructureCatalogModel != null && solutionStructureCatalogModel.CreatedDate == x.CreatedDate && solutionStructureCatalogModel.ModifiedDate != x.ModifiedDate)
                {
                    modifiedOrAddedSSCM.Add(new ModifiedOrAddedSSCM()
                    {
                        typeOfOperation = OperationType.modify,
                        NewSSCModel = x,
                        OldSSCModel = solutionStructureCatalogModel
                    });
                }
                else if (solutionStructureCatalogModel == null)
                {
                    try
                    {
                        secondaryData.Add(generateKey(x), x);
                    }
                    catch (Exception)
                    {
                    }
                    modifiedOrAddedSSCM.Add(new ModifiedOrAddedSSCM()
                    {
                        typeOfOperation = OperationType.add,
                        NewSSCModel = x
                    });
                }
            });

            //secondary database count is greater then primary
            if (primaryDatabase.Count < secondaryData.Count)
            {
                int totalDiffrence = secondaryData.Count - primaryDatabase.Count;
                foreach (var item in secondaryData.Where(x => totalDiffrence > 0))
                {
                    if (!primaryData.ContainsKey(item.Key))
                    {
                        modifiedOrAddedSSCM.Add(new ModifiedOrAddedSSCM()
                        {
                            typeOfOperation = OperationType.delete,
                            OldSSCModel = item.Value
                        });
                        totalDiffrence--;
                    }
                }
            }
            return modifiedOrAddedSSCM;
        }

        /// <summary>
        /// Get Modifed Records
        /// </summary>
        /// <param name="primaryDatabase"></param>
        /// <param name="secondaryDatabase"></param>
        /// <returns></returns>
        public static List<GenericModifiedOrAddedModel<T>> GenericCompareData<T>(List<T> primaryDatabase,
            List<T> secondaryDatabase,
            Func<T, string> generateKeyFunc, Func<T, T, bool> comparerFunc = null)
        {
            List<GenericModifiedOrAddedModel<T>> modifiedOrAddedSSCM = new List<GenericModifiedOrAddedModel<T>>();
            Dictionary<string, T> secondaryDict = new Dictionary<string, T>();
            Dictionary<string, T> primaryDict = new Dictionary<string, T>();
            secondaryDatabase.ForEach(x =>
            {
                try
                {
                    secondaryDict.Add(generateKeyFunc(x), x);
                }
                catch (Exception)
                {
                    //CustomLogger(string.Format("Duplicate data found :{0}", Newtonsoft.Json.JsonConvert.SerializeObject(x))).Wait();
                }
            });
            primaryDatabase.ForEach(primaryData =>
            {
                try
                {
                    primaryDict.Add(generateKeyFunc(primaryData), primaryData);
                }
                catch (Exception)
                {
                    //CustomLogger(string.Format("Duplicate data found :{0}", Newtonsoft.Json.JsonConvert.SerializeObject(x))).Wait();
                }
                T secondaryData = default(T);
                secondaryDict.TryGetValue(generateKeyFunc(primaryData), out secondaryData);
                if (secondaryData != null && comparerFunc != null)
                {
                    if (!comparerFunc(primaryData, secondaryData))
                    {
                        modifiedOrAddedSSCM.Add(new GenericModifiedOrAddedModel<T>
                        {
                            typeOfOperation = OperationType.modify,
                            NewSSCModel = primaryData,
                            OldSSCModel = secondaryData
                        });
                    }
                }
                else if (secondaryData == null)
                {
                    try
                    {
                        secondaryDict.Add(generateKeyFunc(primaryData), primaryData);
                    }
                    catch (Exception)
                    {
                    }
                    modifiedOrAddedSSCM.Add(new GenericModifiedOrAddedModel<T>()
                    {
                        typeOfOperation = OperationType.add,
                        NewSSCModel = primaryData
                    });
                }
            });

            //secondary database count is greater then primary
            if (primaryDatabase.Count < secondaryDict.Count)
            {
                int totalDiffrence = secondaryDict.Count - primaryDatabase.Count;
                foreach (var item in secondaryDict.Where(x => totalDiffrence > 0))
                {
                    if (!primaryDict.ContainsKey(item.Key))
                    {
                        modifiedOrAddedSSCM.Add(new GenericModifiedOrAddedModel<T>
                        {
                            typeOfOperation = OperationType.delete,
                            OldSSCModel = item.Value
                        });
                        totalDiffrence--;
                    }
                }
            }
            return modifiedOrAddedSSCM;
        }

        public static int totalPages(long totalSize, int pageSize = ApplicationConstants.PAGESIZE) => Convert.ToInt32((totalSize + pageSize - 1) / pageSize);

        #region for local machine testing

        public async static System.Threading.Tasks.Task CustomLogger(string error)
        {
            using (System.IO.StreamWriter file = new StreamWriter(@"C:\customlog\mongotoes_log\mongotoes.txt", append: true))
            {
                await file.WriteLineAsync(string.Format("{0}, msg:{1}", DateTime.Now, error));
            }
        }

        #endregion
    }
}
