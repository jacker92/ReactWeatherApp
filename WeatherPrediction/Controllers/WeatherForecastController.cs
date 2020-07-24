using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherPrediction.Backend;
using WeatherPrediction.Models;
using WeatherPrediction.Utilities;

namespace WeatherPrediction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRepository<WeatherForecastModel> _repository;
        private readonly IRequestBodyParser _requestBodyParser;
        private readonly ISearchTermValidator _searchTermValidator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
                                        IRepository<WeatherForecastModel> repository,
                                        IRequestBodyParser requestBodyParser,
                                        ISearchTermValidator searchTermValidator)
        {
            _logger = logger;
            _repository = repository;
            _requestBodyParser = requestBodyParser;
            _searchTermValidator = searchTermValidator;
        }

        [HttpPost]
        public IEnumerable<WeatherForecastModel> Get([FromBody]object body)
        {
            var searchTerm = _requestBodyParser.Parse(body);

            if(!_searchTermValidator.IsValid(searchTerm))
            {
                return new List<WeatherForecastModel>();
            }

            _logger.LogInformation($"Getting weather information with searchterm: {searchTerm}.");

            var items = _repository.GetWeatherData(searchTerm);

            _logger.LogWarning("Got " + items.Count() + " items from repository.");

            return items;
        }
    }
}
