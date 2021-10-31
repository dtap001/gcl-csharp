using God.CLI.Common;
using System;
using System.Threading.Tasks;

namespace God.CLI.Framework.Services.Setup {
  public class WinSetupService : ISetupService {
    RunningContext context;
    UserInteractor interactor;
    public WinSetupService(RunningContext context, UserInteractor interactor) {
      this.context = context;
      this.interactor = interactor;
    }

    public async Task RegisterExecutable() {
      var answer = interactor.AskForPermissionToContinue($"Do you want to add {context.GCLFolderPath} to your PATH variable?");
      if (answer == Domain.UserSaid.NO) {
        await Task.CompletedTask;
        return;
      }
      interactor.Inform("Started to register");
      try {
        var name = "PATH";
        var scope = EnvironmentVariableTarget.User;
        var oldValue = Environment.GetEnvironmentVariable(name, scope);
        if (oldValue.Contains(context.GCLFolderPath)) {
          interactor.Inform("Already registerd!");
          await Task.CompletedTask;
          return;
        }
        var newValue = $"{oldValue};{context.GCLFolderPath}";
        Environment.SetEnvironmentVariable(name, newValue, scope);
        interactor.Inform("Registered");
      }
      catch (Exception e) {
        interactor.Fatal(e);
      }

      await Task.CompletedTask;
      return;
    }
  }
}