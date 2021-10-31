using God.CLI.Common.Services.Console;
using God.CLI.Domain;
using System;

namespace God.CLI.Common {
  public class UserInteractor : IUserInteractor {
    IConsoleIO console;
    public UserInteractor(IConsoleIO console) {
      this.console = console;
    }

    public UserSaid AskForPermissionToContinue(string question) {
      console.WriteLine($"{question} (Type 'y' to continue.)");
      var key = console.ReadKey();
      console.WriteLine(string.Empty);
      if (key.KeyChar == 'y') {
        return UserSaid.YES;
      }
      return UserSaid.NO;
    }

    public void Inform(string msg) {
      console.WriteLine(msg);
    }

    public void Error(string msg) {
      console.WriteHighlight(msg);
      console.WriteLine(string.Empty);
    }
    public void Fatal(Exception exception) {
      console.WriteHighlight("FATAL error.");
      console.WriteLine($"Message: {exception.Message}");
      console.WriteLine($"Trace: {exception.StackTrace}");
    }
  }
}