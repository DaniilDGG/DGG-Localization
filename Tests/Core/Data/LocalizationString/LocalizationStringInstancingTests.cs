//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.LocalizationString
{
    [TestFixture]
    internal class LocalizationStringInstancingTests
    {
        [Test]
        public void Constructor_ShouldInitializeWithGivenValue()
        {
            // Arrange
            var expectedValue = "TestKey";

            // Act
            var localizationString = new DGGLocalization.Data.LocalizationString(expectedValue);

            // Assert
            Assert.AreEqual(expectedValue, localizationString.Value);
        }

        [Test]
        public void ValueProperty_ShouldReturnCorrectValue()
        {
            // Arrange
            var expectedValue = "TestKey";
            var localizationString = new DGGLocalization.Data.LocalizationString(expectedValue);

            // Act
            var actualValue = localizationString.Value;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}