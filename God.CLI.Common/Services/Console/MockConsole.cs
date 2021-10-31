using God.CLI.Common;
using God.CLI.Common.Services.Console;
using System;
using System.Collections.Generic;
using System.Linq;

namespace God.CLI.Common.Services.Console {
  public class MockConsole : IConsoleIO {
    private List<ConsoleKeyInfo> readKeys;
    private string output = string.Empty;
    private int lastReadKeyIndex = 0;
    private int height;

    public MockConsole(List<ConsoleKeyInfo> readKeys, int height) {
      this.readKeys = readKeys;
      this.height = height;
    }
    public string Output {
      get {
        return output.TrimEnd('\n');
      }
    }

    public void Clear() {
      this.output = string.Empty;
    }

    public int GetHeight() {
      return height;
    }

    public ConsoleKeyInfo ReadKey() {
      if (lastReadKeyIndex == readKeys.Count) {
        return new ConsoleKeyInfo(' ', ConsoleKey.Enter, false, false, false);
      }
      var result = readKeys[lastReadKeyIndex];
      lastReadKeyIndex = lastReadKeyIndex + 1;
      return result;
    }

    public string ReadLine() {
      throw new NotImplementedException();
    }

    public void Write(string text) {
      output += text;
    }

    public void WriteHighlight(string text) {
      output += $"#{text}#";

    }

    public void WriteLine(string text) {
      this.output += text + "\n";
    }

    public void WriteToRow(int rowIndex, string text) {
      var outputList = output.Split('\n').ToList();
      if (rowIndex > outputList.Count - 1) {
        int diff = rowIndex - (outputList.Count - 1);
        foreach (var i in Enumerable.Range(0, diff)) {
          outputList.Add("");
        }
      }

      outputList[rowIndex] = text;
      output = string.Join('\n', outputList);

    }
  }
}