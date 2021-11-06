using System.Collections.Generic;
using System.Linq;
using ConsoleAppFramework;
using God.CLI.Common;
using God.CLI.Common.Services.Console;
using God.CLI.Domain;
using God.CLI.Framework;
using God.CLI.Framework.Services.FileSystem;

namespace God.CLI.Console.Commands {
  [Command("fs")]
  public class FleSystemCommand : ConsoleAppBase {
    IFileSystemService fileSystemService;
    RunningContext context;

    public FleSystemCommand(RunningContext context, IFileSystemService fileSystemService) {
      this.fileSystemService = fileSystemService;
      this.context = context;
    }

    [Command("ls", "list folder content")]
    public void ListFolderContent() {
      fileSystemService.ListAndSelect(new OSPath(context.CurrentWorkingDir));
    }
  }
}
