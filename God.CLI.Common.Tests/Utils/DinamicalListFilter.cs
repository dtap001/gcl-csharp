using God.CLI.Common;
using God.CLI.Common.Services.Console;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
namespace God.CLI.Tests {
  public class DinamicalListFilterTest {
   /* List<string> listToFilter;
    List<ConsoleKeyInfo> expectedReadChars;
    MockConsole console;

    public DinamicalListFilterTest() {
      listToFilter = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
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
      Assert.Equal(1, CountSelectedItems(console));
    }


    [Fact]
    public void TestDownToBottomThanUp() {
      expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.DownArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.UpArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.UpArrow, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.UpArrow, false,false, false),
      };

      console = new MockConsole(expectedReadChars, 5);
      var filter = new DinamicalListFilter(console);
      filter.Filter(listToFilter);
      Assert.Equal(1, CountSelectedItems(console));
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
      Assert.Equal(1, CountSelectedItems(console));
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
      Assert.Equal(1, CountSelectedItems(console));
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

      Assert.Equal(1, CountSelectedItems(console));
      Assert.Equal(2, CountSelectionItems(console));
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
      Assert.Equal(0, CountSelectedItems(console));
      Assert.Equal(0, CountSelectionItems(console));
    }
    [Fact]
    public void TestFilterToItemOnOtherChunk() {
      listToFilter = new List<string>() { "aaa", "bbbb", "ab" };
      expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('b', ConsoleKey.B, false,false, false),
      };

      console = new MockConsole(expectedReadChars, 4);
      var filter = new DinamicalListFilter(console);
      filter.Filter(listToFilter);
      Assert.Equal(1, CountSelectedItems(console));
      Assert.Equal(2, CountSelectionItems(console));
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
      Assert.Equal(1, CountSelectedItems(console));
      Assert.Equal(3, CountSelectionItems(console));
    }

    [Fact]
    public void TestDoubleBackspace() {
      listToFilter = new List<string>() { "aaa", "bbbb", "ab" };
      expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('b', ConsoleKey.Backspace, false,false, false),
        new ConsoleKeyInfo('b', ConsoleKey.Backspace, false,false, false),
      };

      console = new MockConsole(expectedReadChars, 6);
      var filter = new DinamicalListFilter(console);
      filter.Filter(listToFilter);
      Assert.Equal(1, CountSelectedItems(console));
      Assert.Equal(3, CountSelectionItems(console));
    }

    [Fact]
    public void TestMultipleFilterWithNoResult() {
      listToFilter = new List<string>() { "aaa", "bbbb", "ab" };
      expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('x', ConsoleKey.X, false,false, false),
        new ConsoleKeyInfo('x', ConsoleKey.X, false,false, false),
      };

      console = new MockConsole(expectedReadChars, 6);
      var filter = new DinamicalListFilter(console);
      filter.Filter(listToFilter);

      Assert.Equal(0, CountSelectedItems(console));
      Assert.Equal(0, CountSelectionItems(console));
    }

     [Fact]
    public void TestMultipleFilterWithNoResultThanBackspace() {
      listToFilter = new List<string>() { "aaa", "bbbb", "ab" };
      expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('a', ConsoleKey.A, false,false, false),
        new ConsoleKeyInfo('x', ConsoleKey.X, false,false, false),
        new ConsoleKeyInfo(' ', ConsoleKey.Backspace, false,false, false)       
      };

      console = new MockConsole(expectedReadChars, 6);
      var filter = new DinamicalListFilter(console);
      filter.Filter(listToFilter);

      Assert.Equal(1, CountSelectedItems(console));
      Assert.Equal(2, CountSelectionItems(console));
    }

    [Fact]
    public void TestMultiFilterMultiBackspace() {
      listToFilter = new List<string>() { "aasdb", "dddsdasd", "ssdadqwdawd","aasdvvasd","asdsssww","hhhh","kkkkk" };
      expectedReadChars = new List<ConsoleKeyInfo>() {
        new ConsoleKeyInfo('a', ConsoleKey.A, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.A, false,false, false),
        new ConsoleKeyInfo('x', ConsoleKey.Backspace, false,false, false),
        new ConsoleKeyInfo('x', ConsoleKey.Backspace, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.A, false,false, false),
        new ConsoleKeyInfo('a', ConsoleKey.Backspace, false,false, false),
      };

      console = new MockConsole(expectedReadChars, 15);
      var filter = new DinamicalListFilter(console);
      filter.Filter(listToFilter);

      Assert.Equal(1, CountSelectedItems(console));
      Assert.Equal(listToFilter.Count, CountSelectionItems(console));
    } 

    private int CountSelectionItems(MockConsole console) {
      return console.Output.Where(i => i == '[').Count();
    }
    private int CountSelectedItems(MockConsole console) {
      return console.Output.Where(i => i == '*').Count();
    }*/
  }
}