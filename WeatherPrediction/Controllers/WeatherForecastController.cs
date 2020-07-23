﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        [HttpPost]
        public IEnumerable<WeatherForecastModel> GetSome([FromBody]object body)
        {
            _logger.LogInformation("Getting weather information.");

            var searchTerm = JObject.Parse(body?.ToString())?["searchString"];

            _logger.LogInformation("Searchterm: " + searchTerm);

            var items = _repository.GetItems();

            _logger.LogWarning("Got " + items.Count() + " items from repository.");

            return items;
        }
    }
}
