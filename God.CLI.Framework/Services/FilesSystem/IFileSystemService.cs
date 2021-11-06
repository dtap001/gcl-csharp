using God.CLI.Domain;

namespace God.CLI.Framework.Services.FileSystem {
  public interface IFileSystemService {
    void ListAndSelect(OSPath currentPath);
  }
}