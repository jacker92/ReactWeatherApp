using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherPrediction.Utilities;

namespace WeatherPredictionTests.Utilities
{
    [TestClass]
    public class SearchTermValidatorTests
    {
        private SearchTermValidator _searchTermValidator;

        [TestInitialize]
        public void InitTests()
        {
            _searchTermValidator = new SearchTermValidator();
        }   

        [TestMethod]
        public void SearchTermValidator_ShouldReturnTrueForValidStrings(string searchTerm)
        {
            Assert.Fail();
        }

        [TestMethod]
        public void SearchTermValidator_ShouldReturnFalseForInvalidStrings(string searchTerm)
        {
            Assert.Fail();
        }
    }
}
