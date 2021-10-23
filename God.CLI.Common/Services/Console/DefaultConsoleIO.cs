using System;
using System.Text.RegularExpressions;

namespace God.CLI.Common.Services.Console {
  public class DefaultConsoleIO : IConsoleIO {
    public void Clear() {
      System.Console.Clear();
    }

    public int GetHeight() {
      return System.Console.WindowHeight;
    }

    public string ReadLine() {
      return System.Console.ReadLine();
    }

    public void Write(string text) {
      System.Console.ForegroundColor = ConsoleColor.White;
      System.Console.Write(text);
    }
    public void WriteHighlight(string text) {
      System.Console.ForegroundColor = ConsoleColor.DarkYellow;
      System.Console.Write(text);
    }

    public void WriteLine(string text) {
      System.Console.WriteLine(text);
    }

    public void WriteToRow(int rowIndex, string text) {
      System.Console.CursorVisible = false;
      System.Console.SetCursorPosition(0, rowIndex);
      System.Console.Write(text);
      System.Console.SetCursorPosition(0, GetHeight() - 1);
      System.Console.CursorVisible = true;
    }

    ConsoleKeyInfo IConsoleIO.ReadKey() {
      return System.Console.ReadKey();
    }
  }
}