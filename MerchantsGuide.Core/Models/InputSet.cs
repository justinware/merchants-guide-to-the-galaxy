using System.Collections.Generic;
using JustinWare.MerchantsGuide.Core.Contracts;

namespace JustinWare.MerchantsGuide.Core.Models
{
  public class InputSet : IInputSet
  {
    public IEnumerable<IFact> Facts { get; set; }
    public IEnumerable<IQuery> Queries { get; set; }
  }
}