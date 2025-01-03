//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.LanguageData
{
    [TestFixture]
    internal class LanguageDataPropertyTests
    {
        [Test]
        public void Language_ShouldReturnCorrectLanguage()
        {
            // Arrange
            var language = new DGGLocalization.Data.LanguageShort("fr");
            var languageData = new DGGLocalization.Data.LanguageData(language, "Bonjour");

            // Act
            var result = languageData.Language;

            // Assert
            Assert.AreEqual(language, result);
        }

        [Test]
        public void Localization_ShouldReturnCorrectContent()
        {
            // Arrange
            var localization = "Hola";
            var languageData = new DGGLocalization.Data.LanguageData(new DGGLocalization.Data.LanguageShort("es"), localization);

            // Act
            var result = languageData.Localization;

            // Assert
            Assert.AreEqual(localization, result);
        }
    }
}