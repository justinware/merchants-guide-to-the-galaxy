using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using JustinWare.MerchantsGuide.Core.Contracts;
using JustinWare.MerchantsGuide.Core.Services;
using JustinWare.MerchantsGuide.Core.Tests.Dummies;

namespace JustinWare.MerchantsGuide.Core.Tests.Services
{
  [TestFixture]
  public class ParserServiceTests
  {
    private Mock<IInputLineFactory> _mockInputLineFactory;

    private IParserService _service;

    [SetUp]
    public void Setup_Scenario()
    {
      _mockInputLineFactory = new Mock<IInputLineFactory>();

      _service = new ParserService(_mockInputLineFactory.Object);
    }

    [Test]
    public void ShouldCreateInputSet_WhenParseInputTextInvoked()
    {
      // Act
      var result = _service.ParseInputText(string.Empty);

      // Assert
      Assert.IsNotNull(result);
    }

    [Test]
    public void ShouldCreateQueries_WhenParseInputTextInvokedWithQueriesText()
    {
      // Arrange
      var mockData = "Query 1 ?" + Environment.NewLine + "Query 2 ?" + Environment.NewLine + "Query 3 ?";
      var mockQuery = new Mock<IQuery>();
      _mockInputLineFactory.Setup(iif => iif.CreateQuery(It.IsAny<string>()))
                    .Returns(mockQuery.Object);

      // Act
      var result = _service.ParseInputText(mockData).Queries;

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(3, result.Count());
    }

    [Test]
    public void ShouldCreateFacts_WhenParseInputTextInvokedWithFactText()
    {
      // Arrange
      var mockData = "Fact 1" + Environment.NewLine + "Fact 2" + Environment.NewLine + "Fact 3" +
                Environment.NewLine + "Fact 4" + Environment.NewLine + "Fact 5";
      var mockFact = new Mock<IFact>();
      _mockInputLineFactory.Setup(iif => iif.CreateFact(It.IsAny<string>()))
                    .Returns(mockFact.Object);

      // Act
      var result = _service.ParseInputText(mockData).Facts;

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(5, result.Count());
    }

    [Test]
    [TestCase("Fact 1 has something", false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase("Fact 1 is something", true)]
    public void ShouldMarkFactsAsAppropriatelyValid_WhenParseInputTextInvokedWithDifferingFactText(string data, bool expected)
    {
      // Arrange
      var mockData = "Fact 1" + Environment.NewLine;
      var mockFact = new DummyFact(data);
      _mockInputLineFactory.Setup(iif => iif.CreateFact(It.IsAny<string>()))
                    .Returns(mockFact);

      // Act
      _service.ParseInputText(mockData).Facts.ToList();

      // Assert
      Assert.AreEqual(expected, mockFact.IsValid);
    }

    [Test]
    public void ShouldSetXAndYEquationPartsOnFacts_WhenParseInputTextInvokedWithValidFactText()
    {
      // Arrange
      var mockData = "Fact 1" + Environment.NewLine;
      var mockFact = new DummyFact("Fact 1 is Something");
      _mockInputLineFactory.Setup(iif => iif.CreateFact(It.IsAny<string>()))
                    .Returns(mockFact);

      // Act
      _service.ParseInputText(mockData).Facts.ToList();

      // Assert
      Assert.AreEqual("Fact 1", mockFact.X);
      Assert.AreEqual("Something", mockFact.Y);
    }

    [Test]
    public void ShouldSetIsRomanNumeralEqualityOnFacts_WhenParseInputTextInvokedWithRomanNumeralAssignment()
    {
      // Arrange
      var mockData = "Something is L" + Environment.NewLine;
      var mockFact = new DummyFact("Something is L");
      _mockInputLineFactory.Setup(iif => iif.CreateFact(It.IsAny<string>()))
                    .Returns(mockFact);

      // Act
      _service.ParseInputText(mockData).Facts.ToList();

      // Assert
      Assert.IsTrue(mockFact.IsRomanNumeralEquality);
    }

    [Test]
    public void ShouldSetFactDetails_WhenParseInputTextInvokedWithCommodityPrice()
    {
      // Arrange
      var mockData = "bla bla Unobtainium is 67 coins" + Environment.NewLine;
      var mockFact = new DummyFact("bla bla Unobtainium is 67 Coins");
      _mockInputLineFactory.Setup(iif => iif.CreateFact(It.IsAny<string>()))
                    .Returns(mockFact);

      // Act
      _service.ParseInputText(mockData).Facts.ToList();

      // Assert
      Assert.AreEqual("bla bla", mockFact.X);
      Assert.AreEqual("Unobtainium", mockFact.Item);
      Assert.AreEqual("67", mockFact.Y);
      Assert.AreEqual("Coins", mockFact.Unit);
    }

    [Test]
    [TestCase("Query 1 has cool stuff ?", false)]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase("How much is Query 1 ?", true)]
    [TestCase("How many things is bla bla Goats ?", true)]
    [TestCase("How much stuff do I need to buy ?", false)]
    public void ShouldMarkQueriesAsAppropriatelyValid_WhenParseInputTextInvokedWithDifferingQueryText(string data, bool expected)
    {
      // Arrange
      var mockData = "Query 1 ?" + Environment.NewLine;
      var mockQuery = new DummyQuery(data);
      _mockInputLineFactory.Setup(iif => iif.CreateQuery(It.IsAny<string>()))
                    .Returns(mockQuery);

      // Act
      _service.ParseInputText(mockData).Queries.ToList();

      // Assert
      Assert.AreEqual(expected, mockQuery.IsValid);
    }

    [Test]
    public void ShouldSetAmountOnQueries_WhenParseInputTextInvokedWithValidQuery()
    {
      // Arrange
      var mockData = "Query 1 ?" + Environment.NewLine;
      var mockQuery = new DummyQuery("How much is the universe ?");
      _mockInputLineFactory.Setup(iif => iif.CreateQuery(It.IsAny<string>()))
                    .Returns(mockQuery);

      // Act
      _service.ParseInputText(mockData).Queries.ToList();

      // Assert
      Assert.AreEqual("the universe", mockQuery.Amount);
    }

    [Test]
    public void ShouldSetIsIntegerLookupOnQueries_WhenParseInputTextInvokedWithIntegerQuestion()
    {
      // Arrange
      var mockData = "Query 1 ?" + Environment.NewLine;
      var mockQuery = new DummyQuery("How much is god ?");
      _mockInputLineFactory.Setup(iif => iif.CreateQuery(It.IsAny<string>()))
                    .Returns(mockQuery);

      // Act
      _service.ParseInputText(mockData).Queries.ToList();

      // Assert
      Assert.IsTrue(mockQuery.IsIntegerLookup);
    }

    [Test]
    public void ShouldSetQueryDetails_WhenParseInputTextInvokedWithCommodityQuery()
    {
      // Arrange
      var mockData = "how many Donkeys is fob fob bla Unobtainium ?" + Environment.NewLine;
      var mockQuery = new DummyQuery("how many Donkeys is fob fob bla Unobtainium ?");
      _mockInputLineFactory.Setup(iif => iif.CreateQuery(It.IsAny<string>()))
                    .Returns(mockQuery);

      // Act
      _service.ParseInputText(mockData).Queries.ToList();

      // Assert
      Assert.AreEqual("fob fob bla", mockQuery.Amount);
      Assert.AreEqual("Unobtainium", mockQuery.Item);
      Assert.AreEqual("Donkeys", mockQuery.Unit);
      Assert.AreEqual("how many", mockQuery.Question);
      Assert.IsFalse(mockQuery.IsIntegerLookup);
    }
  }
}
