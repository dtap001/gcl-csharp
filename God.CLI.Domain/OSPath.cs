using System;

namespace God.CLI.Domain {
  public class OSPath {
    private string _path;

    public OSPath(string path) {
      this._path = path;
    }

    public override string ToString() {
      return this._path;
    }
  }
}
