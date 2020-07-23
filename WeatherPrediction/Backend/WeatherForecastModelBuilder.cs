using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherPrediction.Backend
{
    public static class WeatherForecastModelBuilder 
    {
        private static readonly decimal _convertFromKelvin = -273.15m;

        public static List<WeatherForecastModel> Build(string json)
        {
            var allModels = new List<WeatherForecastModel>();

            foreach (var item in JArray.Parse(JObject.Parse(json)?.ToString()))
            {
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(item.ToString());

                var newModel = new WeatherForecastModel()
                {
                    Temperature = jsonObject.main.temp + _convertFromKelvin,
                    MinimumTemperature = ((decimal)jsonObject.main.temp_min) + _convertFromKelvin,
                    MaximumTemperature = ((decimal)jsonObject.main.temp_max) + _convertFromKelvin,
                    Sunrise = ((string)jsonObject.sys.sunrise).TryConvertToDateTimeOffset(),
                    Sunset = ((string)jsonObject.sys.sunset).TryConvertToDateTimeOffset(),
                    Country = jsonObject.sys.country,
                    Date = ((string)jsonObject.dt).TryConvertToDateTimeOffset(),
                    City = jsonObject.name
                };

                allModels.Add(newModel);
            }
            return allModels;
        }

    }
}
