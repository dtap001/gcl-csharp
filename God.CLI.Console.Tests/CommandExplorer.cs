using System;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using God.CLI.Common.Services.Console;
using God.CLI.Console.Utils;
namespace God.CLI.Common.Tests {
  public class CommandExplorerTest {
    [Fact]
    public async Task Test1() {
      CommandExplorer.GetAll();
    }
  }
}
