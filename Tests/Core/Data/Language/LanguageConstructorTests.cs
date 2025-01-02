using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.Language
{
    [TestFixture]
    internal class LanguageConstructorTests
    {
        [Test]
        public void Constructor_ShouldInitializeWithGivenCodeAndName()
        {
            // Arrange
            var languageCode = "en";
            var languageName = "English";

            // Act
            var language = new DGGLocalization.Data.Language(languageCode, languageName);

            // Assert
            Assert.AreEqual(languageCode, language.LanguageCode);
            Assert.AreEqual(languageName, language.LanguageName);
        }
    }
}