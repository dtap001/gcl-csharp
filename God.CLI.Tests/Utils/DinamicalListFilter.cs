using God.CLI.Common;
using God.CLI.Common.Services.Console;
using System;
using System.Collections.Generic;
using Xunit;

namespace God.CLI.Tests {
  public class DinamicalListFilterTest {
    [Fact]
    public void TestDown() {
      var expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
      };
      var textList = new List<string>() { "asd", "bash", "cast", "asd", "bash", "cast", "asd", "bash", "cast", "asd", "bash", "cast", "asd", "bash", "cast", "asd", "bash", "cast", "asd", "bash", "cast", "asd", "bash", "cast", };
      var console = new MockConsole(expectedReadChars, 5);
      var filter = new DinamicalListFilter(console, new SelectionCanvas(console));
      filter.Filter(textList);
    }

  }
}