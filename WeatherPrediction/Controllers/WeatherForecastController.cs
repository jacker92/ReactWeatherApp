using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WeatherPrediction.Backend;

namespace WeatherPrediction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRepository<WeatherForecastModel> _repository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepository<WeatherForecastModel> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecastModel> Get()
        {
            _logger.LogWarning("Getting weather information.");

            var items = _repository.GetItems();

            _logger.LogWarning("MinTemp is " + items.First().MinimumTemperature);
            _logger.LogWarning("MaxTemp is " + items.First().MaximumTemperature);
            return items;
        }
    }
}
