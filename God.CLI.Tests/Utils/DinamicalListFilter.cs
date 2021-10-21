using God.CLI.Common;
using God.CLI.Common.Services.Console;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
namespace God.CLI.Tests {
  public class DinamicalListFilterTest {
    List<string> listToFilter;
    List<ConsoleKeyInfo> expectedReadChars;
    MockConsole console;

    public DinamicalListFilterTest() {
      listToFilter = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", };
    }

    [Fact]
    public void TestDown() {
      expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
      };

      console = new MockConsole(expectedReadChars, 5);
      var filter = new DinamicalListFilter(console);
      filter.Filter(listToFilter);
      int selectedCount = console.Output.Where(c => c == '*').ToList().Count;
      Assert.Equal(1, selectedCount);
    }

    [Fact]
    public void TestUp() {
      expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('a', ConsoleKey.UpArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.UpArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.UpArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.UpArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.UpArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.UpArrow, false,false, false),
      };

      console = new MockConsole(expectedReadChars, 5);
      var filter = new DinamicalListFilter(console);
      filter.Filter(listToFilter);
      int selectedCount = console.Output.Where(c => c == '*').ToList().Count;
      Assert.Equal(1, selectedCount);
    }

    [Fact]
    public void TestUpDown() {
      expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('a', ConsoleKey.UpArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.UpArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.UpArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.UpArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
      };

      console = new MockConsole(expectedReadChars, 5);
      var filter = new DinamicalListFilter(console);
      filter.Filter(listToFilter);
      int selectedCount = console.Output.Where(c => c == '*').ToList().Count;
      Assert.Equal(1, selectedCount);
    }

    [Fact]
    public void TestFilter() {
      listToFilter = new List<string>() { "aaa", "bbbb", "ab" };
      expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('a', ConsoleKey.A, false,false, false),
      };

      console = new MockConsole(expectedReadChars, 5);
      var filter = new DinamicalListFilter(console);
      filter.Filter(listToFilter);

      int selectedCount = console.Output.Where(c => c == '*').ToList().Count;
      Assert.Equal(1, selectedCount);

      var outputList = console.Output.Split('\n').ToList();
      Assert.Equal(2, outputList.Count);
    }

    [Fact]
    public void TestFilterToNonIncludedItem() {
      listToFilter = new List<string>() { "aaa", "bbbb", "ab" };
      expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('x', ConsoleKey.X, false,false, false),
      };

      console = new MockConsole(expectedReadChars, 5);
      var filter = new DinamicalListFilter(console);
      filter.Filter(listToFilter);

      int selectedCount = console.Output.Where(c => c == '*').ToList().Count;
      Assert.Equal(0, selectedCount);

      var outputList = console.Output.Split('\n').ToList();
      Assert.Equal(0, outputList.Count);
    }

    [Fact]
    public void TestFilterToItemOnOtherChunk() {
      listToFilter = new List<string>() { "aaa", "bbbb", "ab" };
      expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('b', ConsoleKey.B, false,false, false),
      };

      console = new MockConsole(expectedReadChars, 3);
      var filter = new DinamicalListFilter(console);
      filter.Filter(listToFilter);

      int selectedCount = console.Output.Where(c => c == '*').ToList().Count;
      Assert.Equal(1, selectedCount);

      var outputList = console.Output.Split('\n').ToList();
      Assert.Equal(2, outputList.Count);
    }


      [Fact]
    public void TestFilterToItemThanBackspace() {
      listToFilter = new List<string>() { "aaa", "bbbb", "ab" };
      expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('b', ConsoleKey.B, false,false, false),
        new ConsoleKeyInfo('b', ConsoleKey.Backspace, false,false, false),
      };

      console = new MockConsole(expectedReadChars, 6);
      var filter = new DinamicalListFilter(console);
      filter.Filter(listToFilter);

      int selectedCount = console.Output.Where(c => c == '*').ToList().Count;
      Assert.Equal(1, selectedCount);

      var outputList = console.Output.Split('\n').ToList();
      Assert.Equal(3, outputList.Count);
    }

  }
}