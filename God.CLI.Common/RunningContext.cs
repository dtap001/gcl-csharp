namespace God.CLI.Common {
  public class RunningContext {

    public string CurrentWorkingDir {
      get {
        return System.Environment.CurrentDirectory;
      }
    }
    public string GCLBinaryPath {
      get {
        string folder = GCLFolderPath;
        string file = "God.CLI.exe";
        string result = System.IO.Path.Combine(folder, file);
        return $"{result}";
      }
    }
    public string GCLFolderPath {
      get {
        string folder = System.AppDomain.CurrentDomain.BaseDirectory;
        return $"{folder}";
      }
    }

    public string GCLConfigPath {
      get {
        string folder = GCLFolderPath;
        string file = "config.json";
        string result = System.IO.Path.Combine(folder, file);
        return $"{result}";
      }
    }
  }
}