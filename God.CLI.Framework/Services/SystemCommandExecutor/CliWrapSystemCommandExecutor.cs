using CliWrap;
using System.Text;
using System.Threading.Tasks;
using God.CLI.Domain;
using God.CLI.Common;
using System.Collections.Generic;
using System.Linq;
using System;
using God.CLI.Common.Services.Console;

namespace God.CLI.Framework {
  public class CliWrapSystemCommandExecutor : ISystemCommandExecutor {
    IConsoleIO console;
    public CliWrapSystemCommandExecutor(IConsoleIO consoleIO) {
      this.console = consoleIO;
    }

    public async Task<CommandExecutionResult> Exec(RunningContext context, string executable, List<CommandArgument> arguments) {
      CommandExecutionResult result = null;
      try {
        console.WriteLine("asd");
        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();
        var argumentString = string.Join(" ", arguments.Select((arguments) => arguments.ToString()).ToArray());
        CommandResult execResult = null;

        execResult = await Cli.Wrap(executable)
                 .WithArguments(argumentString)
                 .WithWorkingDirectory(context.CurrentWorkingDir)
                 .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
                 .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
                 .ExecuteAsync();

        var stdOut = stdOutBuffer.ToString();
        var stdErr = stdErrBuffer.ToString();
        result = new CommandExecutionResult() {
          ExecutionDuration = execResult.RunTime,
          IsOK = execResult.ExitCode == 0 ? true : false,
          STDErr = stdErr,
          STDOut = stdOut
        };
      }
      catch (Exception e) {
        console.WriteLine("Exception " + e.Message);
      }
      return result;
    }
  }
}