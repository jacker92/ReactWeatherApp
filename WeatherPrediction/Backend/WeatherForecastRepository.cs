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

        public WeatherForecastModel GetItem(string ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WeatherForecastModel> GetItems()
        {
            var items = new List<WeatherForecastModel>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", _configuration["X-RapidAPI-Host"]);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", _configuration["X-RapidAPI-Key"]);
                var jsonResponse = client.GetStringAsync(@"https://community-open-weather-map.p.rapidapi.com/weather?id=2172797&units=%2522metric%2522+or+%2522imperial%2522&mode=xml%252C+ht&q=Helsinki").Result;

                var model = WeatherForecastModelBuilder.Build(jsonResponse);

                items.Add(model);
            }

            return items;
        }
    }
}
