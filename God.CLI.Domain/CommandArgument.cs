namespace God.CLI.Domain {
  public class CommandArgument {
    public string Key { get; set; }
    public string Value { get; set; }

    public override string ToString() {
      return $"{Key} {Value}";
    }
  }
}