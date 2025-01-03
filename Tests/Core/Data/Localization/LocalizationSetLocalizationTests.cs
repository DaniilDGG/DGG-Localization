//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.Localization
{
    [TestFixture]
    internal class LocalizationSetLocalizationTests
    {
        [Test]
        public void SetLocalization_ShouldAddNewLocalizationIfNotExists()
        {
            // Arrange
            var localization = new DGGLocalization.Data.Localization();
            var localizationCode = "test_code";
            
            var languageData = new List<DGGLocalization.Data.LanguageData>
            {
                new(new DGGLocalization.Data.LanguageShort("en"), "Hello")
            };

            // Act
            localization.SetLocalization(localizationCode, languageData);

            // Assert
            Assert.AreEqual(1, localization.Localizations.Length);
            Assert.AreEqual(localizationCode, localization.Localizations[0].LocalizationCode);
            Assert.AreEqual(languageData.Count, localization.Localizations[0].Data.Count);
        }

        [Test]
        public void SetLocalization_ShouldUpdateExistingLocalization()
        {
            // Arrange
            var localization = new DGGLocalization.Data.Localization();
            var localizationCode = "test_code";
            
            var initialData = new List<DGGLocalization.Data.LanguageData>
            {
                new(new DGGLocalization.Data.LanguageShort("en"), "Hello")
            };
            var updatedData = new List<DGGLocalization.Data.LanguageData>
            {
                new(new DGGLocalization.Data.LanguageShort("en"), "Updated Hello")
            };

            localization.SetLocalization(localizationCode, initialData);

            // Act
            localization.SetLocalization(localizationCode, updatedData);

            // Assert
            Assert.AreEqual(1, localization.Localizations.Length);
            Assert.AreEqual("Updated Hello", localization.Localizations[0].Data[0].Localization);
        }

        [Test]
        public void SetLocalization_ShouldReplaceAllLocalizations()
        {
            // Arrange
            var localization = new DGGLocalization.Data.Localization();
            var newLocalizations = new List<DGGLocalization.Data.LocalizationData>
            {
                new("new_code", new List<DGGLocalization.Data.LanguageData>
                {
                    new(new DGGLocalization.Data.LanguageShort("fr"), "Bonjour")
                })
            };

            // Act
            localization.SetLocalization(newLocalizations);

            // Assert
            Assert.AreEqual(1, localization.Localizations.Length);
            Assert.AreEqual("new_code", localization.Localizations[0].LocalizationCode);
        }
    }
}