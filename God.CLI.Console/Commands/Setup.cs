using System.Collections.Generic;
using System.Linq;
using ConsoleAppFramework;
using God.CLI.Common;
using God.CLI.Framework.Services.Setup;

namespace God.CLI.Console.Commands {
  [Command("setup")]
  public class SetupCommand : ConsoleAppBase {
    UserInteractor interactor;
    ISetupService service;

    public SetupCommand(ISetupService service, UserInteractor interactor) {
      this.service = service;
      this.interactor = interactor;
    }

    [Command("set", "add current folder the execution path")]
    public async void Setup() {
      await service.RegisterExecutable();
    }
    /*  [Command("this", "list fucker")]
      public void ListThis([Option(0)] string list) {
        var filter = new DinamicalListFilter(new DefaultConsoleIO());
        filter.Filter(new List<string>(list.Split('\n')));
      }*/
  }
}
