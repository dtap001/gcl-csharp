using God.CLI.Common.Services.Console;
using System;
using System.Collections.Generic;

namespace God.CLI.Tests {
  public class MockConsole : IConsoleIO {
    private List<ConsoleKeyInfo> readKeys;
    private string output = string.Empty;
    private int lastReadKeyIndex = 0;
    private int height;

    public MockConsole(List<ConsoleKeyInfo> readKeys, int height) {
      this.readKeys = readKeys;
      this.height = height;
    }

    public void Clear() {
      this.output = string.Empty;
    }

    public int GetHeight() {
      return height;
    }

    public ConsoleKeyInfo ReadKey() {
      var result = readKeys[lastReadKeyIndex];
      lastReadKeyIndex = lastReadKeyIndex + 1;
      return result;
    }

    public string ReadLine() {
      throw new NotImplementedException();
    }

    public void Write(string text) {
      throw new NotImplementedException();
    }

    public void WriteLine(string text) {
      this.output += text + "\n";
    }

    public void WriteToRow(int rowIndex, string text) {
      var outputList = output.Split('\n');
      outputList[rowIndex] = text;
      output = string.Join('\n', outputList);

    }
  }
}