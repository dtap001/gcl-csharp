using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using God.CLI.Domain;

namespace God.CLI.Common {
  public class FileListUtil {
    public static List<OSPath> ListFilesInFolder(OSPath path) {
      string[] fileEntries = Directory.GetFiles(path.ToString());
      string[] directories = Directory.GetDirectories(path.ToString());

      var result = new List<OSPath>();
      result.AddRange(directories.Select(item => new OSPath(item)));
      result.AddRange(fileEntries.Select(item => new OSPath(item)));
      return result;
    }
  }
}
