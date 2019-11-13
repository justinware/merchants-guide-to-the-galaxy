namespace JustinWare.MerchantsGuide.Core.Contracts
{
  public interface IFact : IInputLine
  {
    string X { get; set; }
    string Y { get; set; }
    string Unit { get; set; }
    string Item { get; set; }
    bool IsRomanNumeralEquality { get; set; }
  }
}