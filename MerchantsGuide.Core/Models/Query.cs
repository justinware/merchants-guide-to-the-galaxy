/*

ThoughtWorks - Coding Assignment
Problem Three: Merchant's Guide to the Galaxy

10/03/2014
Justin Ware
wareisjustin@gmail.com

*/
using JustinWare.MerchantsGuide.Core.Contracts;

namespace JustinWare.MerchantsGuide.Core.Models
{
  public class Query : InputLine, IQuery
  {
    public Query(string originalText) : base(originalText)
    {
    }

    public string Unit { get; set; }
    public string Question { get; set; }
    public string Amount { get; set; }
    public string Item { get; set; }
    public bool IsIntegerLookup { get; set; }
  }
}