using God.CLI.Common.Services.Console;
using System.Collections.Generic;
using System.Linq;

namespace God.CLI.Common {
  public class SelectionCanvas {
    private const int BOTTOM_PLACEHOLDER_HEIGHT = 2;
    private bool isInit = true;
    private IConsoleIO console;
    private List<SelectionItem> items;
    private int previouslyDrawnChunkIndex = -1;
    public SelectionCanvas(IConsoleIO console, List<SelectionItem> items) {
      this.console = console;
      this.items = items;
    }

    public void Drawn() {
      var containerHeight = console.GetHeight();
      var chunkSize = (containerHeight - BOTTOM_PLACEHOLDER_HEIGHT);
      var chunkCount = items.Count / chunkSize;
      var currentSelectedItemIndex = items.IndexOf(items.Where(x => x.IsSelectedCurrently).ToList().First());
      var previouslySelectedItemIndex = items.IndexOf(items.Where(x => x.IsSelectedPreviously).ToList().First());

      var currentSelectedItemChunkStartIndex = 0;
      for (int currentChunkStartIndex = 0; currentChunkStartIndex >= items.Count; currentChunkStartIndex = currentChunkStartIndex + chunkSize) {
        if (currentSelectedItemIndex > currentChunkStartIndex) {
          continue;
        }
        currentSelectedItemChunkStartIndex = currentChunkStartIndex;
        break;
      }

      var currentSelectedItemChunkEndIndex = currentSelectedItemChunkStartIndex + chunkSize;
      var itemsForCurrentChunk = items.GetRange(currentSelectedItemChunkStartIndex, chunkSize);
      var selectedItemIndexInChunk = itemsForCurrentChunk.IndexOf(itemsForCurrentChunk.Where(x => x.IsSelectedCurrently).ToList().First());

      var isMovingDown = items[currentSelectedItemIndex - 1].IsSelectedCurrently != items[currentSelectedItemIndex - 1].IsSelectedPreviously;
      var isMovingUp = items[currentSelectedItemIndex + 1].IsSelectedCurrently != items[currentSelectedItemIndex + 1].IsSelectedPreviously;

      var isOnChunkStartBorder = currentSelectedItemIndex == currentSelectedItemChunkStartIndex;
      var isOnChunkEndBorder = currentSelectedItemIndex == currentSelectedItemChunkEndIndex;

      var needFullRedrawn = false;
      if (isOnChunkStartBorder && isMovingDown) {
        needFullRedrawn = true;
      }

      if (isOnChunkEndBorder && isMovingUp) {
        needFullRedrawn = true;
      }

      if (isInit) {
        isInit = false;
        needFullRedrawn = true;
      }

      if (needFullRedrawn) {
        Redrawn(itemsForCurrentChunk);
      }

      if (isMovingUp) {
        RedrawnAt(itemsForCurrentChunk, selectedItemIndexInChunk, selectedItemIndexInChunk--);
      }
      if (isMovingDown) {
        RedrawnAt(itemsForCurrentChunk, selectedItemIndexInChunk, selectedItemIndexInChunk++);
      }
    }

    private void Redrawn(List<SelectionItem> items) {
      foreach (var item in items) {
        console.WriteLine(GetSelectionItemRepresentation(item));
      }
    }

    private void RedrawnAt(List<SelectionItem> items, int newIndex, int previousIndex) {
      console.WriteToRow(newIndex, GetSelectionItemRepresentation(items[newIndex]));
      console.WriteToRow(newIndex, GetSelectionItemRepresentation(items[previousIndex]));
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