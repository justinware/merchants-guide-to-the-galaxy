using System;
using System.IO;
using NUnit.Framework;
using JustinWare.MerchantsGuide.Core.Models;

namespace JustinWare.MerchantsGuide.Core.Tests.Model
{
  [TestFixture]
  public class RomanNumeralTests
  {
    [Test]
    public void ShouldThrowNotImplementedException_WhenCreateFromIntInvoked()
    {
      // Act + Assert
      Assert.Throws<NotImplementedException>(() => RomanNumeral.CreateFromInt(42));
    }

    [Test]
    public void ShouldThrowInvalidDataException_WhenCreateFromStringInvokedWithInvalidData()
    {
      // Act + Assert
      Assert.Throws<InvalidDataException>(() => RomanNumeral.CreateFromString(String.Empty));
    }

    [Test]
    public void ShouldCreateInstance_WhenCreateFromStringInvokedWithValidData()
    {
      // Arrange
      const string expected = "MCXX";

      // Act
      var result = RomanNumeral.CreateFromString(expected);

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(expected, result.Value);
    }

    [Test]
    public void ShouldUpperCaseValue_WhenCreateFromStringInvokedWithValidData()
    {
      // Arrange
      const string expected = "MCXX";

      // Act
      var result = RomanNumeral.CreateFromString("mcxx");

      // Assert
      Assert.AreEqual(expected, result.Value);
    }

    [Test]
    [TestCase("I", 1)]
    [TestCase("V", 5)]
    [TestCase("X", 10)]
    [TestCase("L", 50)]
    [TestCase("C", 100)]
    [TestCase("D", 500)]
    [TestCase("M", 1000)]
    [TestCase("II", 2)]
    [TestCase("VIII", 8)]
    [TestCase("MMMCCCXXXIII", 3333)]
    [TestCase("IV", 4)]
    [TestCase("IX", 9)]
    [TestCase("XL", 40)]
    [TestCase("XC", 90)]
    [TestCase("CD", 400)]
    [TestCase("CM", 900)]
    [TestCase("MMMCMXCIX", 3999)]
    [TestCase("MMVI", 2006)]
    [TestCase("MCMXLIV", 1944)]
    [TestCase("MCMIII", 1903)]
    [TestCase("XXXIV", 34)]
    [TestCase("LXXXVIII", 88)]
    [TestCase("DCCVII", 707)]
    [TestCase("XCVIII", 98)]
    [TestCase("CCVII", 207)]
    [TestCase("MCMXC", 1990)]
    public void ShouldConvertStringRepresentationToInteger_WhenGetIntegerValueInvoked(string value, int expected)
    {
      // Arrange
      var numeral = RomanNumeral.CreateFromString(value);

      // Act
      var result = numeral.GetIntegerValue();

      // Assert
      Assert.AreEqual(expected, result);
    }
  }
}
