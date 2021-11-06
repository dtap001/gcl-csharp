using System;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using God.CLI.Common.Services.Console;
using God.CLI.Domain;
using God.CLI.Framework.Services.Configuration;

namespace God.CLI.Framework.Tests {
  public class ConfigurationServiceTest {
    [Fact]
    public async Task Test1() {
      var service = new ConfigurationService(new Common.RunningContext());
      var config = service.Load();
      config.FavoriteFolders.Add("test");
      service.Save(config);
      var config2 = service.Load();
       
    }
  }
}
