using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using WeatherPrediction.Models;
using WeatherPrediction.Utilities;

namespace WeatherPrediction.Backend
{
    public static class WeatherForecastModelBuilder 
    {
        private static readonly decimal _convertFromKelvin = -273.15m;

        public static List<WeatherForecastModel> Build(string fiveDaysPredictionDataJson, string currentWeatherData)
        {
            var allModels = new List<WeatherForecastModel>();

            if (string.IsNullOrWhiteSpace(fiveDaysPredictionDataJson) || 
                string.IsNullOrWhiteSpace(currentWeatherData))
            {
                return allModels;
            }

            var currentDataJsonObject = JsonConvert.DeserializeObject<dynamic>(currentWeatherData.ToString());

            try
            {
                foreach (var item in JArray.Parse(JObject.Parse(fiveDaysPredictionDataJson)?["list"].ToString()))
                {
                    allModels.Add(ProcessEachWeatherForecastModel(currentDataJsonObject, item));
                }
            } catch (JsonReaderException)
            {
                
            }
         
            return allModels;
        }

        private static WeatherForecastModel ProcessEachWeatherForecastModel(dynamic currentDataJsonObject, JToken item)
        {
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(item.ToString());

            var newModel = new WeatherForecastModel()
            {
                ID = currentDataJsonObject.id,
                Temperature = jsonObject.main.temp + _convertFromKelvin,
                MinimumTemperature = ((decimal)jsonObject.main.temp_min) + _convertFromKelvin,
                MaximumTemperature = ((decimal)jsonObject.main.temp_max) + _convertFromKelvin,
                Country = currentDataJsonObject.sys.country,
                Date = ((string)jsonObject.dt).TryConvertToDateTimeOffset(),
                City = currentDataJsonObject.name,
                Sunrise = ((string)currentDataJsonObject.sys.sunrise).TryConvertToDateTimeOffset(),
                Sunset = ((string)currentDataJsonObject.sys.sunset).TryConvertToDateTimeOffset()
            };
            return newModel;
        }
    }
}
