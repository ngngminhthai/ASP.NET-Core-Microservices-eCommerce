using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TestApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        /*public async Task<IActionResult> OnGet()
        {
            using var client = new HttpClient();

            // var token = await _tokenService.GetToken("weatherapi.read");
            var token = await HttpContext.GetTokenAsync("access_token");
            client.SetBearerToken(token);

            var result = await client.GetAsync("https://localhost:5445/weatherforecast");

            if (result.IsSuccessStatusCode)
            {
                var model = await result.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<List<WeatherData>>(model);

                return View(data);
            }

            throw new Exception("Unable to get content");
        }*/
    }
}
