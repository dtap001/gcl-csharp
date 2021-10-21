using God.CLI.Common.Services.Console;
using System.Collections.Generic;
using System.Linq;

namespace God.CLI.Common {
  public class SelectionCanvas {
    private const int BOTTOM_PLACEHOLDER_HEIGHT = 2;
    private bool isInit = true;
    private IConsoleIO console;
    public SelectionCanvas(IConsoleIO console) {
      this.console = console;
    }

    public void Drawn(List<SelectionItem> items, string currentFilter, Change change) {
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
      var isOnChunkEndBorder = currentSelectedItemIndex == currentSelectedItemChunkEndIndex;

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
        Redrawn(itemsForCurrentChunk);
      }
      else {
        RedrawnAt(itemsForCurrentChunk, selectedItemIndexInChunk, change);
      }
    }

    private void Redrawn(List<SelectionItem> items) {
      foreach (var item in items) {
        console.WriteLine(GetSelectionItemRepresentation(item));
      }
    }

    private void RedrawnAt(List<SelectionItem> items, int newIndex, Change change) {
      int previousIndex = newIndex;
      if (change == Change.UP) {
        previousIndex = newIndex + 1;
      }
      if (change == Change.DOWN) {
        previousIndex = newIndex - 1;
      }
      console.WriteToRow(newIndex, GetSelectionItemRepresentation(items[newIndex]));
      console.WriteToRow(previousIndex, GetSelectionItemRepresentation(items[previousIndex]));
    }

    public string GetSelectionItemRepresentation(SelectionItem item) {
      var result = string.Empty;
      if (item.IsSelectedCurrently) {
        result += "[*] ";
      }
      else {
        result += "[ ] ";
      }
      result += item.Value;
      return result;
    }
  }
}