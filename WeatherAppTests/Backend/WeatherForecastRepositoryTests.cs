using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using System.Linq;
using WeatherApp.Backend;
using WeatherApp.Models;

namespace WeatherAppTests.Backend
{
    [TestClass()]
    public class WeatherForecastRepositoryTests
    {
        private Mock<IRepository<WeatherForecastModel>> _weatherForecastRepository;

        [TestInitialize]
        public void InitTests()
        {
            _weatherForecastRepository = new Mock<IRepository<WeatherForecastModel>>();
        }

        [TestMethod()]
        [DataRow("test")]
        public void GetWeatherForecastModels_ShouldReturnItemsWhenCalled(string searchString)
        {
            _weatherForecastRepository.Setup(x => x.GetWeatherData(searchString)).Returns(new List<WeatherForecastModel>()
           { new WeatherForecastModel() 
            
            });

            var result = _weatherForecastRepository.Object.GetWeatherData(searchString);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<WeatherForecastModel>));
            Assert.AreEqual(1, result.Count());
        }
    }
}