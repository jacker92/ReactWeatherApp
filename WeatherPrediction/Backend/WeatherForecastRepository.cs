using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using WeatherPrediction.Models;

namespace WeatherPrediction.Backend
{
    public class WeatherForecastRepository : IRepository<WeatherForecastModel>
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache<WeatherForecastModel> _weatherForecastModelCache;
        private readonly ILogger<WeatherForecastRepository> _logger;

        public WeatherForecastRepository(IConfiguration configuration, IMemoryCache<WeatherForecastModel> weatherForecastModelCache, ILogger<WeatherForecastRepository> logger)
        {
            _configuration = configuration;
            _weatherForecastModelCache = weatherForecastModelCache;
            _logger = logger;
        }
        public IEnumerable<WeatherForecastModel> GetWeatherData(string searchString)
        {
            var result = _weatherForecastModelCache.GetOrCreate(searchString, () =>
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", _configuration["X-RapidAPI-Host"]);
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", _configuration["X-RapidAPI-Key"]);

                    var message = client.GetAsync(@"https://community-open-weather-map.p.rapidapi.com/forecast?type=link%252C%20accurate&units=imperial%252C%20metric&q=" + searchString);
                    message.Wait();

                    using (var result = message.Result)
                    {
                        if (result.IsSuccessStatusCode)
                        {
                            var content = result.Content.ReadAsStringAsync();
                            return WeatherForecastModelBuilder.Build(content.Result);
                        }

                        _logger.LogError("Error: " + result.StatusCode);
                        return new List<WeatherForecastModel>();
                    }
                }
            });

            return result;
        }
    }
}
