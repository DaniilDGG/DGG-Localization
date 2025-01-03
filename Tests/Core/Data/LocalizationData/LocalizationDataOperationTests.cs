//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.LocalizationData
{
    [TestFixture]
    internal class LocalizationDataOperationTests
    {
        [Test]
        public void Replace_ShouldReplaceStringInAllLanguages()
        {
            // Arrange
            var languageData = new List<DGGLocalization.Data.LanguageData>
            {
                new(new DGGLocalization.Data.LanguageShort("en"), "Hello"),
                new(new DGGLocalization.Data.LanguageShort("fr"), "Bonjour")
            };
            var localizationData = new DGGLocalization.Data.LocalizationData("test_code", languageData);

            // Act
            var result = localizationData.Replace("Hello", "Hi");

            // Assert
            Assert.AreEqual("Hi", result.Data[0].Localization);
            Assert.AreEqual("Bonjour", result.Data[1].Localization);
        }

        [Test]
        public void OperatorPlus_ShouldCombineTwoLocalizationDataObjects()
        {
            // Arrange
            var dataA = new DGGLocalization.Data.LocalizationData("codeA", new List<DGGLocalization.Data.LanguageData>
            {
                new(new DGGLocalization.Data.LanguageShort("en"), "Hello"),
                new(new DGGLocalization.Data.LanguageShort("fr"), "Bonjour")
            });
            var dataB = new DGGLocalization.Data.LocalizationData("codeB", new List<DGGLocalization.Data.LanguageData>
            {
                new(new DGGLocalization.Data.LanguageShort("en"), " World"),
                new(new DGGLocalization.Data.LanguageShort("fr"), " le Monde")
            });

            // Act
            var result = dataA + dataB;

            // Assert
            Assert.AreEqual("Hello World", result.Data[0].Localization);
            Assert.AreEqual("Bonjour le Monde", result.Data[1].Localization);
        }

        [Test]
        public void OperatorPlus_ShouldAddStringToLocalizationData()
        {
            // Arrange
            var dataA = new DGGLocalization.Data.LocalizationData("codeA", new List<DGGLocalization.Data.LanguageData>
            {
                new(new DGGLocalization.Data.LanguageShort("en"), "Hello"),
                new(new DGGLocalization.Data.LanguageShort("fr"), "Bonjour")
            });
            var suffix = "!";

            // Act
            var result = dataA + suffix;

            // Assert
            Assert.AreEqual("Hello!", result.Data[0].Localization);
            Assert.AreEqual("Bonjour!", result.Data[1].Localization);
        }
    }
}