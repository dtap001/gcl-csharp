using System.Collections.Generic;
using System.Linq;
using ConsoleAppFramework;
using God.CLI.Common;
using God.CLI.Common.Services.Console;
using God.CLI.Domain;
using God.CLI.Framework.Services.FileSystem;

namespace God.CLI.Console.Commands {
  [Command("fs")]
  public class FleSystemCommand : ConsoleAppBase {
    IFileSystemService fileSystemService;

    public FleSystemCommand(IFileSystemService fileSystemService) {
      this.fileSystemService = fileSystemService;
    }


    [Command("ls", "list folder content")]
    public void ListFolderContent() {
      fileSystemService.ListFolderContent(new OSPath(new RunningContext().CurrentWorkingDir));
    }
    /*  [Command("this", "list fucker")]
      public void ListThis([Option(0)] string list) {
        var filter = new DinamicalListFilter(new DefaultConsoleIO());
        filter.Filter(new List<string>(list.Split('\n')));
      }*/
  }
}
