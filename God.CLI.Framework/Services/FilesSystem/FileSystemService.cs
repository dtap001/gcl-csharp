using God.CLI.Common;
using God.CLI.Common.Services.Console;
using God.CLI.Domain;
using System.Linq;
namespace God.CLI.Framework.Services.FileSystem {
  public class FileSystemService : IFileSystemService {
    IConsoleIO consoleIO;

    public FileSystemService(IConsoleIO consoleIO) {
      this.consoleIO = consoleIO;
    }

    public void ListFolderContent(OSPath currentPath) {
      var filter = new DinamicalListFilter(consoleIO);
      var files = FileListUtil.ListFilesInFolder(currentPath);
      var selected = filter.Filter<OSPath>(files);
    }
  }
}