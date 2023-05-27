using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        /*        private readonly GeeterGrpcService _geeterGrpcService;
        */
        /*  public WeatherForecastController(ILogger<WeatherForecastController> logger, GeeterGrpcService geeterGrpcService)
          {
              _logger = logger;
              _geeterGrpcService = geeterGrpcService;
          }*/

        //if use dont run grpc service, then use this constructor
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /*   [HttpGet("Greeting")]
           public async Task<IActionResult> GetGreeting()
           {
               string name = "John Doe";
               string greeting = await _geeterGrpcService.GetGreetingAsync(name);
               return Ok(greeting);
           }*/

    }
}