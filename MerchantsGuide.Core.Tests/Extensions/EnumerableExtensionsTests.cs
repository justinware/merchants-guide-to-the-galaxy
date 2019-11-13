using NUnit.Framework;
using JustinWare.MerchantsGuide.Core.Extensions;

namespace JustinWare.MerchantsGuide.Core.Tests.Extensions
{
  [TestFixture]
  public class EnumerableExtensionsTests
  {
    [Test]
    public void ShouldIterateOverAllItems_WhenForEachInvoked()
    {
      // Arrange
      var count = 0;
      var items = new[] { "Item 1", "Item 2", "Item 3", "Item 4" };

      // Act
      items.ForEach(i => { count++; });

      // Assert
      Assert.AreEqual(4, count);
    }
  }
}
