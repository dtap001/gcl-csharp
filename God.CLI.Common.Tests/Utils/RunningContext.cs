using System;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using God.CLI.Common.Services.Console;

namespace God.CLI.Common.Tests {
  public class RunningContextTest {
    [Fact]
    public async Task Test1() {
      var context = new RunningContext();
      string result = context.GCLBinaryPath;
    }
  }
}
