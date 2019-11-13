using JustinWare.MerchantsGuide.Core.Contracts;

namespace JustinWare.MerchantsGuide.Core.Models
{
  public class InputLine : IInputLine
  {
    public InputLine(string originalText)
    {
      OriginalText = originalText;
    }

    public string OriginalText { get; private set; }
    public bool IsValid { get; set; }
  }
}