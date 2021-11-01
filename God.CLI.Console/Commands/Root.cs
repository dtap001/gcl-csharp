using ConsoleAppFramework;
using God.CLI.Common;
using God.CLI.Common.Services.Console;
using God.CLI.Console.Utils;
using System.Collections.Generic;
using God.CLI.Framework;
using God.CLI.Domain;
using System.Threading.Tasks;
using Autofac;
namespace God.CLI.Console.Commands {
  public class RootCommand : ConsoleAppBase {
    IConsoleIO console;
    ISystemCommandExecutor executor;
    RunningContext context;
    IComponentContext autofacContext;
    public RootCommand(IComponentContext autofacContextX, IConsoleIO consoleIO, ISystemCommandExecutor executor, RunningContext context) {
      this.console = consoleIO;
      this.context = context;
      this.executor = executor;
      this.autofacContext = autofacContextX;
    }

    public async void Run() {
      var filter = new DinamicalListFilter(console);
      List<GCLCommandInfo> allCommands = CommandExplorer.GetAll();

      GCLCommandInfo selected = filter.Filter<GCLCommandInfo>(allCommands);
      var service = autofacContext.ResolveOptional(selected.ClassType);
      var methodInfo = selected.ClassType.GetMethod(selected.MethodName);
      console.Clear();
      methodInfo.Invoke(service, null);
    }

    [Command("hello")]
    public void Hello() {
      System.Console.WriteLine("bello");
    }
  }
}