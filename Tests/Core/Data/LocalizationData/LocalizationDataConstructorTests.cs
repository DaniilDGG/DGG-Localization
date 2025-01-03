//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.LocalizationData
{
    [TestFixture]
    internal class LocalizationDataConstructorTests
    {
        [Test]
        public void Constructor_ShouldInitializeWithGivenValues()
        {
            // Arrange
            var localizationCode = "test_code";
            var languageData = new List<DGGLocalization.Data.LanguageData>
            {
                new(new DGGLocalization.Data.LanguageShort("en"), "Hello"),
                new(new DGGLocalization.Data.LanguageShort("fr"), "Bonjour")
            };

            // Act
            var localizationData = new DGGLocalization.Data.LocalizationData(localizationCode, languageData);

            // Assert
            Assert.AreEqual(localizationCode, localizationData.LocalizationCode);
            Assert.AreEqual(languageData, localizationData.Data);
        }
    }
}