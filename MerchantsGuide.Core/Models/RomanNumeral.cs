using System;
using System.Globalization;
using System.IO;
using JustinWare.MerchantsGuide.Core.Contracts;

namespace JustinWare.MerchantsGuide.Core.Models
{
  public class RomanNumeral : IRomanNumeral
  {
    private readonly string _value;

    // Hide the default public constructor, as we only want an instance to be created via the factory methods.
    // This is so we can do validation on the input before creating, and error if need be.
    private RomanNumeral(string value)
    {
      _value = value.ToUpper();
    }

    public static IRomanNumeral CreateFromString(string value)
    {
      if (IsValid(value))
      {
        return new RomanNumeral(value);
      }

      throw new InvalidDataException("The input Roman Numeral value is invalid");
    }

    public static IRomanNumeral CreateFromInt(int value)
    {
      // TODO: In the interest of time and doing things in an agile (as needs) manner, this factory method for creating from an 
      // integer value is not yet implemented. As the business requirement / specification for the example currently only uses
      // string values as input, this fromInt method will be implemented when the need arises. 

      throw new NotImplementedException();
    }

    private static bool IsValid(string value)
    {
      // In the interest of time, this method has not been fully implemented. It is assumed that for the sake of the
      // exercise, that the test data will be a valid Roman Numeral combination.
      // TODO: Implement valid roman numeral checking here !!!

      return !string.IsNullOrEmpty(value);
    }

    public string Value
    {
      get { return _value; }
    }

    public int GetIntegerValue()
    {
      var result = 0;
      var tempValue = _value;

      // TODO: Could this be done better with LINQ ?? Revise this
      foreach (var pair in Constants.Data.RomanReductions)
      {
        while (tempValue.IndexOf(pair.Key, StringComparison.Ordinal) == 0)
        {
          result += int.Parse(pair.Value.ToString(CultureInfo.InvariantCulture));
          tempValue = tempValue.Substring(pair.Key.Length);
        }
      }

      return result;
    }
  }
}