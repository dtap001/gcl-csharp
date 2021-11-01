using System;

namespace God.CLI.Domain {
  public class CommandExecutionResult {
    public bool IsOK { get; set; }
    public TimeSpan ExecutionDuration { get; set; }
    public string STDOut { get; set; }
    public string STDErr { get; set; }

    public override string ToString() {
      return $"IsOK: {IsOK}\nSTDOut: {STDOut}\nSTDErr: {STDErr}\nExecution Time: {ExecutionDuration}";
    }
  }
}