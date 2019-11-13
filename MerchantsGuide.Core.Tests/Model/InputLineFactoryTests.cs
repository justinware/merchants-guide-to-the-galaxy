using NUnit.Framework;
using JustinWare.MerchantsGuide.Core.Contracts;
using JustinWare.MerchantsGuide.Core.Models;

namespace JustinWare.MerchantsGuide.Core.Tests.Model
{
  [TestFixture]
  public class InputLineFactoryTests
  {
    private IInputLineFactory _factory;

    [Test]
    public void Setup_Scenario()
    {
      _factory = new InputLineFactory();
    }

    [Test]
    public void ShouldCreateFact_WhenCreateFactInvoked()
    {
      // Arrange
      const string text = "Some Fact Text is cool";

      // Act
      var result = _factory.CreateFact(text);

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(text, result.OriginalText);
    }

    [Test]
    public void ShouldCreateQuery_WhenCreateQueryInvoked()
    {
      // Arrange
      const string text = "What is Some Query Text ?";

      // Act
      var result = _factory.CreateQuery(text);

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(text, result.OriginalText);
    }
  }
}
