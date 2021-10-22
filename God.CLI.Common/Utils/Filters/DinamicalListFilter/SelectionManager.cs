using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace God.CLI.Common {
  public class SelectionManager {

    private List<SelectionItem> items;
    private List<SelectionItem> originalItems;

    public List<SelectionItem> Items { get { return items; } }
    public string Filter { get { return filter; } }
    private string filter = string.Empty;
    public SelectionManager(List<string> list) {
      items =
       list
           .Select(x =>
               new SelectionItem(x) { IsSelectedCurrently = false, IsSelectedPreviously = false })
           .ToList();
      originalItems = new List<SelectionItem>(items);
      items.First().IsSelectedCurrently = true;
    }
    public void Reset() {
      this.items = originalItems;
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
      if (this.items.Count == 0) { return; }
      this.items = new List<SelectionItem>();

      foreach (var item in originalItems) {
        if (!item.Value.ToLower().Contains(filter.ToLower())) {
          continue;
        }

        var splittedValue = Regex.Split(item.Value.ToLower(), $"({filter.ToLower()})");
        var newSelectionItemParts = new List<ValuePart>();
        foreach (var value in splittedValue) {
          if (value.ToLower().Contains(filter)) {
            newSelectionItemParts.Add(new HighlightableValuePart() { Value = value });
            continue;
          }
          newSelectionItemParts.Add(new ValuePart() { Value = value });
          item.SetValue(newSelectionItemParts);
        }
        this.items.Add(item);
      }

      bool hasItems = items.Count > 0;
      bool isNothingSelected = hasItems && items.Where(i => i.IsSelectedCurrently).ToList().Count == 0;
      if (isNothingSelected) {
        items.First().IsSelectedCurrently = true;
      }
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