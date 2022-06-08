using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Web.Vaccine.Alert.Models;

namespace Web.Vaccine.Alert.Controllers
{
    /// <summary>
    /// API Controller Home
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Read only variable of ILogger
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="logger">ILogger logger</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Privacy
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Get the available slots
        /// </summary>
        /// <param name="Pincode">string pincode</param>
        /// <param name="Date">string date</param>
        /// <param name="day">int day</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSlotAval(string Pincode, string Date, int day)
        {
            try
            {
                var counter = 0;
                List<SessionByPin> sessions = new List<SessionByPin>();

                if (!string.IsNullOrWhiteSpace(Pincode))
                {
                    var pincodes = new List<string>();
                    pincodes = Pincode.Split(',').ToList();

                    foreach (var pin in pincodes)
                    {
                        if (!string.IsNullOrWhiteSpace(Date))
                        {
                            var date = Convert.ToDateTime(Date);
                            if (day > 0)
                            {
                                for (int i = 0; i < day; i++)
                                {
                                    date = date.AddDays(i);

                                    var API_URL = string.Format("https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/findByPin?pincode={0}&date={1}", pin, date.ToString("dd-MM-yyyy"));
                                    // string API_URL = $"https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/findByPin?pincode={item}&date={date.ToString("dd-MM-yyyy")}";
                                    var client = new RestClient(API_URL);
                                    var request = new RestRequest(Method.GET);
                                    request.AddHeader("content-type", "application/json");
                                    var queryResult = client.Execute<CoWin_SessionByPin>(request).Data;
                                    if (queryResult != null && queryResult.Sessions != null && queryResult.Sessions.Count > 0)
                                    {
                                        bool IsAnySlotsAvailable = queryResult.Sessions.Where(s => s.AvailableCapacity > 0 && (s.AvailableCapacityDose1 > 0 || s.AvailableCapacityDose2 > 0) && s.MinAgeLimit < 45).Count() > 0 ? true : false;
                                        if (IsAnySlotsAvailable)
                                        {
                                            counter++;
                                            sessions.AddRange(queryResult.Sessions);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (counter > 0)
                    {
                        return Json(new { IsError = false, Slots = sessions, IsAnySlotsAvailable = true });
                    }
                    else
                    {
                        return Json(new { IsError = true });
                    }
                }
                else
                {
                    return Json(new { IsError = true });
                }
            }
            catch (Exception)
            {
                return Json(new { IsError = true });
            }
        }
    }
}
