using JustinWare.MerchantsGuide.Core.Contracts;

namespace JustinWare.MerchantsGuide.Core.Models
{
  public class Fact : InputLine, IFact
  {
    public Fact(string originalText) : base(originalText)
    {
    }

    public string X { get; set; }
    public string Y { get; set; }
    public string Unit { get; set; }
    public string Item { get; set; }
    public bool IsRomanNumeralEquality { get; set; }
  }
}