using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherPrediction;

namespace WeatherPrediction.Backend
{
    public class WeatherForecastRepository : IRepository<WeatherForecastModel>
    {
        public WeatherForecastModel GetItem(string ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WeatherForecastModel> GetItems()
        {
            return new List<WeatherForecastModel> { new WeatherForecastModel() };
        }
    }
}
