using God.CLI.Domain;
using System.Collections.Generic;
using System.Linq;

namespace God.CLI.Common {
  public class SelectionItem<T> where T : IToStringable {
    public SelectionItem(T initialValue) {
      OriginalContent = initialValue;
      SetValue(initialValue.ToString());
    }
    public SelectionItem(List<SelectionItemPart> selectionItemPart, IToStringable originalSourceItem) {
      this.itemParts = selectionItemPart;
    }
    private List<SelectionItemPart> itemParts = new List<SelectionItemPart>();
    public bool IsSelectedPreviously { get; set; }
    public bool IsSelectedCurrently { get; set; }
    public T OriginalContent { get; set; }

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