using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherPrediction;

namespace WeatherPrediction.Backend
{
    public class WeatherForecastRepository : IRepository<WeatherForecastModel>
    {
        private readonly IConfiguration _configuration;

        public WeatherForecastRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<WeatherForecastModel> GetItems(string searchString)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", _configuration["X-RapidAPI-Host"]);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", _configuration["X-RapidAPI-Key"]);
                var jsonResponse = client.GetStringAsync(@"https://community-open-weather-map.p.rapidapi.com/find&q=" + searchString).Result;

                return WeatherForecastModelBuilder.Build(jsonResponse);
            }
        }
    }
}
