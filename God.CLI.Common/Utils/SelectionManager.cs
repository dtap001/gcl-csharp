using System.Collections.Generic;
using System.Linq;

namespace God.CLI.Common {
  public class SelectionManager {

    private List<SelectionItem> items;
    private List<SelectionItem> originalItems;

    public List<SelectionItem> Items { get { return items; } }
    public string Filter { get { return filter; } }
    private string filter;
    public SelectionManager(List<string> list) {
      items =
       list
           .Select(x =>
               new SelectionItem() { IsSelectedCurrently = false, IsSelectedPreviously = false, Value = x })
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

      foreach (var item in items) {
        item.IsSelectedPreviously = false;
        item.IsSelectedCurrently = false;
      }
      this.items = originalItems.Where(x => x.Value.Contains(filter)).ToList();
      if (items.Where(i => i.IsSelectedCurrently).ToList().Count == 0) {
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