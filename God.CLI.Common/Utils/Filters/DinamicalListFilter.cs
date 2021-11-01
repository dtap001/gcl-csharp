using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using God.CLI.Common.Services.Console;
using God.CLI.Domain;

namespace God.CLI.Common {
  public enum Change {
    DOWN,
    UP,
    OTHER
  }
  public class DinamicalListFilter {
    IConsoleIO console;

    public DinamicalListFilter(IConsoleIO console) {
      this.console = console;
    }
    public T Filter<T>(List<T> content) where T : IToStringable {
      var manager = new SelectionManager<T>(content);
      var change = Change.OTHER;
      var canvas = new SelectionCanvas<T>(console);

      canvas.Drawn(manager.Items, manager.Filter, change);

      ConsoleKeyInfo keyinfo;
      do {
        keyinfo = console.ReadKey();

        if (keyinfo.Key == ConsoleKey.Backspace) {
          manager.BackspaceFilter();
          manager.FilterNow();
          canvas.Reset();
        }
        else if (keyinfo.Key == ConsoleKey.UpArrow) {
          manager.MoveSelectionUp();
          change = Change.UP;
        }
        else if (keyinfo.Key == ConsoleKey.DownArrow) {
          manager.MoveSelectionDown();
          change = Change.DOWN;
        }
        else if (IsFilterChar(keyinfo.KeyChar + "")) {
          manager.AddToFilter(keyinfo.KeyChar);
          manager.FilterNow();
          canvas.Reset();
        }
        canvas.Drawn(manager.Items, manager.Filter, change);
      }
      while (keyinfo.Key != ConsoleKey.Enter);
      console.Clear();
      return manager.Items.Where(item => item.IsSelectedCurrently).ToList().First().OriginalContent;
    }

    private bool IsFilterChar(string input) {
      return Regex.IsMatch(input, @"^[a-zA-Z0-9_]+$");
    }
  }
}
