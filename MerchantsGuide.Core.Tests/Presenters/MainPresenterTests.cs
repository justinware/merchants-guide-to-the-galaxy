using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using JustinWare.MerchantsGuide.Core.Contracts;
using JustinWare.MerchantsGuide.Core.Models;
using JustinWare.MerchantsGuide.Core.Presenters;

namespace JustinWare.MerchantsGuide.Core.Tests.Presenters
{
  [TestFixture]
  public class MainPresenterTests
  {
    private Mock<IView> _mockView;
    private Mock<IFileService> _mockFileService;
    private Mock<IParserService> _mockParserService;
    private Mock<IFactRepository> _mockFactRepository;
    private Mock<IQueryService> _mockQueryService;

    private string _mockData;
    private Mock<IInputSet> _mockInputSet;
    private Mock<IFact> _mockFact1;
    private Mock<IFact> _mockFact2;
    private Mock<IQuery> _mockQuery1;
    private Mock<IQuery> _mockQuery2;

    private IPresenter _presenter;

    [SetUp]
    public void Setup_Scenario()
    {
      _mockView = new Mock<IView>();
      _mockFileService = new Mock<IFileService>();
      _mockParserService = new Mock<IParserService>();
      _mockFactRepository = new Mock<IFactRepository>();
      _mockQueryService = new Mock<IQueryService>();

      _mockData = "Some mock data";
      _mockFact1 = new Mock<IFact>();
      _mockFact2 = new Mock<IFact>();
      _mockQuery1 = new Mock<IQuery>();
      _mockQuery2 = new Mock<IQuery>();
      _mockInputSet = new Mock<IInputSet>();

      _mockFileService.Setup(fs => fs.CheckForInputFile()).Returns(true);
      _mockFileService.Setup(fs => fs.ReadInputFile()).Returns(_mockData);

      _mockFact1.Setup(f => f.IsValid).Returns(true);
      _mockFact2.Setup(f => f.OriginalText).Returns("Some fact 2");
      _mockFact2.Setup(f => f.IsValid).Returns(false);
      _mockInputSet.Setup(i => i.Facts).Returns(new List<IFact> { _mockFact1.Object, _mockFact2.Object });
      _mockInputSet.Setup(i => i.Queries).Returns(new List<IQuery> { _mockQuery1.Object, _mockQuery2.Object });

      _mockParserService.Setup(ps => ps.ParseInputText(_mockData)).Returns(_mockInputSet.Object);
      _mockQueryService.Setup(qs => qs.ProcessQuery(It.IsAny<IQuery>())).Returns("Some query result");

      _presenter = new MainPresenter(_mockView.Object,
                    _mockFileService.Object,
                    _mockParserService.Object,
                    _mockFactRepository.Object,
                    _mockQueryService.Object);
    }

    [Test]
    public void ShouldWaitForUserToBeReady_WhenInitialiseInvoked()
    {
      // Act
      _presenter.Initialise();

      // Assert
      _mockView.Verify(v => v.WriteToOutput(Constants.Output.EnsureInputFileText), Times.Once);
      _mockView.Verify(v => v.WaitForUserResponse(), Times.AtLeastOnce);
    }

    [Test]
    public void ShouldCacheFactsInFactRepository_WhenInitialiseInvoked()
    {
      // Act
      _presenter.Initialise();

      // Assert
      _mockFactRepository.Verify(fr => fr.Add(It.IsAny<IFact>()), Times.Exactly(2));
    }

    [Test]
    public void ShouldOutputInvalidFacts_WhenInitialiseInvoked()
    {
      // Arrange
      var expected = string.Format(Constants.Output.InvalidFactText, "Some fact 2");

      // Act
      _presenter.Initialise();

      // Assert
      _mockView.Verify(v => v.WriteLinesToOutput(It.Is<IEnumerable<string>>(s => s.Count() == 1 &&
                                        s.First() == expected)), Times.Once);
    }

    [Test]
    public void ShouldProcessQueriesAndOutputResults_WhenInitialiseInvoked()
    {
      // Act
      _presenter.Initialise();

      // Assert
      _mockView.Verify(v => v.WriteLinesToOutput(It.Is<IEnumerable<string>>(s => s.Count() == 2 &&
                                        s.All(s1 => s1 == "Some query result"))), Times.Once);
    }

    [Test]
    public void ShouldWaitForUserToAcknowledgeBeforeExiting_WhenInitialiseInvoked()
    {
      // Act
      _presenter.Initialise();

      // Assert
      _mockView.Verify(v => v.WaitForUserResponse(), Times.Exactly(2));
    }
  }
}
