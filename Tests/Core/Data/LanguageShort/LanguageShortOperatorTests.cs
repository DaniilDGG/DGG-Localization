using NUnit.Framework;

namespace DGGLocalization.Tests.Core.Data.LanguageShort
{
    [TestFixture]
    internal class LanguageShortOperatorTests
    {
        [Test]
        public void EqualityOperator_ShouldReturnTrueForEqualObjects()
        {
            // Arrange
            var language1 = new DGGLocalization.Data.LanguageShort("en");
            var language2 = new DGGLocalization.Data.LanguageShort("en");

            // Act
            var result = language1 == language2;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EqualityOperator_ShouldReturnFalseForDifferentObjects()
        {
            // Arrange
            var language1 = new DGGLocalization.Data.LanguageShort("en");
            var language2 = new DGGLocalization.Data.LanguageShort("fr");

            // Act
            var result = language1 == language2;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void InequalityOperator_ShouldReturnTrueForDifferentObjects()
        {
            // Arrange
            var language1 = new DGGLocalization.Data.LanguageShort("en");
            var language2 = new DGGLocalization.Data.LanguageShort("fr");

            // Act
            var result = language1 != language2;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void InequalityOperator_ShouldReturnFalseForEqualObjects()
        {
            // Arrange
            var language1 = new DGGLocalization.Data.LanguageShort("en");
            var language2 = new DGGLocalization.Data.LanguageShort("en");

            // Act
            var result = language1 != language2;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Equals_ShouldReturnTrueForEqualObjects()
        {
            // Arrange
            var language1 = new DGGLocalization.Data.LanguageShort("en");
            var language2 = new DGGLocalization.Data.LanguageShort("en");

            // Act
            var result = language1.Equals(language2);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_ShouldReturnFalseForDifferentObjects()
        {
            // Arrange
            var language1 = new DGGLocalization.Data.LanguageShort("en");
            var language2 = new DGGLocalization.Data.LanguageShort("fr");

            // Act
            var result = language1.Equals(language2);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Equals_ShouldReturnFalseWhenComparedToNull()
        {
            // Arrange
            var language = new DGGLocalization.Data.LanguageShort("en");

            // Act
            var result = language.Equals(null);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetHashCode_ShouldReturnSameHashCodeForEqualObjects()
        {
            // Arrange
            var language1 = new DGGLocalization.Data.LanguageShort("en");
            var language2 = new DGGLocalization.Data.LanguageShort("en");

            // Act
            var hashCode1 = language1.GetHashCode();
            var hashCode2 = language2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2);
        }

        [Test]
        public void GetHashCode_ShouldReturnDifferentHashCodeForDifferentObjects()
        {
            // Arrange
            var language1 = new DGGLocalization.Data.LanguageShort("en");
            var language2 = new DGGLocalization.Data.LanguageShort("fr");

            // Act
            var hashCode1 = language1.GetHashCode();
            var hashCode2 = language2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2);
        }
    }
}