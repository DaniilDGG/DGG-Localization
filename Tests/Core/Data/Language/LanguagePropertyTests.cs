using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.Language
{
    [TestFixture]
    internal class LanguagePropertyTests
    {
        [Test]
        public void LanguageCode_ShouldReturnCorrectCode()
        {
            // Arrange
            var languageCode = "fr";
            var language = new DGGLocalization.Data.Language(languageCode, "French");

            // Act
            var result = language.LanguageCode;

            // Assert
            Assert.AreEqual(languageCode, result);
        }

        [Test]
        public void LanguageName_ShouldReturnCorrectName()
        {
            // Arrange
            var languageName = "German";
            var language = new DGGLocalization.Data.Language("de", languageName);

            // Act
            var result = language.LanguageName;

            // Assert
            Assert.AreEqual(languageName, result);
        }
    }
}