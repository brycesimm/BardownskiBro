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

        public IActionResult TeamStats(string TeamName, string season = "", string seasonType = "")
        {
            if (TeamName.Length == 3)
            {
                try
                {
                    List<PlayerStats> skaterStats = new List<PlayerStats>();
                    List<PlayerStats> goalieStats = new List<PlayerStats>();
                    List<Player> forwardsList = new List<Player>();
                    List<Player> defensemenList = new List<Player>();
                    List<Player> goalieList = new List<Player>();
                    TeamStatsViewModel teamStatsViewModel = new TeamStatsViewModel();

                    if (season == "")
                    {
                        season = DateTime.Now.Year.ToString()+(DateTime.Now.Year+1).ToString();
                    }
                    if(seasonType == "")
                    {
                        seasonType = "2";
                    }
                    HttpClient httpClient = new HttpClient();

                    //Get Team Player Stats
                    string? response = httpClient.GetStringAsync(BaseURL + @$"club-stats/{TeamName}/{season}/{seasonType}").Result;

                    if(response is not null)
                    {
                        JObject? content = JSONHelper.StringToJObject(response);
                        if(content is not null)
                        {
                            //Get Skater Stats
                            string? skatersOnlyString = content["skaters"].ToString();
                            JArray? skaters = JArray.Parse(skatersOnlyString);

                            foreach(JObject skater in skaters)
                            {
                                PlayerStats skaterStat = new PlayerStats()
                                { 
                                    playerId = Int32.TryParse(skater.GetValue("playerId").ToString(), out int didItWork) == true ? didItWork : 0,
                                    headshot = skater.GetValue("headshot").ToString(),
                                    firstName = skater.GetValue("firstName")["default"].ToString(),
                                    lastName = skater.GetValue("lastName")["default"].ToString(),
                                    positionCode = skater.GetValue("positionCode").ToString(),
                                    gamesPlayed = skater.GetValue("gamesPlayed").ToString(),
                                    goals = skater.GetValue("goals").ToString(),
                                    assists = skater.GetValue("assists").ToString(),
                                    points = skater.GetValue("points").ToString(),
                                    plusMinus = skater.GetValue("plusMinus").ToString(),
                                    penaltyMinutes = skater.GetValue("penaltyMinutes").ToString(),
                                    powerPlayGoals = skater.GetValue("powerPlayGoals").ToString(),
                                    shorthandedGoals = skater.GetValue("shorthandedGoals").ToString(),
                                    gameWinningGoals = skater.GetValue("gameWinningGoals").ToString(),
                                    overtimeGoals = skater.GetValue("overtimeGoals").ToString(),
                                    shots = skater.GetValue("shots").ToString(),
                                    shootingPctg = skater.GetValue("shootingPctg").ToString(),
                                    avgTimeOnIcePerGame = skater.GetValue("avgTimeOnIcePerGame").ToString(),
                                    avgShiftsPerGame = skater.GetValue("avgShiftsPerGame").ToString(),
                                    faceoffWinPctg = skater.GetValue("faceoffWinPctg").ToString()
                                };
                                //assign forward, defenseman, or goalie based on position code since endpoint doesn't include it
                                switch(skaterStat.positionCode){
                                    case "L":
                                        skaterStat.position = "F";
                                        break;
                                    case "R":
                                        skaterStat.position = "F";
                                        break;
                                    case "C":
                                        skaterStat.position = "F";
                                        break;
                                    case "D":
                                        skaterStat.position = "D";
                                        break;
                                    case "G":
                                        skaterStat.position = "G";
                                        break;
                                    default:
                                        skaterStat.position = "UNK";
                                        break;
                                }
                                //Get skater's number since the first endpoint does not include it
                                string? skaterResponse = httpClient.GetStringAsync(BaseURL + $@"player/{skaterStat.playerId}/landing").Result;
                                if(skaterResponse is not null)
                                {
                                    JObject? skaterContent = JSONHelper.StringToJObject(skaterResponse);
                                    if(skaterContent is not null)
                                    {
                                        skaterStat.sweaterNumber = skaterContent.GetValue("sweaterNumber").ToString();
                                    }
                                }
                                else
                                {
                                    skaterStat.sweaterNumber = "UNK";
                                }
                                skaterStats.Add(skaterStat);
                            }

                            teamStatsViewModel.skaterStats = skaterStats;

                            //Get Goalie Stats
                            string? goaliesOnlyString = content["goalies"].ToString();
                            JArray? goalies = JArray.Parse(goaliesOnlyString);

                            foreach (JObject goalie in goalies)
                            {
                                PlayerStats goalieStat = new PlayerStats()
                                {
                                    playerId = Int32.TryParse(goalie.GetValue("playerId").ToString(), out int didItWork) == true ? didItWork : 0,
                                    headshot = goalie.GetValue("headshot").ToString(),
                                    firstName = goalie.GetValue("firstName")["default"].ToString(),
                                    lastName = goalie.GetValue("lastName")["default"].ToString(),
                                    gamesPlayed = goalie.GetValue("gamesPlayed").ToString(),
                                    gamesStarted = goalie.GetValue("gamesStarted").ToString(),
                                    wins = goalie.GetValue("wins").ToString(),
                                    losses = goalie.GetValue("losses").ToString(),
                                    ties = goalie.GetValue("ties").ToString(),
                                    overtimeLosses = goalie.GetValue("overtimeLosses").ToString(),
                                    goalsAgainstAverage = goalie.GetValue("goalsAgainstAverage").ToString(),
                                    savePercentage = goalie.GetValue("savePercentage").ToString(),
                                    shotsAgainst = goalie.GetValue("shotsAgainst").ToString(),
                                    saves = goalie.GetValue("saves").ToString(),
                                    goalsAgainst = goalie.GetValue("goalsAgainst").ToString(),
                                    shutouts = goalie.GetValue("shutouts").ToString(),
                                    goals = goalie.GetValue("goals").ToString(),
                                    assists = goalie.GetValue("assists").ToString(),
                                    points = goalie.GetValue("points").ToString(),
                                    penaltyMinutes = goalie.GetValue("penaltyMinutes").ToString(),
                                    timeOnIce = goalie.GetValue("timeOnIce").ToString()
                                };
                                //assign forward, defenseman, or goalie based on position code since endpoint doesn't include it
                                goalieStat.position = "G";
                                goalieStat.positionCode = "G";
                                //Get skater's number since the first endpoint does not include it
                                string? goalieResponse = httpClient.GetStringAsync(BaseURL + $@"player/{goalieStat.playerId}/landing").Result;
                                if (goalieResponse is not null)
                                {
                                    JObject? goalieContent = JSONHelper.StringToJObject(goalieResponse);
                                    if (goalieContent is not null)
                                    {
                                        goalieStat.sweaterNumber = goalieContent.GetValue("sweaterNumber").ToString();
                                    }
                                }
                                else
                                {
                                    goalieStat.sweaterNumber = "UNK";
                                }
                                goalieStats.Add(goalieStat);
                            }

                            teamStatsViewModel.goalieStats = goalieStats;
                        }
                    }

                    //Get Everyone on Roster
                    response = httpClient.GetStringAsync(BaseURL + @$"roster/{TeamName}/current").Result;
                    if (response is not null)
                    {
                        JObject? content = JSONHelper.StringToJObject(response);
                        if (content is not null)
                        {
                            string? forwardsOnlyString = content["forwards"].ToString();
                            JArray? forwards = JArray.Parse(forwardsOnlyString);

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

                            teamStatsViewModel.forwardsList = forwardsList;

                            return View(teamStatsViewModel);
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
