//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.LanguageShort
{
    [TestFixture]
    internal class LanguageShortImplicitTests
    {
        [Test]
        public void ImplicitStringConversion_ShouldReturnCorrectString()
        {
            // Arrange
            var languageCode = "de";
            var languageShort = new DGGLocalization.Data.LanguageShort(languageCode);

            // Act
            string result = languageShort;

            // Assert
            Assert.AreEqual(languageCode, result);
        }

        [Test]
        public void ImplicitStringConversion_ShouldHandleNullCorrectly()
        {
            // Arrange
            DGGLocalization.Data.LanguageShort languageShort = null;

            // Act
            string result = languageShort;

            // Assert
            Assert.IsNull(result);
        }
    }
}