using System.Collections.Generic;

namespace WeatherApp.Backend
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetWeatherData(string searchString);
    }
}
