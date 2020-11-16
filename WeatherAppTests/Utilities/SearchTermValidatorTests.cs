using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using WeatherApp.Utilities;

namespace WeatherAppTests.Utilities
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
        [DataRow("Helsinki")]
        [DataRow("Rio De Janeiro")]
        [DataRow("Stockholm")]
        public void SearchTermValidator_ShouldReturnTrueForValidStrings(string searchTerm)
        {
            var result = _searchTermValidator.IsValid(searchTerm);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("    ")]
        public void SearchTermValidator_ShouldReturnFalseForInvalidStrings(string searchTerm)
        {
            var result = _searchTermValidator.IsValid(searchTerm);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SearchTermValidator_ShouldReturnFalseForLongStrings()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < 50; i++)
            {
                builder.Append("aasdfasdfa");
            }

            var result = _searchTermValidator.IsValid(builder.ToString());

            Assert.IsFalse(result);
        }
    }
}
