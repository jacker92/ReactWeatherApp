using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherPrediction.Backend;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using System.Linq;

namespace WeatherPredictionTests.Backend
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
            _weatherForecastRepository.Setup(x => x.GetItems(searchString)).Returns(new List<WeatherForecastModel>()
           { new WeatherForecastModel() 
            
            });

            var result = _weatherForecastRepository.Object.GetItems(searchString);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<WeatherForecastModel>));
            Assert.AreEqual(1, result.Count());
        }
    }
}