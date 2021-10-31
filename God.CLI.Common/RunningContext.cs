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
        string binary = "God.CLI.exe";
        string result = System.IO.Path.Combine(folder, binary);
        return $"{result}";
      }
    }
       public string GCLFolderPath {
      get {
        string folder = System.AppDomain.CurrentDomain.BaseDirectory;
        return $"{folder}";
      }
    }
  }
}