using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.LocalizationString
{
    [TestFixture]
    internal class LocalizationStringImplicitTests
    {
        [Test]
        public void ImplicitStringConversion_ShouldReturnCorrectString()
        {
            // Arrange
            var expectedValue = "TestKey";
            var localizationString = new DGGLocalization.Data.LocalizationString(expectedValue);

            // Act
            string actualValue = localizationString;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void ImplicitLocalizationStringConversion_ShouldReturnCorrectLocalizationString()
        {
            // Arrange
            var key = "TestKey";

            // Act
            DGGLocalization.Data.LocalizationString localizationString = key;

            // Assert
            Assert.AreEqual(key, localizationString.Value);
        }
    }
}