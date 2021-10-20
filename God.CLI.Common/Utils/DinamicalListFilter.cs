using System;
using System.Collections.Generic;
using System.Linq;
using God.CLI.Common.Services.Console;

namespace God.CLI.Common {
  public class DinamicalListFilter {
    IConsoleIO console;
    SelectionCanvas canvas;

    public DinamicalListFilter(IConsoleIO consoleIO, SelectionCanvas canvas) {
      this.console = consoleIO;
      this.canvas = canvas;
    }
    public string Filter(List<string> content) {
      var manager = new SelectionManager(content);
      canvas.Drawn(manager.Items, manager.Filter);

      ConsoleKeyInfo keyinfo;
      do {
        keyinfo = console.ReadKey();

        if (keyinfo.Key == ConsoleKey.Backspace) {
          manager.BackspaceFilter();
        }
        else if (keyinfo.Key == ConsoleKey.UpArrow) {
          manager.MoveSelectionUp();
        }
        else if (keyinfo.Key == ConsoleKey.DownArrow) {
          manager.MoveSelectionDown();
        }
        else {
          manager.FilterNow(keyinfo.KeyChar);
        }
        canvas.Drawn(manager.Items, manager.Filter);
      }
      while (keyinfo.Key != ConsoleKey.Enter);

      return manager.Items.First().Value;
    }

    private void RefreshStatus(
        ConsoleKey lastKey,
        List<SelectionItem> options,
        string currentFilter
    ) {
      if (lastKey != ConsoleKey.UpArrow && lastKey != ConsoleKey.DownArrow) {
        console.Clear();
        System
          .Console
          .WriteLine(string
              .Join('\n', options.Select(x => x.ToString())));
        console.WriteLine($"================================");
        console.Write($":{currentFilter}");
      }
      else {
        var maxHeight = System.Console.GetCursorPosition().Top;
        var sliceCount = options.Count / maxHeight;

      }
    }
  }


}
