using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.LanguageData
{
    [TestFixture]
    internal class LanguageDataOperatorTests
    {
        [Test]
        public void AdditionOperator_ShouldMergeTwoLanguageDataObjects()
        {
            // Arrange
            var language = new DGGLocalization.Data.LanguageShort("en");
            var languageData1 = new DGGLocalization.Data.LanguageData(language, "Hello");
            var languageData2 = new DGGLocalization.Data.LanguageData(language, " World");

            // Act
            var result = languageData1 + languageData2;

            // Assert
            Assert.AreEqual("Hello World", result.Localization);
            Assert.AreEqual(language, result.Language);
        }

        [Test]
        public void AdditionOperator_ShouldAddStringToLocalization()
        {
            // Arrange
            var language = new DGGLocalization.Data.LanguageShort("en");
            var languageData = new DGGLocalization.Data.LanguageData(language, "Hello");

            // Act
            var result = languageData + " World";

            // Assert
            Assert.AreEqual("Hello World", result.Localization);
            Assert.AreEqual(language, result.Language);
        }
    }
}