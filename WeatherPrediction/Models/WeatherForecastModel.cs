using System;

namespace WeatherPrediction
{
    public class WeatherForecastModel
    {
        public DateTime Date { get; set; } = DateTime.Now;

        public int TemperatureC { get; set; } = 1;

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; } = "This is summary";
    }
}
