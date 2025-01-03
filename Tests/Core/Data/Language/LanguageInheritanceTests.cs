//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.Language
{
    [TestFixture]
    internal class LanguageInheritanceTests
    {
        [Test]
        public void Inheritance_ShouldBehaveLikeLanguageShort()
        {
            // Arrange
            var languageCode = "es";
            var languageName = "Spanish";
            var language = new DGGLocalization.Data.Language(languageCode, languageName);

            // Act
            string result = language;

            // Assert
            Assert.AreEqual(languageCode, result);
        }
    }
}