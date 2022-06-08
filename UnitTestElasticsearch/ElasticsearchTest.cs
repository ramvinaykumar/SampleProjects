using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTestElasticsearch
{
    [TestClass]
    public class ElasticsearchTest
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetResultTest()
        {
            ElasticSearch objSearch = new ElasticSearch();
            var result = objSearch.GetResult();

            Assert.IsTrue(result.FirstOrDefault<Model.City>(x => x.Name == "Delhi") != null);
            Assert.IsTrue(result.FirstOrDefault<Model.City>(x => x.Name == "Mumbai") != null);
            Assert.IsTrue(result.FirstOrDefault<Model.City>(x => x.Name == "Chenai") != null);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddNewIndexTest()
        {
            ElasticSearch objSearch = new ElasticSearch();

            objSearch.AddNewIndex(new Model.City(1, "Delhi", "Delhi", "India", "9.879 million"));
            objSearch.AddNewIndex(new Model.City(2, "Mumbai", "Maharashtra", "India", "11.98 million"));
            objSearch.AddNewIndex(new Model.City(3, "Chenai", "Tamil Nadu", "India", "4.334 million"));
            objSearch.AddNewIndex(new Model.City(4, "Kolkata", "W. Bengal", "India", "4.573 million"));
            objSearch.AddNewIndex(new Model.City(4, "Banglore", "Karnataka", "India", "4.302 million"));
            objSearch.AddNewIndex(new Model.City(4, "Pune", "Maharashtra", "India", "2.538 million"));
        }
    }
}
