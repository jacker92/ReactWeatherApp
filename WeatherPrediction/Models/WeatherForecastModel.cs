using System;

namespace WeatherPrediction.Models
{
    public class WeatherForecastModel
    {
        public DateTimeOffset Date { get; set; } = DateTimeOffset.MinValue;

        public decimal Temperature { get; set; } = 1;

        public decimal MinimumTemperature { get; set; } = 1;

        public decimal MaximumTemperature { get; set; } = 1;
        public DateTimeOffset Sunrise { get; set; }
        public DateTimeOffset Sunset { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public string FormattedDate => Date.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss");

        public string FormattedSunrise => Sunrise.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss");

        public string FormattedSunset => Sunset.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss");

        public string ID { get; set; }
    }
}
