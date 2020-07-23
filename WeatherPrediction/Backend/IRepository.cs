using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherPrediction.Backend
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetItems(string searchString);
    }
}
