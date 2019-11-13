using System.Linq;
using JustinWare.MerchantsGuide.Core.Contracts;
using JustinWare.MerchantsGuide.Core.Extensions;
using JustinWare.MerchantsGuide.Core.Models;

namespace JustinWare.MerchantsGuide.Core.Services
{
  public class QueryService : IQueryService
  {
    private readonly IFactRepository _factRepository;

    public QueryService(IFactRepository factRepository)
    {
      _factRepository = factRepository;
    }

    public string ProcessQuery(IQuery query)
    {
      if (!query.IsValid)
      {
        return Constants.Output.InvalidQueryText;
      }

      return query.IsIntegerLookup ? GetIntegerLookupText(query) : GetCommodityLookupText(query);
    }

    private string GetIntegerLookupText(IQuery query)
    {
      var value = GetIntegerValueFromAmount(query.Amount);

      return string.Format(Constants.Output.IntegerLookupText, query.Amount, value);
    }

    private string GetCommodityLookupText(IQuery query)
    {
      var queryAmount = GetIntegerValueFromAmount(query.Amount);

      var matchingFact =
        _factRepository.Facts.SingleOrDefault(f => f.IsValid 
                                     && !f.IsRomanNumeralEquality 
                                     && (f.Unit.ToLowerInvariant() == query.Unit.ToLowerInvariant())
                                     && (f.Item.ToLowerInvariant() == query.Item.ToLowerInvariant()));

      if (matchingFact == null)
      {
        return string.Format(Constants.Output.InsufficientFactData, query.OriginalText);
      }

      var factQuantity = GetIntegerValueFromAmount(matchingFact.X);
      var factPrice = int.Parse(matchingFact.Y);

      var result = (queryAmount * factPrice) / factQuantity;

      return string.Format(Constants.Output.CommodityLookupText, query.Amount, query.Item, result, query.Unit);
    }

    private int GetIntegerValueFromAmount(string amount)
    {
      amount = amount.ToLowerInvariant();

      // TODO: Can this be done more elegantly with LINQ (i.e. no foreach) ?? Revise later !!
      _factRepository.Facts.Where(f => f.IsValid && f.IsRomanNumeralEquality)
                    .ForEach(f => { amount = amount.Replace(f.X.ToLowerInvariant(), f.Y); });

      // TODO: This is not ideal (actually creating an instance like this using a static method). Look to probably do this
      //     with IRomanNumeralFactory or something so is more testable. I mean it is being tested, but just double
      //     tested...as we have already tested the RomanNumeral class individually. But in the interest of time and simplicity,
      //     will leave like this for now. Revise later !!!
      var romanNumeral = RomanNumeral.CreateFromString(amount.Replace(Constants.Input.WordSeparator, string.Empty));

      return romanNumeral.GetIntegerValue();
    }
  }
}