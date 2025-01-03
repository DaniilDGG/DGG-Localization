//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

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