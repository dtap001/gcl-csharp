using System.Collections.Generic;
using System.Linq;
using ConsoleAppFramework;
using God.CLI.Common;
using God.CLI.Domain;
using God.CLI.Framework.Services.FileSystem;

namespace God.CLI.ConsoleFramework.Commands {
  [Command("fs", "list fucker")]
  public class FleSystemCommand : ConsoleAppBase {
    IFileSystemService fileSystemService;

    public FleSystemCommand(IFileSystemService fileSystemService) {
      this.fileSystemService = fileSystemService;
    }

    [Command("ls", "list fucker")]
    public void ListFolderContent() {
      fileSystemService.ListFolderContent(new OSPath("C:\\Program Files"));
    }
  }
}
