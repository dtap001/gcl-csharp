using System;

namespace God.CLI.Common.Services.Console {
  public class NullConsole : IConsoleIO {
    public void Clear() {

    }

    public int GetHeight() {
      return 12;
    }

    public string ReadLine() {
      return "";
    }

    public void Write(string text) {

    }

    public void WriteLine(string text) {

    }

    public void WriteToRow(int rowIndex, string text) {

    }

    ConsoleKeyInfo IConsoleIO.ReadKey() {
      return new ConsoleKeyInfo();
    }
  }
}