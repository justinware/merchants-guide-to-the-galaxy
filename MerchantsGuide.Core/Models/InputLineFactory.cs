using JustinWare.MerchantsGuide.Core.Contracts;

namespace JustinWare.MerchantsGuide.Core.Models
{
  public class InputLineFactory : IInputLineFactory
  {
    public IFact CreateFact(string text)
    {
      return new Fact(text);
    }

    public IQuery CreateQuery(string text)
    {
      return new Query(text);
    }
  }
}