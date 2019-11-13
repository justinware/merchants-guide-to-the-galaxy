using System.Linq;
using JustinWare.MerchantsGuide.Core.Contracts;
using JustinWare.MerchantsGuide.Core.Extensions;
using JustinWare.MerchantsGuide.Core.Models;

namespace JustinWare.MerchantsGuide.Core.Presenters
{
  public class MainPresenter : IPresenter
  {
    private readonly IView _view;
    private readonly IFileService _fileService;
    private readonly IParserService _parserService;
    private readonly IFactRepository _factRepository;
    private readonly IQueryService _queryService;
    
    public MainPresenter(IView view,
                  IFileService fileService,
                  IParserService parserService,
                  IFactRepository factRepository,
                  IQueryService queryService)
    {
      _view = view;
      _fileService = fileService;
      _parserService = parserService;
      _factRepository = factRepository;
      _queryService = queryService;
    }
    
    public void Initialise()
    {
      // Ensure input file is present and wait for user ready
      _view.WriteToOutput(Constants.Output.EnsureInputFileText);
      _view.WaitForUserResponse();
      while (!_fileService.CheckForInputFile())
      {
        _view.WriteLineToOutput(Constants.Output.InputFileNotFound);
        _view.WriteToOutput(Constants.Output.EnsureInputFileText);
        _view.WaitForUserResponse();
      }

      // Read input data from file and parse into data structures
      var rawInput = _fileService.ReadInputFile();
      var inputSet = _parserService.ParseInputText(rawInput);

      // Cache facts ready for query's
      inputSet.Facts.ForEach(f => _factRepository.Add(f));

      // Output error for invalid facts
      _view.WriteLineToOutput(string.Empty);
      _view.WriteLinesToOutput(inputSet.Facts.Where(f => !f.IsValid)
                                .Select(f => string.Format(Constants.Output.InvalidFactText, f.OriginalText)));

      // Process each query and output result
      _view.WriteLinesToOutput(inputSet.Queries.Select(q => _queryService.ProcessQuery(q)));

      // Wait for user OK before terminating
      _view.WaitForUserResponse();
    }
  }
}
