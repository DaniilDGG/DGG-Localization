//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.Localization
{
    [TestFixture]
    internal class LocalizationConstructorTests
    {
        [Test]
        public void Constructor_ShouldInitializeWithDefaultValues()
        {
            // Act
            var localization = new DGGLocalization.Data.Localization();

            // Assert
            Assert.AreNotEqual(Guid.Empty, localization.GUID);
            Assert.IsNotNull(localization.Localizations);
            Assert.IsNotNull(localization.Languages);
            Assert.AreEqual(1, localization.Languages.Length);
            Assert.AreEqual("en", localization.Languages[0].LanguageCode);
        }
    }
}