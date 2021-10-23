using System.Collections.Generic;
using System.Linq;

namespace God.CLI.Common {
  public class SelectionItem {
    public SelectionItem(string initialValue) {
      SetValue(initialValue);
    }
    public SelectionItem(List<SelectionItemPart> selectionItemPart) {
      this.itemParts = selectionItemPart;
    }
    private List<SelectionItemPart> itemParts = new List<SelectionItemPart>();
    public bool IsSelectedPreviously { get; set; }
    public bool IsSelectedCurrently { get; set; }

    public void SetValue(List<SelectionItemPart> values) {
      this.itemParts = values;
    }

    public void SetValue(string value) {
      this.itemParts = new List<SelectionItemPart>() { new SelectionItemPart() { Value = value } };
    }

    public List<SelectionItemPart> GetValueParts() {
      return itemParts;
    }

    public string Value {
      get {
        return string.Concat(itemParts.Select(item => item.Value));
      }
    }
  }

  public class SelectionItemPart {
    public string Value { get; set; }
  }
  public class SelectionHighlightedItemPart : SelectionItemPart { }
}