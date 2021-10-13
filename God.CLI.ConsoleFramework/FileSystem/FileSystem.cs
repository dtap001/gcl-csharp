using System.Collections.Generic;
using System.Linq;
using ConsoleAppFramework;
using God.CLI.Common;
using God.CLI.Domain;

namespace God.CLI.ConsoleFramework.FileSystem {
  [Command("fs", "list fucker")]
  public class FleSystemCommand : ConsoleAppBase {
    [Command("ls", "list fucker")]
    public void ListContent() {
      var content =
          FileListUtil
              .ListFilesInFolder(new OSPath("C:\\bluetooth-reconnect"));
      var list = content.Select(item => item.ToString()).ToList();
      new DinamicalListFilter(list).Filter();
    }
  }
}
