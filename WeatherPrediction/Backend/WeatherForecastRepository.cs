﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using WeatherPrediction.Models;

namespace WeatherPrediction.Backend
{
    public class WeatherForecastRepository : IRepository<WeatherForecastModel>
    {
        private const string FIVE_DAYS_PREDICTION_API_URI = @"https://community-open-weather-map.p.rapidapi.com/forecast?type=link%252C%20accurate&units=imperial%252C%20metric&q=";
        private const string CURRENT_WEATHER_DATA_API_URI = @"https://community-open-weather-map.p.rapidapi.com/weather?units=%2522metric%2522%20or%20%2522imperial%2522&q=";

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
                var fiveDaysResponse = ProcessHttpGet(searchString, FIVE_DAYS_PREDICTION_API_URI);
                var currentWeatherDataResponse = ProcessHttpGet(searchString, CURRENT_WEATHER_DATA_API_URI);

                if (fiveDaysResponse.IsSuccessStatusCode && currentWeatherDataResponse.IsSuccessStatusCode)
                {
                    var fiveDaysContent = fiveDaysResponse.Content.ReadAsStringAsync();
                    var currentWeatherDataContent = currentWeatherDataResponse.Content.ReadAsStringAsync();
                    return WeatherForecastModelBuilder.Build(fiveDaysContent.Result, currentWeatherDataContent.Result);
                }

                _logger.LogError("Statuscode from five days forecast api: " + fiveDaysResponse.StatusCode);
                _logger.LogError("Statuscode from current weather data api: " + currentWeatherDataResponse.StatusCode);
                return new List<WeatherForecastModel>();
            });

            return result;
        }

        private HttpResponseMessage ProcessHttpGet(string searchString, string uri)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", _configuration["X-RapidAPI-Host"]);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", _configuration["X-RapidAPI-Key"]);

                try
                {
                    var message = client.GetAsync(uri + searchString);
                    message.Wait();
                    return message.Result;
                }
                catch (AggregateException ae)
                {
                    _logger.LogError(ae.StackTrace);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                }
            }
        }
    }

}
