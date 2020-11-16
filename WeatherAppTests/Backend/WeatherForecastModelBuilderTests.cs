using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using WeatherApp.Backend;
using WeatherApp.Models;


namespace WeatherAppTests.Backend
{
    [TestClass]
    public class WeatherForecastModelBuilderTests
    {

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void WeatherForecastModelBuilder_ShouldReturnEmptyArray_WhenInvalidDataIsPosted(string data)
        {
            var result = WeatherForecastModelBuilder.Build(data);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<WeatherForecastModel>));
        }

        [TestMethod]
        public void WeatherForecastModelBuilder_ShouldWork_WhenValidDataIsBeingPosted()
        {
            var result = WeatherForecastModelBuilder.Build(GetFiveDaysWeatherDataSample()) ;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<WeatherForecastModel>));
        }

        private string GetFiveDaysWeatherDataSample()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestData/FiveDaysWeatherData.json");
            return File.ReadAllText(filePath);
        }

    }
}
