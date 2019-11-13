using JustinWare.MerchantsGuide.Core.Contracts;

namespace JustinWare.MerchantsGuide.Core.Tests.Dummies
{
  public class DummyFact : IFact
  {
    public DummyFact(string text)
    {
      OriginalText = text;
    }

    public string OriginalText { get; private set; }
    public bool IsValid { get; set; }
    public string X { get; set; }
    public string Y { get; set; }
    public string Unit { get; set; }
    public string Item { get; set; }
    public bool IsRomanNumeralEquality { get; set; }
  }
}
