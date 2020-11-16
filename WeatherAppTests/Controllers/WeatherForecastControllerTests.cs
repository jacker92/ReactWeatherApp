using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WeatherApp.Controllers;
using WeatherApp.Models;
using WeatherApp.Utilities;

namespace WeatherAppTests.Controllers
{
    [TestClass]
    public class WeatherForecastControllerTests
    {
        private WeatherForecastController _weatherForecastController;

        [TestInitialize]
        public void InitTests()
        {
            _weatherForecastController = new WeatherForecastController(
                                           new Mock<ILogger<WeatherForecastController>>().Object,
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
