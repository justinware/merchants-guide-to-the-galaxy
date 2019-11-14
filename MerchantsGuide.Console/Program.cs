using System;
using JustinWare.MerchantsGuide.Core.Contracts;
using JustinWare.MerchantsGuide.Core.Models;
using JustinWare.MerchantsGuide.Core.Presenters;
using JustinWare.MerchantsGuide.Core.Services;

namespace JustinWare.MerchantsGuide.Console
{
   public class Program
   {
      static void Main(string[] args)
      {
         try
         {
            Bootstrap().Initialise();
         }
         catch (Exception ex)
         {
            System.Console.WriteLine(Constants.Output.ExceptionFriendlyMessage);
            System.Console.WriteLine(ex);
            System.Console.Write(Constants.Output.PressEnterToExit);
            System.Console.ReadLine();
         }
      }

      private static IPresenter Bootstrap()
      {
         // TODO: In the interest of time, this is blatantly "poor man's dependency injection" !!!! This is not ideal,
         //       not production code, and just done to bootstrap the app for the sake of the example. In a real world
         //       application this would be handled with an IoC container such as Ninject, Autofac, StructureMap, etc.
         //       This is only done here like this because I still used the principles of DI / coding to interfaces / Mocking, etc
         //       for the app design & testing, and the wire-up of dependencies is deemed a trivial IoC concern. So for the sake
         //       of the example, poor man's dependency injection will have to suffice...

         var factRepository = new FactRepository();

         return new MainPresenter(new ConsoleView(), 
                                  new FileService(),
                                  new ParserService(new InputLineFactory()),
                                  factRepository,
                                  new QueryService(factRepository));
      }
   }
}
