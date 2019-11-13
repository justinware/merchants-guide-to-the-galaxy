using System.Collections.Generic;

namespace JustinWare.MerchantsGuide.Core.Contracts
{
  public interface IInputSet
  {
    IEnumerable<IFact> Facts { get; set; }
    IEnumerable<IQuery> Queries { get; set; } 
  }
}
