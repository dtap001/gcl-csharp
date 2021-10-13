using System;
using System.Collections.Generic;
using System.Linq;

namespace God.CLI.Common {
  public class DinamicalListFilter {
    List<SelectionOption> _options;

    public DinamicalListFilter(List<string> content) {
      _options =
          content
              .Select(x =>
                  new SelectionOption() { IsSelected = false, Value = x })
              .ToList();
      _options.First().IsSelected = true;
    }

    public string Filter() {
      string filter = string.Empty;
      var filteredList = new List<SelectionOption>(_options);
      RefreshStatus(filteredList, filter);

      ConsoleKeyInfo keyinfo;
      do {
        keyinfo = Console.ReadKey();

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

        RefreshStatus(filteredList, filter);
      }
      while (keyinfo.Key != ConsoleKey.Enter);

      return filteredList.First().Value;
    }

    private void MoveSelectedUp(List<SelectionOption> options) {
      var selected =
          options.Where(x => x.IsSelected).ToList().FirstOrDefault();
      var index = options.IndexOf(selected);

      selected.IsSelected = false;

      int indexToChange = index - 1;
      if (index == 0) {
        indexToChange = options.Count - 1;
      }

      options[indexToChange].IsSelected = true;
    }

    private void MoveSelectedDown(List<SelectionOption> options) {
      var selected =
          options.Where(x => x.IsSelected).ToList().FirstOrDefault();
      var index = options.IndexOf(selected);

      selected.IsSelected = false;

      int indexToChange = index + 1;
      if (index == options.Count - 1) {
        indexToChange = 0;
      }

      options[indexToChange].IsSelected = true;
    }

    private void EnsureHasChecked(List<SelectionOption> options) {
      var selected = options.Where(x => x.IsSelected).ToList();

      if (selected.Count == 1) {
        return;
      }
      else if (selected.Count > 1) {
        var index = options.IndexOf(selected.First());
        options[index].IsSelected = false;
        return;
      }
      options.First().IsSelected = true;
    }

    private void RefreshStatus(
        List<SelectionOption> options,
        string currentFilter
    ) {
      System.Console.Clear();
      System
          .Console
          .WriteLine(string
              .Join('\n', options.Select(x => x.ToString())));
      System.Console.WriteLine("================================\n");
      System.Console.Write($":{currentFilter}");
    }
  }

  class SelectionOption {
    public bool IsSelected { get; set; }

    public string Value { get; set; }

    public override string ToString() {
      var result = string.Empty;
      if (IsSelected) {
        result += "[*] ";
      }
      else {
        result += "[ ] ";
      }
      result += Value;
      return result;
    }
  }
}
