using System.Linq;
using Moq;
using NUnit.Framework;
using JustinWare.MerchantsGuide.Core.Contracts;
using JustinWare.MerchantsGuide.Core.Services;

namespace JustinWare.MerchantsGuide.Core.Tests.Services
{
  [TestFixture]
  public class FactRepositoryTests
  {
    private IFactRepository _factRepository;

    [SetUp]
    public void Setup_Scenario()
    {
      _factRepository = new FactRepository();
    }

    [Test]
    public void ShouldCreateFactList_WhenInitialised()
    {
      // Assert
      Assert.IsNotNull(_factRepository.Facts);
    }

    [Test]
    public void ShouldAddFactToList_WhenAddIsInvoked()
    {
      // Arrange
      var mockFact = new Mock<IFact>();

      // Act
      _factRepository.Add(mockFact.Object);

      // Assert
      Assert.AreEqual(1, _factRepository.Facts.Count());
      Assert.AreEqual(mockFact.Object, _factRepository.Facts.First());
    }
  }
}
