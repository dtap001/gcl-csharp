using System;
using System.Collections.Generic;
using System.Linq;
using God.CLI.Common.Services.Console;

namespace God.CLI.Common {
  public class DinamicalListFilter {

    IConsoleIO consoleIO;

    public DinamicalListFilter(IConsoleIO consoleIO) {
      this.consoleIO = consoleIO;
    }

    List<SelectionItem> _options;

    public string Filter(List<string> content) {
      _options =
         content
             .Select(x =>
                 new SelectionItem() { IsSelectedCurrently = false, Value = x })
             .ToList();
      
      string filter = string.Empty;
      var filteredList = new List<SelectionItem>(_options);
      RefreshStatus(ConsoleKey.Spacebar, filteredList, filter);

      ConsoleKeyInfo keyinfo;
      do {
        keyinfo = consoleIO.ReadKey();

        if (keyinfo.Key == ConsoleKey.Backspace) {
          filter =
              filter.Length > 0
                  ? filter.Remove(filter.Length - 1, 1)
                  : "";
        }
        else if (
            keyinfo.Key != ConsoleKey.UpArrow &&
            keyinfo.Key != ConsoleKey.DownArrow
        ) {
          filter += keyinfo.KeyChar;
        }

        filteredList =
            _options.Where(x => x.Value.Contains(filter)).ToList();
        EnsureHasChecked(filteredList);

        if (keyinfo.Key == ConsoleKey.UpArrow) {
          MoveSelectedUp(filteredList);
        }
        else if (keyinfo.Key == ConsoleKey.DownArrow) {
          MoveSelectedDown(filteredList);
        }

        RefreshStatus(keyinfo.Key, filteredList, filter);
      }
      while (keyinfo.Key != ConsoleKey.Enter);

      return filteredList.First().Value;
    }

    private void MoveSelectedUp(List<SelectionItem> options) {
      var selected =
          options.Where(x => x.IsSelectedCurrently).ToList().FirstOrDefault();
      var index = options.IndexOf(selected);

      selected.IsSelectedCurrently = false;

      int indexToChange = index - 1;
      if (index == 0) {
        indexToChange = options.Count - 1;
      }

      options[indexToChange].IsSelectedCurrently = true;
    }

    private void MoveSelectedDown(List<SelectionItem> options) {
      var selected =
          options.Where(x => x.IsSelectedCurrently).ToList().FirstOrDefault();
      var index = options.IndexOf(selected);

      selected.IsSelected = false;

      int indexToChange = index + 1;
      if (index == options.Count - 1) {
        indexToChange = 0;
      }

      options[indexToChange].IsSelectedCurrently = true;
    }

    private void EnsureHasChecked(List<SelectionItem> options) {
      var selected = options.Where(x => x.IsSelectedCurrently).ToList();

      if (selected.Count == 1) {
        return;
      }
      else if (selected.Count > 1) {
        var index = options.IndexOf(selected.First());
        options[index].IsSelected = false;
        return;
      }
      options.First().IsSelectedCurrently = true;
    }

    private void RefreshStatus(
        ConsoleKey lastKey,
        List<SelectionItem> options,
        string currentFilter
    ) {
      if (lastKey != ConsoleKey.UpArrow && lastKey != ConsoleKey.DownArrow) {
        consoleIO.Clear();
        System
          .Console
          .WriteLine(string
              .Join('\n', options.Select(x => x.ToString())));
        consoleIO.WriteLine($"================================");
        consoleIO.Write($":{currentFilter}");
      }
      else {
        var maxHeight = System.Console.GetCursorPosition().Top;
        var sliceCount = options.Count / maxHeight;
        
      }
    }
  }

 
}
