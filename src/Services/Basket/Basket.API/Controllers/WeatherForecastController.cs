using Basket.API.GrpcServices;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly StockItemGrpcService stockItemGrpcService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, StockItemGrpcService stockItemGrpcService)
        {
            _logger = logger;
            this.stockItemGrpcService = stockItemGrpcService;
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

        [HttpGet("Greeting")]
        public async Task<IActionResult> GetGreeting()
        {
            await stockItemGrpcService.GetStock();
            return Ok();
        }

    }
}