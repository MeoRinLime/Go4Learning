//using back.Context;
//using back.Service;
//using Microsoft.AspNetCore.Mvc;

//namespace back.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class WeatherForecastController : ControllerBase
//    {
//        private readonly ContentSectionContext contentSectionContext;
//        private static readonly string[] Summaries = new[]
//        {
//        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//    };

//        private readonly ILogger<WeatherForecastController> _logger;

//        public WeatherForecastController(ContentSectionContext contentSectionContext, ILogger<WeatherForecastController> logger)
//        {
//            this.contentSectionContext = contentSectionContext;
//            _logger = logger;
//        }

//        [HttpGet(Name = "GetWeatherForecast")]
//        public IEnumerable<WeatherForecast> Get()
//        {
//            TestService t=new TestService(contentSectionContext);
//            t.test();
//            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//            {
//                Date = DateTime.Now.AddDays(index),
//                TemperatureC = Random.Shared.Next(-20, 55),
//                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//            })
//            .ToArray();
//        }
//    }
//}