using JustinWare.MerchantsGuide.Core.Contracts;

namespace JustinWare.MerchantsGuide.Core.Tests.Dummies
{
  public class DummyQuery : IQuery
  {
    public DummyQuery(string text)
    {
      OriginalText = text;
    }

    public string OriginalText { get; private set; }
    public bool IsValid { get; set; }
    public string Unit { get; set; }
    public string Question { get; set; }
    public string Amount { get; set; }
    public string Item { get; set; }
    public bool IsIntegerLookup { get; set; }
  }
}
