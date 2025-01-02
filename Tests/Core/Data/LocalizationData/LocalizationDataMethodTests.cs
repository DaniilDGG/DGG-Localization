using System.Collections.Generic;
using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.LocalizationData
{
    [TestFixture]
    internal class LocalizationDataMethodTests
    {
        [Test]
        public void GetTargetLocalization_ShouldReturnCorrectLocalization()
        {
            // Arrange
            var language = new DGGLocalization.Data.LanguageShort("en");
            var languageData = new List<DGGLocalization.Data.LanguageData>
            {
                new(language, "Hello"),
                new(new DGGLocalization.Data.LanguageShort("fr"), "Bonjour")
            };
            
            var localizationData = new DGGLocalization.Data.LocalizationData("test_code", languageData);

            // Act
            var result = localizationData.GetTargetLocalization(language);

            // Assert
            Assert.AreEqual("Hello", result.Localization);
        }

        [Test]
        public void GetTargetLocalization_ShouldReturnDefaultIfNotFound()
        {
            // Arrange
            var language = new DGGLocalization.Data.LanguageShort("es");
            var enLocalization = new DGGLocalization.Data.LanguageData(new DGGLocalization.Data.LanguageShort("en"), "Hello");
            var languageData = new List<DGGLocalization.Data.LanguageData>
            {
                enLocalization,
                new(new DGGLocalization.Data.LanguageShort("fr"), "Bonjour")
            };
            var localizationData = new DGGLocalization.Data.LocalizationData("test_code", languageData);

            // Act
            var result = localizationData.GetTargetLocalization(language);

            // Assert
            Assert.AreEqual(enLocalization, result);
        }
        
        [Test]
        public void GetTargetLocalization_ShouldReturnNull()
        {
            // Arrange
            var language = new DGGLocalization.Data.LanguageShort("es");
            var localizationData = new DGGLocalization.Data.LocalizationData("test_code", new List<DGGLocalization.Data.LanguageData>());

            // Act
            var result = localizationData.GetTargetLocalization(language);

            // Assert
            Assert.AreEqual("Localization is null!", result.Localization);
        }
    }
}