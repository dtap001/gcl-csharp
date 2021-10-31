using ConsoleAppFramework;
using God.CLI.Common;
using God.CLI.Common.Services.Console;
using God.CLI.Console.Utils;
using System.Collections.Generic;

namespace God.CLI.Console.Commands {
  public class RootCommand : ConsoleAppBase {
    IConsoleIO console;

    public RootCommand(IConsoleIO consoleIO) {
      this.console = consoleIO;
    }

    public void Run() {
      var filter = new DinamicalListFilter(console);
      var allCommands = CommandExplorer.GetAll();

      var toFilter = new List<string>();
      foreach (var commandInfo in allCommands) {
        string commandInfoResult = string.Empty;
        if (!string.IsNullOrEmpty(commandInfo.ParentCommand)) {
          commandInfoResult += $"{commandInfo.ParentCommand} ";
        }

        commandInfoResult += commandInfo.Command;

        if (!string.IsNullOrEmpty(commandInfo.Description)) {
          commandInfoResult += $"  => {commandInfo.Description}";
        }
        toFilter.Add(commandInfoResult);
      }
      var selected = filter.Filter(toFilter);
      console.WriteLine("You have selected " + selected);

    }

    [Command("hello")]
    public void Hello() {
      System.Console.WriteLine("bello");
    }
  }
}