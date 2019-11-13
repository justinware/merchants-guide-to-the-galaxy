using System.Collections.Generic;

namespace JustinWare.MerchantsGuide.Core.Contracts
{
  public interface IView
  {
    void WaitForUserResponse();
    void WriteToOutput(string text);
    void WriteLineToOutput(string text);
    void WriteLinesToOutput(IEnumerable<string> lines);
  }
}
