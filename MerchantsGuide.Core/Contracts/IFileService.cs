namespace JustinWare.MerchantsGuide.Core.Contracts
{
  public interface IFileService
  {
    bool CheckForInputFile();
    string ReadInputFile();
  }
}