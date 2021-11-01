using System;

namespace God.CLI.Domain {

  public class GCLCommandInfo : IToStringable {
    public string Command { get; set; }
    public string Description { get; set; }
    public string ParentCommand { get; set; }
    public Type ClassType { get; set; }
    public string MethodName { get; set; }

    public override string ToString() {
      string commandInfoResult = string.Empty;
      if (!string.IsNullOrEmpty(ParentCommand)) {
        commandInfoResult += $"{ParentCommand} ";
      }

      commandInfoResult += Command;

      if (!string.IsNullOrEmpty(Description)) {
        commandInfoResult += $"  # {Description}";
      }
      return commandInfoResult;
    }
  }
}