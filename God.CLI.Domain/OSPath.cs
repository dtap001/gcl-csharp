using System;

namespace God.CLI.Domain {
  public class OSPath : IToStringable {
    private string _path;

    public OSPath(string path) {
      this._path = path;
    }

    public override string ToString() {
      return this._path;
    }
  }
}
