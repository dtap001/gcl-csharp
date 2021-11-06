using System.Collections.Generic;

namespace God.CLI.Domain {
  public class GCLConfiguration {
    public string Version { get; init; } = "1.0";
    public List<string> FavoriteFolders { get; init; } = new List<string>();
  }
}