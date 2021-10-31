using God.CLI.Common;
using God.CLI.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace God.CLI.Framework {
  public interface ISystemCommandExecutor {
    Task<CommandExecutionResult> Exec(RunningContext context, string executable, List<CommandArgument> arguments);
  }
}