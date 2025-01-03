//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.LanguageShort
{
    [TestFixture]
    internal class LanguageShortPropertyTests
    {
        [Test]
        public void LanguageCode_ShouldReturnCorrectValue()
        {
            // Arrange
            var languageCode = "fr";
            var languageShort = new DGGLocalization.Data.LanguageShort(languageCode);

            // Act
            var result = languageShort.LanguageCode;

            // Assert
            Assert.AreEqual(languageCode, result);
        }
    }
}