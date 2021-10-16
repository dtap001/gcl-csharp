using System;

namespace God.CLI.Common.Services.Console {
  public class DefaultConsoleIO : IConsoleIO {
    public void Clear() {
      System.Console.Clear();
    }

    public string ReadLine() {
      return System.Console.ReadLine();
    }

    public void Write(string text) {
      System.Console.Write(text);
    }

    public void WriteLine(string text) {
      System.Console.WriteLine(text);
    }

    ConsoleKeyInfo IConsoleIO.ReadKey() {
      return System.Console.ReadKey();
    }
  }
}