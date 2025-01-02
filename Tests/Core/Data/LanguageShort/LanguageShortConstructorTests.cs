using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.LanguageShort
{
    [TestFixture]
    internal class LanguageShortConstructorTests
    {
        [Test]
        public void Constructor_ShouldInitializeWithGivenCode()
        {
            // Arrange
            var languageCode = "en";

            // Act
            var languageShort = new DGGLocalization.Data.LanguageShort(languageCode);

            // Assert
            Assert.AreEqual(languageCode, languageShort.LanguageCode);
        }
    }
}