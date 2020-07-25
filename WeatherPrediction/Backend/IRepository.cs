using System.Collections.Generic;

namespace WeatherPrediction.Backend
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetWeatherData(string searchString);
    }
}
