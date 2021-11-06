using CliWrap;
using System.Text;
using System.Threading.Tasks;
using God.CLI.Domain;
using God.CLI.Common;
using System.Collections.Generic;
using System.Linq;
using System;
using God.CLI.Common.Services.Console;
using CliWrap.Buffered;
namespace God.CLI.Framework {
  public class CliWrapSystemCommandExecutor : ISystemCommandExecutor {
    IConsoleIO console;
    public CliWrapSystemCommandExecutor(IConsoleIO consoleIO) {
      this.console = consoleIO;
    }
    public async Task<CommandExecutionResult> Exec(RunningContext context, string executable) {
      return await this.Exec(context, executable, new List<CommandArgument>());
    }
    public async Task<CommandExecutionResult> Exec(RunningContext context, string executable, List<CommandArgument> arguments) {
      CommandExecutionResult result = null;
      try {
        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();
        var argumentString = string.Join(" ", arguments.Select((arguments) => arguments.ToString()).ToArray());

        console.WriteLine($"CWD: {context.CurrentWorkingDir} EXE: {executable} ARGS: {argumentString}");
        var execResult = await Cli.Wrap(executable)
                 .WithArguments(argumentString)
                 .WithWorkingDirectory(context.CurrentWorkingDir)
                 .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
                 .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
                 .WithValidation(CommandResultValidation.None)
                 .ExecuteBufferedAsync();

        var stdOut = stdOutBuffer.ToString();
        var stdErr = stdErrBuffer.ToString();
        result = new CommandExecutionResult() {
          ExecutionDuration = execResult.RunTime,
          IsOK = execResult.ExitCode == 0 ? true : false,
          STDErr = stdErr,
          STDOut = stdOut
        };
        System.Console.WriteLine("Finished execution");
      }
      catch (Exception e) {
        result = new CommandExecutionResult() {
          IsOK = false,
          STDErr = e.Message,
        };
      }
      return result;
    }
  }
}