using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using JustinWare.MerchantsGuide.Core.Contracts;
using JustinWare.MerchantsGuide.Core.Models;
using JustinWare.MerchantsGuide.Core.Services;
using JustinWare.MerchantsGuide.Core.Tests.Dummies;

namespace JustinWare.MerchantsGuide.Core.Tests.Services
{
  [TestFixture]
  public class QueryServiceTests
  {
    private Mock<IFactRepository> _mockFactRepository;
    private Mock<IQuery> _mockQuery;

    private IQueryService _service;

    [SetUp]
    public void Setup_Scenario()
    {
      _mockFactRepository = new Mock<IFactRepository>();
      _mockQuery = new Mock<IQuery>();

      _mockQuery.Setup(q => q.IsValid).Returns(true);
      _mockFactRepository.Setup(fr => fr.Facts).Returns(CreateDummyFacts());

      _service = new QueryService(_mockFactRepository.Object);
    }

    private IEnumerable<IFact> CreateDummyFacts()
    {
      return new List<IFact>
             {
               DummyInputObjectMother.CreateFact("bla", "I"),
               DummyInputObjectMother.CreateFact("fob", "V"),
               DummyInputObjectMother.CreateFact("gadi", "X"),
               DummyInputObjectMother.CreateFact("dre", "L"),
               DummyInputObjectMother.CreateFact("bla bla", "34", "Unobtainium", "Donkeys"),
               DummyInputObjectMother.CreateFact("bla fob", "57800", "Awesainium", "Donkeys"),
               DummyInputObjectMother.CreateFact("gadi gadi", "3910", "Dirt", "Donkeys"),
             };
    }

    [Test]
    public void ShouldReturnInvalidQueryText_WhenProcessQueryInvokedWithInvalidQuery()
    {
      // Arrange
      _mockQuery.Setup(q => q.IsValid).Returns(false);

      // Act
      var result = _service.ProcessQuery(_mockQuery.Object);

      // Assert
      Assert.AreEqual(Constants.Output.InvalidQueryText, result);
    }

    [Test]
    [TestCase("bla fob", "4")]
    [TestCase("gadi fob bla bla", "17")]
    [TestCase("dre gadi gadi fob bla", "76")]
    public void ShouldReturnIntegerLookupText_WhenProcessQueryInvokedWithIntegerQuestion(string amount, string expected)
    {
      // Arrange
      _mockQuery.Setup(q => q.IsIntegerLookup).Returns(true);
      _mockQuery.Setup(q => q.Amount).Returns(amount);

      // Act
      var result = _service.ProcessQuery(_mockQuery.Object);

      // Assert
      Assert.AreEqual(string.Format(Constants.Output.IntegerLookupText, amount, expected), result);
    }

    [Test]
    public void ShouldReturnInsufficientDataText_WhenProcessQueryInvokedWithUnknownCommodity()
    {
      // Arrange
      var mockQuery = DummyInputObjectMother.CreateQuery("bla bla bla", "Donkeys", "Unknownium");

      // Act
      var result = _service.ProcessQuery(mockQuery);

      // Assert
      Assert.AreEqual(string.Format(Constants.Output.InsufficientFactData, "irrelevant"), result);
    }

    [Test]
    [TestCase("bla fob", "Donkeys", "Unobtainium", "68")]
    [TestCase("dre gadi gadi bla bla bla", "Donkeys", "Unobtainium", "1241")]
    [TestCase("bla fob", "Donkeys", "Awesainium", "57800")]
    [TestCase("dre gadi", "Donkeys", "Awesainium", "867000")]
    [TestCase("bla fob", "Donkeys", "Dirt", "782")]
    [TestCase("gadi fob bla bla", "Donkeys", "Dirt", "3323")]
    public void ShouldReturnCommodityLookupText_WhenProcessQueryInvokedWithCommodityQuery(string amount, string unit,
                                                              string item, string expected)
    {
      // Arrange
      var mockQuery = DummyInputObjectMother.CreateQuery(amount, unit, item);

      // Act
      var result = _service.ProcessQuery(mockQuery);

      // Assert
      Assert.AreEqual(string.Format(Constants.Output.CommodityLookupText, amount, item, expected, unit), result);
    }
  }
}
