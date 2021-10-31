using System;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using God.CLI.Common.Services.Console;

namespace God.CLI.Framework.Tests {
  public class SystemCommandExecutor {
    [Fact]
    public async Task Test1() {
      string valami = "asda";
      CliWrapSystemCommandExecutor executor = new CliWrapSystemCommandExecutor(new MockConsole(new List<ConsoleKeyInfo>(), 10));
      valami = "asdasd";
      var result = await executor.Exec(new Common.RunningContext(), "cmd.exe", new List<Domain.CommandArgument>(){new Domain.CommandArgument(){
          Key= "set",
          Value=  $"x=y"
      }});
    }
  }
}
