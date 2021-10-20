using God.CLI.Common;
using God.CLI.Common.Services.Console;
using System.Collections.Generic;
using Xunit;

namespace God.CLI.Tests {
  public class DinamicalListFilterTest {
    [Fact]
    public void TestNormalFilter() {
      var console = new NullConsole();
      var filter = new DinamicalListFilter(console, new SelectionCanvas(console));
      filter.Filter(new List<string>() { "asd", "bash", "cast" });
    }

  }
}