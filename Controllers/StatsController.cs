using BardownskiBro.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using BardownskiBro.HelperClasses;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using System;

namespace BardownskiBro.Controllers
{
    public class StatsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _configuration;
        protected string? BaseURL;
        protected string? BaseURL2;

        public StatsController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            BaseURL = _configuration is not null ? _configuration.GetValue<string>("NHLAPI:BaseURL") : "";
            BaseURL2 = _configuration is not null ? _configuration.GetValue<string>("NHLAPI:BaseURL2") : "";
        }

        public IActionResult Index()
        {
            ViewBag.NHLAPIURL = _configuration.GetValue<string>("NHLAPI:BaseURL");
            return View();
        }

        public IActionResult TeamStats(string TeamName)
        {
            if (TeamName.Length == 3)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    string? response = httpClient.GetStringAsync(BaseURL + @$"roster/{TeamName}/current").Result;
                    if (response is not null)
                    {
                        JObject? content = JSONHelper.StringToJObject(response);
                        if (content is not null)
                        {
                            //PositionsHelper positions = (PositionsHelper)JsonConvert.DeserializeObject(response, typeof(PositionsHelper));
                            string? forwardsOnlyString = content["forwards"].ToString();
                            JArray? forwards = JArray.Parse(forwardsOnlyString);
                            //Player positions = (Player)JsonConvert.DeserializeObject(response, typeof(PositionsHelper));
                            //JArray? forwards = JArray.Parse(content["forwards"].ToString());
                            //List<Player> forwardsList = forwards.ToObject<List<Player>>();
                            List<Player> forwardsList = new List<Player>();
                            //forwards.ToObject<Player>();
                            //foreach (JToken forward in forwards)
                            //{
                            //    //(Player)JsonConvert.DeserializeObject
                            //}
                            foreach (JObject forward in forwards)
                            {

                                Player player = new Player()
                                {
                                    id = Int32.TryParse(forward.GetValue("id").ToString(), out int didItWork) == true ? didItWork : 0,
                                    headshot = forward.GetValue("headshot").ToString(),
                                    firstName = forward.GetValue("firstName")["default"].ToString(),
                                    lastName = forward.GetValue("lastName")["default"].ToString(),
                                    sweaterNumber = forward.GetValue("sweaterNumber").ToString(),
                                    position = "F",
                                    positionCode = forward.GetValue("positionCode").ToString(),
                                    shootsCatches = forward.GetValue("shootsCatches").ToString(),
                                    heightInInches = forward.GetValue("heightInInches").ToString(),
                                    weightInPounds = forward.GetValue("weightInPounds").ToString(),
                                    birthDate = forward.GetValue("birthDate").ToString(),
                                    birthCity = forward.GetValue("birthCity")["default"].ToString(),
                                    birthStateProvince = forward.GetValue("birthStateProvince") is not null ? forward.GetValue("birthStateProvince")["default"].ToString() : "",
                                    birthCountry = forward.GetValue("birthCountry").ToString()
                                };
                                player.age = ConvertBDayToAge.ConvertDateStringToAge(player.birthDate);
                                forwardsList.Add(player);
                            }
                            return View(forwardsList);
                        }

                    }
                    else
                    {
                        return BadRequest("NHL API Returned A Null Response Result");
                    }
                }catch (Exception ex)
                {
                    if (_configuration.GetValue<string>("Environment") == "DEV")
                    {
                        return BadRequest(ex.Message + "\n" + ex.StackTrace + "\n" + ex.Source);
                    }
                    else
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            else
            {
                return BadRequest("Invalid Team Name");
            }
            return View(new List<Player>());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
