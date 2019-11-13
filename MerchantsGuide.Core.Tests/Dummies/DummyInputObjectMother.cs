using JustinWare.MerchantsGuide.Core.Contracts;

namespace JustinWare.MerchantsGuide.Core.Tests.Dummies
{
  public static class DummyInputObjectMother
  {
    public static IFact CreateFact(string x, string y)
    {
      return new DummyFact("irrelevant")
      {
        X = x,
        Y = y,
        IsRomanNumeralEquality = true,
        IsValid = true
      };
    }

    public static IFact CreateFact(string x, string y, string item, string unit)
    {
      return new DummyFact("irrelevant")
      {
        X = x,
        Y = y,
        IsRomanNumeralEquality = false,
        Item = item,
        Unit = unit,
        IsValid = true
      };
    }

    public static IQuery CreateQuery(string amount, string unit, string item)
    {
      return new DummyQuery("irrelevant")
      {
        IsIntegerLookup = false,
        IsValid = true,
        Amount = amount,
        Unit = unit,
        Item = item
      };
    }
  }
}
