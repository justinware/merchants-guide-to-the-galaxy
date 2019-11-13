namespace JustinWare.MerchantsGuide.Core.Contracts
{
  public interface IQuery : IInputLine
  {
    string Unit { get; set; }
    string Question { get; set; }
    string Amount { get; set; }
    string Item { get; set; }
    bool IsIntegerLookup { get; set; }
  }
}
