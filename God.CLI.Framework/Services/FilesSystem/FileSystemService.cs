using God.CLI.Common;
using God.CLI.Common.Services.Console;
using God.CLI.Domain;
using System.Linq;
using System;

namespace God.CLI.Framework.Services.FileSystem {
  public class FileSystemService : IFileSystemService {
    IConsoleIO consoleIO;
    ISystemCommandExecutor executor;
    RunningContext context;
    public FileSystemService(IConsoleIO consoleIO, ISystemCommandExecutor executor, RunningContext context) {
      this.consoleIO = consoleIO;
      this.executor = executor;
      this.context = context;
    }

    public async void ListAndSelect(OSPath currentPath) {
      var filter = new DinamicalListFilter(consoleIO);
      var files = FileListUtil.ListFilesInFolder(currentPath);
      var selected = filter.Filter<OSPath>(files);


    }
  }

}