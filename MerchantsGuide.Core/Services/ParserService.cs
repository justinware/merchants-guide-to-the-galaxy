using System;
using System.Linq;
using JustinWare.MerchantsGuide.Core.Contracts;
using JustinWare.MerchantsGuide.Core.Models;

namespace JustinWare.MerchantsGuide.Core.Services
{
  public class ParserService : IParserService
  {
    private readonly IInputLineFactory _inputLineFactory;

    private readonly string[] _inputSeparators = new[]
                                  {
                                    Constants.Input.EquationSeparator,
                                    Constants.Input.EquationSeparator.ToUpper()
                                  };

    public ParserService(IInputLineFactory inputLineFactory)
    {
      _inputLineFactory = inputLineFactory;
    }

    public IInputSet ParseInputText(string input)
    {
      var result = new InputSet();
      
      var lines = input.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

      result.Facts = lines.Where(l => !l.EndsWith(Constants.Input.QuerySuffix))
                    .Select(CreateFact);

      result.Queries = lines.Where(l => l.EndsWith(Constants.Input.QuerySuffix))
                     .Select(CreateQuery);
      
      return result;
    }

    private IFact CreateFact(string factText)
    {
      var fact = _inputLineFactory.CreateFact(factText);

      // TODO: Should the parsing and interpreting of fact text be a responsibility of this class or the factory 
      //     or fact class itself ?? Not sure, revise this later !!

      if (string.IsNullOrEmpty(fact.OriginalText))
      {
        fact.IsValid = false;
        return fact;
      }

      var parts = fact.OriginalText.Split(_inputSeparators, StringSplitOptions.RemoveEmptyEntries);
      fact.IsValid = parts.Length == 2;

      if (fact.IsValid)
      {
        fact.X = parts[0].Trim();
        fact.Y = parts[1].Trim();

        var yParts = fact.Y.Split(new[] { Constants.Input.WordSeparator }, StringSplitOptions.RemoveEmptyEntries);
        if (yParts.Length == 1)
        {
          fact.IsRomanNumeralEquality = true;
        }
        else
        {
          fact.Y = yParts[0];
          fact.Unit = yParts[1];

          var xParts = fact.X.Split(new[] { Constants.Input.WordSeparator }, StringSplitOptions.RemoveEmptyEntries);
          fact.Item = xParts.Last();
          fact.X = fact.X.Replace(fact.Item, String.Empty).Trim();
        }
      }

      // TODO: Perform final validation here checking that Roman Numerals, Commodities & Currencies are valid as per known data.
      //     Possibly using IDataValidationService or similar

      return fact;
    }

    private IQuery CreateQuery(string queryText)
    {
      var query = _inputLineFactory.CreateQuery(queryText);

      // TODO: Should the parsing and interpreting of query text be a responsibility of this class or the factory 
      //     or Query class itself ?? Not sure, revise this later !!

      if (string.IsNullOrEmpty(query.OriginalText))
      {
        query.IsValid = false;
        return query;
      }

      var parts = query.OriginalText.Split(_inputSeparators, StringSplitOptions.RemoveEmptyEntries);
      query.IsValid = (parts.Length == 2 && (query.OriginalText.ToLowerInvariant().StartsWith(Constants.Input.QueryPrefix1)
                              || query.OriginalText.ToLowerInvariant().StartsWith(Constants.Input.QueryPrefix2)));

      if (query.IsValid)
      {
        query.Question = parts[0].Trim();
        query.Amount = parts[1].Replace(Constants.Input.QuerySuffix, String.Empty).Trim();

        if (query.Question.ToLowerInvariant() == Constants.Input.QueryPrefix2)
        {
          query.IsIntegerLookup = true;
        }
        else
        {
          var questionParts = query.Question.Split(new[] { Constants.Input.WordSeparator }, StringSplitOptions.RemoveEmptyEntries);
          query.Unit = questionParts.Last();
          query.Question = query.Question.Replace(query.Unit, String.Empty).Trim();

          var amountParts = query.Amount.Split(new[] { Constants.Input.WordSeparator }, StringSplitOptions.RemoveEmptyEntries);
          query.Item = amountParts.Last();
          query.Amount = query.Amount.Replace(query.Item, String.Empty).Trim();
        }
      }

      // TODO: Perform final validation here checking that Commodities & Currencies are valid as per known data.
      //     Possibly using IDataValidationService or similar

      return query;
    }
  }
}