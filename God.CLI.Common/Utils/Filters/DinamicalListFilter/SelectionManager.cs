using God.CLI.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace God.CLI.Common {
  public class SelectionManager<T> where T : IToStringable {
    private List<SelectionItem<T>> items;
    private List<T> originalItems;
    public List<SelectionItem<T>> Items { get { return items; } }
    public string Filter { get { return filter; } }
    private string filter = string.Empty;
    public SelectionManager(List<T> list) {
      this.originalItems = new List<T>(list);
      ResetToOriginalList();
    }
    public void ResetToOriginalList() {
      this.items =
      this.originalItems
          .Select(x =>
              new SelectionItem<T>(x) { IsSelectedCurrently = false, IsSelectedPreviously = false })
          .ToList();
      this.items.First().IsSelectedCurrently = true;
    }
    public void MoveSelectionUp() {
      var originalIndex = CurrentlySelectedIndex();
      if (originalIndex == 0) {
        return;
      }
      foreach (var item in items) {
        item.IsSelectedPreviously = false;
      }

      items[originalIndex].IsSelectedCurrently = false;
      items[originalIndex].IsSelectedPreviously = true;
      items[originalIndex - 1].IsSelectedCurrently = true;
    }

    public void MoveSelectionDown() {
      var originalIndex = CurrentlySelectedIndex();
      if (originalIndex == items.Count - 1) {
        return;
      }
      foreach (var item in items) {
        item.IsSelectedPreviously = false;
      }
      items[originalIndex].IsSelectedCurrently = false;
      items[originalIndex].IsSelectedPreviously = true;
      items[originalIndex + 1].IsSelectedCurrently = true;
    }

    public void AddToFilter(char newChar) {
      filter += newChar;
    }

    public void FilterNow() {
      if (filter.Length == 0) {
        ResetToOriginalList();
        return;
      }
      var newItems = new List<SelectionItem<T>>();

      foreach (var item in originalItems) {
        if (!Regex.Match(item.ToString(), $"({filter})", RegexOptions.IgnoreCase).Success) {
          continue;
        }

        var splittedValue = Regex.Split(item.ToString(), $"({filter})", RegexOptions.IgnoreCase);
        var newSelectionItemParts = new List<SelectionItemPart>();
        foreach (var value in splittedValue) {

          if (Regex.Match(value, $"({filter})", RegexOptions.IgnoreCase).Success) {
            newSelectionItemParts.Add(new SelectionHighlightedItemPart() { Value = value });
            continue;
          }
          newSelectionItemParts.Add(new SelectionItemPart() { Value = value });
        }

        newItems.Add(new SelectionItem<T>(newSelectionItemParts, item));
      }

      bool hasItems = newItems.Count > 0;
      bool isNothingSelected = hasItems && newItems.Where(i => i.IsSelectedCurrently).ToList().Count == 0;
      if (isNothingSelected) {
        newItems.First().IsSelectedCurrently = true;
      }
      this.items = newItems;
    }

    public void BackspaceFilter() {
      if (filter.Length > 0) {
        filter = filter.Remove(filter.Length - 1, 1);
      }
      else {
        filter = string.Empty;
      }
    }

    private int CurrentlySelectedIndex() {
      var selected =
        items.Where(x => x.IsSelectedCurrently).ToList().FirstOrDefault();
      return items.IndexOf(selected);
    }

  }
}