using System.Collections.Generic;

namespace JustinWare.MerchantsGuide.Core.Contracts
{
  public interface IFactRepository
  {
    IEnumerable<IFact> Facts { get; }
    void Add(IFact fact);
  }
}
