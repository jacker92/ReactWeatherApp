using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherPrediction.Controllers;
using WeatherPrediction.Models;
using WeatherPrediction.Utilities;

namespace WeatherPredictionTests.Controllers
{
    [TestClass]
    public class WeatherForecastControllerTests
    {
        private WeatherForecastController _weatherForecastController;

        [TestInitialize]
        public void InitTests()
        {
            _weatherForecastController = new WeatherForecastController(
                                            null, 
                                            null, 
                                            new RequestBodyParser(), 
                                            new SearchTermValidator());
        }

        [TestMethod]

        public void WeatherForecastController_ShouldWork_WhenGetIsCalled()
        {
           var result = _weatherForecastController.Get(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<WeatherForecastModel>));
        }
    }
}
