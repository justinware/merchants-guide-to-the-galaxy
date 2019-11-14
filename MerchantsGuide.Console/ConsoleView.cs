using System;
using System.Collections.Generic;
using JustinWare.MerchantsGuide.Core.Contracts;
using JustinWare.MerchantsGuide.Core.Extensions;

namespace JustinWare.MerchantsGuide.Console
{
   public class ConsoleView : IView
   {
      public void WaitForUserResponse()
      {
         System.Console.ReadLine();
      }

      public void WriteToOutput(string text)
      {
         System.Console.Write(text);
      }

      public void WriteLineToOutput(string text)
      {
         System.Console.WriteLine(text);
      }

      public void WriteLinesToOutput(IEnumerable<string> lines)
      {
         lines.ForEach(WriteLineToOutput);
      }
   }
}
