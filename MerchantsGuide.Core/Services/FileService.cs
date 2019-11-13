using System.IO;
using JustinWare.MerchantsGuide.Core.Contracts;
using JustinWare.MerchantsGuide.Core.Models;

namespace JustinWare.MerchantsGuide.Core.Services
{
  public class FileService : IFileService
  {
    private string _inputFilePath;
    
    public bool CheckForInputFile()
    {
      // TODO: For simplicity, just get the input file from the (current) runtime directorty. Ideally would have the
      //     user input / choose themselves or provide as an argument to the app. But in the interest of time,
      //     this solution is deemed sufficent. Revise and evolve this later !!!
      _inputFilePath = string.Concat(Directory.GetCurrentDirectory(), "\\", Constants.Input.InputFileName);

      return File.Exists(_inputFilePath);
    }

    public string ReadInputFile()
    {
      return File.ReadAllText(_inputFilePath);
    }
  }
}