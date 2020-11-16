using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WeatherApp.Backend;
using WeatherApp.Models;

namespace WeatherAppTests.Backend
{
    [TestClass]
    public class WeatherForecastModelMemoryCacheTests
    {
        private IMemoryCache<WeatherForecastModel> _memoryCache;

        [TestInitialize]
        public void InitTests()
        {
            _memoryCache = new WeatherForecastModelMemoryCache<WeatherForecastModel>();
        }

        [TestMethod]
        public void WeatherForecastModelMemoryCache_ShouldWork()
        {
            var list = new List<WeatherForecastModel>();
            var searchString = "search";

            var result = _memoryCache.GetOrCreate(searchString, () => list);
        }
    }
}
