using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using test_fullStack.Models;

namespace test_fullStack.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var dataToProcess = "~IN*TN>CHENNAI>MADURAI>KOVAI>ERODE*AP>ONGOLE>TENALI>VIZAG*TS>HYDERABAD>WARANGAL>VIKARABAD~USA*ALASKA>JUNEAU>SITKA>KENAI~CHINA*HAINAN>HAIKOU>SANYA>DONGFANG*HUNAN>CHANGHSA>YUEYANG>CHANGDE";

            var result = new Dictionary<string, Dictionary<string, List<string>>>();
            var sections = dataToProcess.Split('~');

            foreach (var section in sections)
            {
                if(section != "")
                {
                    var parts = section.Split('*').ToList();
                    var country = parts[0];
                    parts.RemoveAt(0);

                    var states_data = new Dictionary<string, List<string>>();
                    foreach (var item in parts)
                    {
                        var statesAndCities_general = item.Split('>').ToList();
                        var state_general = statesAndCities_general[0];

                        statesAndCities_general.RemoveAt(0);

                        var cities_general = statesAndCities_general;
                        states_data[state_general] = statesAndCities_general;

                    }

                    result[country] = states_data;
                }

            }


            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}