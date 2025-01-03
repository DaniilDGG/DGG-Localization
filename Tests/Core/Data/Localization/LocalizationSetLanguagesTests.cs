//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.Localization
{
    [TestFixture]
    internal class LocalizationSetLanguagesTests
    {
        [Test]
        public void SetLanguages_ShouldUpdateLanguagesAndFixLocalizations()
        {
            // Arrange
            var localization = new DGGLocalization.Data.Localization();
            var newLanguages = new[]
            {
                new DGGLocalization.Data.Language("fr", "French"),
                new DGGLocalization.Data.Language("es", "Spanish")
            };

            var initialLocalizationCode = "test_code";
            var initialLanguageData = new List<DGGLocalization.Data.LanguageData>
            {
                new(new DGGLocalization.Data.LanguageShort("en"), "Hello")
            };

            localization.SetLocalization(initialLocalizationCode, initialLanguageData);

            // Act
            localization.SetLocalization(newLanguages);

            // Assert
            Assert.AreEqual(2, localization.Languages.Length);
            Assert.AreEqual("fr", localization.Languages[0].LanguageCode);
            Assert.AreEqual("es", localization.Languages[1].LanguageCode);

            var fixedData = localization.Localizations[0].Data;
            
            Assert.AreEqual(2, fixedData.Count);
            Assert.AreEqual("empty", fixedData.Find(data => data.Language.LanguageCode == "fr").Localization);
            Assert.AreEqual("empty", fixedData.Find(data => data.Language.LanguageCode == "es").Localization);
        }
        
        [Test]
        public void SetLanguages_ShouldNotChangeLanguages_WhenEmptyArrayIsPassed()
        {
            // Arrange
            var localization = new DGGLocalization.Data.Localization();
            var emptyLanguages = Array.Empty<DGGLocalization.Data.Language>();

            // Act
            localization.SetLocalization(emptyLanguages);

            // Assert
            UnityEngine.TestTools.LogAssert.Expect(UnityEngine.LogType.Error, "Languages == 0! Logic aborted...");
            
            Assert.AreEqual(1, localization.Languages.Length);
            Assert.AreEqual("en", localization.Languages[0].LanguageCode);
        }
    }
}