using God.CLI.Common;
using God.CLI.Domain;
using Newtonsoft.Json;
using System;

namespace God.CLI.Framework.Services.Configuration {
  public class ConfigurationService : IConfigurationService {
    RunningContext context;
    public ConfigurationService(RunningContext context) {
      this.context = context;
    }
    public GCLConfiguration Load() {
      var configFile = ReadConfigFile(context.GCLConfigPath);
      GCLConfiguration gclConfig;
      if (!string.IsNullOrEmpty(configFile)) {
        gclConfig = (GCLConfiguration)JsonConvert.DeserializeObject(configFile, typeof(GCLConfiguration), new JsonSerializerSettings() {
        });
      }
      else {
        gclConfig = new GCLConfiguration();
      }
      return gclConfig;
    }

    public void Save(GCLConfiguration configuration) {
      var configString = JsonConvert.SerializeObject(configuration);
      System.IO.File.WriteAllText(context.GCLConfigPath, configString);
    }

    private string ReadConfigFile(string path) {
      string result = string.Empty;
      try {
        result = System.IO.File.ReadAllText(path);
      }
      catch (Exception e) {
      }
      return result;
    }
  }
}