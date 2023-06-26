using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestMVCSSO.Models;

namespace TestMVCSSO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            using var client = new HttpClient();

            // var token = await _tokenService.GetToken("weatherapi.read");
            var token = await HttpContext.GetTokenAsync("access_token");
            client.SetBearerToken(token);

            var result = await client.GetAsync("https://localhost:5445/weatherforecast");

            if (result.IsSuccessStatusCode)
            {
                /* var model = await result.Content.ReadAsStringAsync();

                 var data = JsonConvert.DeserializeObject<List<WeatherData>>(model);

                 return View(data);*/
                return View();

            }

            throw new Exception("Unable to get content");
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