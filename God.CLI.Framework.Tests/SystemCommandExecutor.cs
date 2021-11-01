using System;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using God.CLI.Common.Services.Console;
using God.CLI.Domain;

namespace God.CLI.Framework.Tests {
  public class SystemCommandExecutor {
    [Fact]
    public async Task Test1() {
      string valami = "asda";
      CliWrapSystemCommandExecutor executor = new CliWrapSystemCommandExecutor(new MockConsole(new List<ConsoleKeyInfo>(), 10));
      
      var context = new Common.RunningContext();
      var result = await executor.Exec(context, context.GCLBinaryPath, new List<CommandArgument>() { new CommandArgument() { Key = "", Value = "hello" } });

    }
  }
}
