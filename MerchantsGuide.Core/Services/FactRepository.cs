using System.Collections.Generic;
using JustinWare.MerchantsGuide.Core.Contracts;

namespace JustinWare.MerchantsGuide.Core.Services
{
  // NOTE: As this class is intended to be a state bucket / store, it needs to be registered as a singleton in
  //     any IoC container.
  public class FactRepository : IFactRepository
  {
    private readonly IList<IFact> _facts;
    
    public FactRepository()
    {
      _facts = new List<IFact>();
    }
    
    public IEnumerable<IFact> Facts
    {
      get { return _facts; }
    }
    
    public void Add(IFact fact)
    {
      _facts.Add(fact);
    }
  }
}