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

        public static IEnumerable<WeatherForecastModel> Build(string fiveDaysPredictionDataJson)
        {
            var allModels = new List<WeatherForecastModel>();

            if (string.IsNullOrWhiteSpace(fiveDaysPredictionDataJson))
            {
                return allModels;
            }

            var fiveDaysTest = JObject.Parse(fiveDaysPredictionDataJson);

            try
            {
                foreach (var item in JArray.Parse(fiveDaysTest?["list"].ToString()))
                {
                    allModels.Add(ProcessEachWeatherForecastModel(fiveDaysTest, item));
                }
            } catch (JsonReaderException)
            {
                
            }
         
            return allModels;
        }

        private static WeatherForecastModel ProcessEachWeatherForecastModel(JToken currentDataJsonObject, JToken jsonObject)
        {
            return new WeatherForecastModel()
            {
                ID = currentDataJsonObject["city"]["id"].ToString(),
                Temperature = decimal.Parse(jsonObject["main"]["temp"].ToString()) + _convertFromKelvin,
                MinimumTemperature = decimal.Parse(jsonObject["main"]["temp_min"].ToString()) + _convertFromKelvin,
                MaximumTemperature = decimal.Parse(jsonObject["main"]["temp_max"].ToString()) + _convertFromKelvin,
                Country = currentDataJsonObject["city"]["country"].ToString(),
                Date = jsonObject["dt"].ToString().TryConvertToDateTimeOffset(),
                City = currentDataJsonObject["city"]["name"].ToString(),
                Sunrise = (currentDataJsonObject["city"]["sunrise"].ToString()).TryConvertToDateTimeOffset(),
                Sunset = (currentDataJsonObject["city"]["sunset"].ToString()).TryConvertToDateTimeOffset()
            };
        }
    }
}
