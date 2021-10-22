using System.Collections.Generic;
using System.Linq;

namespace God.CLI.Common {
  public class SelectionItem {
    public SelectionItem(string initialValue) {
      SetValue(initialValue);
    }
    private List<ValuePart> valueParts = new List<ValuePart>();
    public bool IsSelectedPreviously { get; set; }
    public bool IsSelectedCurrently { get; set; }

    public void SetValue(List<ValuePart> values) {
      this.valueParts = values;
    }

    public void SetValue(string value) {
      this.valueParts = new List<ValuePart>() { new ValuePart() { Value = value } };
    }

    public List<ValuePart> GetValueParts() {
      return valueParts;
    }

    public string Value {
      get {
        return string.Concat(valueParts.Select(item => item.Value));
      }
    }
  }

  public class ValuePart {
    public string Value { get; set; }
  }
  public class HighlightableValuePart : ValuePart { }
}