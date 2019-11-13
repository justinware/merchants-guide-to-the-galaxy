namespace JustinWare.MerchantsGuide.Core.Contracts
{
  public interface IInputLineFactory
  {
    IFact CreateFact(string text);
    IQuery CreateQuery(string text);
  }
}
