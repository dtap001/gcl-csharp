using God.CLI.Common;
using God.CLI.Common.Services.Console;
using God.CLI.Domain;
using System.Linq;
namespace God.CLI.Framework.Services.FileSystem {
  public class FileSystemService : IFileSystemService {

    public void ListFolderContent(OSPath currentPath) {
      var filter = new DinamicalListFilter(new DefaultConsoleIO());
      var files = FileListUtil.ListFilesInFolder(currentPath);
      filter.Filter(files.Select(x => x.ToString()).ToList());
    }
  }
}