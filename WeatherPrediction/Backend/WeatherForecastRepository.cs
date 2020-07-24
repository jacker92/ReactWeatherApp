using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherPrediction;
using WeatherPrediction.Models;

namespace WeatherPrediction.Backend
{
    public class WeatherForecastRepository : IRepository<WeatherForecastModel>
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache<WeatherForecastModel> _weatherForecastModelCache;

        public WeatherForecastRepository(IConfiguration configuration, IMemoryCache<WeatherForecastModel> weatherForecastModelCache)
        {
            _configuration = configuration;
            _weatherForecastModelCache = weatherForecastModelCache;
        }
        public IEnumerable<WeatherForecastModel> GetItems(string searchString)
        {
            var result = _weatherForecastModelCache.GetOrCreate(searchString, () =>
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", _configuration["X-RapidAPI-Host"]);
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", _configuration["X-RapidAPI-Key"]);
                    var jsonResponse = client.GetStringAsync(@"https://community-open-weather-map.p.rapidapi.com/find?type=link%252C%20accurate&units=imperial%252C%20metric&q=" + searchString).Result;

                   return WeatherForecastModelBuilder.Build(jsonResponse);
                }
            });

            return result;
        }
    }
}
