namespace JustinWare.MerchantsGuide.Core.Contracts
{
  public interface IInputLine
  {
    string OriginalText { get; }
    bool IsValid { get; set; }
  }
}
