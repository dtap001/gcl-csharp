using God.CLI.Common.Services.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace God.CLI.Common {
  public class SelectionCanvas {
    public const int BOTTOM_PLACEHOLDER_HEIGHT = 2;
    private bool isInit = true;
    private IConsoleIO console;
    public SelectionCanvas(IConsoleIO console) => this.console = console;

    public void Reset() {
      isInit = true;
    }

    public void Drawn(List<SelectionItem> items, string currentFilter, Change change) {
      DrawnList(items, change);
      DrawnFilterBar(currentFilter);
    }
    private void DrawnFilterBar(string currentFilter) {
      var containerHeight = console.GetHeight();
      console.WriteToRow(containerHeight - 2, "===================================");
      console.WriteToRow(containerHeight - 1, " => " + currentFilter);
    }

    private void DrawnList(List<SelectionItem> items, Change change) {
      if (items.Count == 0) {
        console.Clear();
        return;
      }
      var containerHeight = console.GetHeight();
      var chunkSize = (containerHeight - BOTTOM_PLACEHOLDER_HEIGHT);
      var chunkCount = items.Count / chunkSize;
      var currentSelectedItemIndex = items.IndexOf(items.Where(x => x.IsSelectedCurrently).ToList().First());

      var currentSelectedItemChunkStartIndex = 0;
      for (int currentChunkStartIndex = 0; currentChunkStartIndex <= items.Count; currentChunkStartIndex = currentChunkStartIndex + chunkSize) {
        if (currentSelectedItemIndex >= currentChunkStartIndex + chunkSize) {
          continue;
        }
        currentSelectedItemChunkStartIndex = currentChunkStartIndex;
        break;
      }

      var currentSelectedItemChunkEndIndex = currentSelectedItemChunkStartIndex + chunkSize;
      var itemCountForChunk = chunkSize;
      //we have fewer items then the current chunksize
      if (currentSelectedItemChunkEndIndex > items.Count - 1) {
        itemCountForChunk = (items.Count - currentSelectedItemChunkStartIndex);
      }

      var itemsForCurrentChunk = items.GetRange(currentSelectedItemChunkStartIndex, itemCountForChunk);
      var selectedItemIndexInChunk = itemsForCurrentChunk.IndexOf(itemsForCurrentChunk.Where(x => x.IsSelectedCurrently).ToList().First());

      var isOnChunkStartBorder = currentSelectedItemIndex == currentSelectedItemChunkStartIndex;
      var isOnChunkEndBorder = currentSelectedItemIndex == currentSelectedItemChunkEndIndex - 1;

      var needFullRedrawn = false;
      if (isOnChunkStartBorder && change == Change.DOWN) {
        needFullRedrawn = true;
      }

      if (isOnChunkEndBorder && change == Change.UP) {
        needFullRedrawn = true;
      }

      if (isInit) {
        isInit = false;
        needFullRedrawn = true;
      }

      if (needFullRedrawn) {
        RedrawnList(itemsForCurrentChunk);
      }
      else {
        RedrawnListAt(itemsForCurrentChunk, selectedItemIndexInChunk, change);
      }
    }

    private void RedrawnList(List<SelectionItem> items) {
      console.Clear();
      foreach (var item in items) {
        console.Write(GetSelectionItemRepresentation(item));
        foreach (var itemPart in item.GetValueParts()) {
          if (itemPart is SelectionHighlightedItemPart) {
            console.WriteHighlight(itemPart.Value);
            continue;
          }
          console.Write(itemPart.Value);
        }
        console.Write("\n");
      }
    }

    private void RedrawnListAt(List<SelectionItem> items, int newIndex, Change change) {
      int previousIndex = newIndex;
      if (change == Change.UP) {
        previousIndex = newIndex + 1;
      }
      if (change == Change.DOWN) {
        previousIndex = newIndex - 1;
      }
      console.WriteToRow(newIndex, GetSelectionItemRepresentation(items[newIndex]));
      if (previousIndex >= 0 && previousIndex <= items.Count - 1) {
        console.WriteToRow(previousIndex, GetSelectionItemRepresentation(items[previousIndex]));
      }
    }

    public string GetSelectionItemRepresentation(SelectionItem item) {
      var result = string.Empty;
      if (item.IsSelectedCurrently) {
        result += "[*] ";
      }
      else {
        result += "[ ] ";
      }
      return result;
    }
  }
}