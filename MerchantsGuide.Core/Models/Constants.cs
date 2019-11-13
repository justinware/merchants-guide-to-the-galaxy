using System.Collections.Generic;

namespace JustinWare.MerchantsGuide.Core.Models
{
  public static class Constants
  {
    public static class Input
    {
      public const string EquationSeparator = " is ";
      public const string WordSeparator = " ";
      public const string QueryPrefix1 = "how many";
      public const string QueryPrefix2 = "how much";
      public const string QuerySuffix = "?";
      public const string InputFileName = "input.txt";
    }

    public static class Output
    {
      public const string EnsureInputFileText = "Ensure input file (input.txt) is in runtime directory and please enter when ready...";
      public const string InvalidFactText = "ERROR: Input fact '{0}' is not valid input";
      public const string InvalidQueryText = "I have no idea what you are talking about";
      public const string IntegerLookupText = "{0} is {1}";
      public const string CommodityLookupText = "{0} {1} is {2} {3}";
      public const string InsufficientFactData = "ERROR: Insufficient fact data to answer query '{0}'";
      public const string InputFileNotFound = "\r\nERROR: Input file not found\r\n";
      public const string PressEnterToExit = "\r\nPress enter to exit...";
      public const string ExceptionFriendlyMessage =
        "\r\nOops...something bad has happened :( Whilst this is terribly embarrassing, here are the details none the less:\r\n";
    }

    public static class Data
    {
      public static IDictionary<string, int> RomanReductions = new Dictionary<string, int>
        {
          { "M", 1000 }, { "CM", 900 }, { "D", 500 }, { "CD", 400 },
          { "C", 100 }, { "XC", 90 }, { "L", 50 }, { "XL", 40 },
          { "X", 10 }, { "IX", 9 }, { "V", 5 }, { "IV", 4 }, { "I", 1 }
        };
    }
  }
}
