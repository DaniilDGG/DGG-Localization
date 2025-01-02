using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.LanguageData
{
    [TestFixture]
    internal class LanguageDataConstructorTests
    {
        [Test]
        public void Constructor_ShouldInitializeWithGivenValues()
        {
            // Arrange
            var language = new DGGLocalization.Data.LanguageShort("en");
            var localization = "Hello";

            // Act
            var languageData = new DGGLocalization.Data.LanguageData(language, localization);

            // Assert
            Assert.AreEqual(language, languageData.Language);
            Assert.AreEqual(localization, languageData.Localization);
        }
    }
}