using ElasticSearch.Model;
using ElasticSearch.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Threading.Tasks;

namespace ElasticSearch.MVC.Controllers
{
    /// <summary>
    /// API Control for user
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// Registration
        /// </summary>
        /// <returns></returns>
        public ActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Post user registration
        /// </summary>
        /// <param name="user">UserEN user</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Registration(UserEN user)
        {
            ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200"));

            settings.DefaultIndex("employeestore");
            ElasticClient esClient = new ElasticClient(settings);
            Employee emp = new Employee { EmployeeID = user.ID, EmployeeName = user.Name, Address = user.Address };

            //var response = await esClient.IndexAsync<Employee>(emp, i => i
            //                                  .Index("employeestore")

            //                                  .Type(Type.FilterName<Employee>())
            //                                  .Id(user.ID)
            //                                  .Refresh(Elasticsearch.Net.Refresh.True));

            return await Task.Run(() => RedirectToAction("GetUsers"));
        }
    }
}
