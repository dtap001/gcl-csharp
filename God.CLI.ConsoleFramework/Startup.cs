using System;
using System.Threading.Tasks;
using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;

namespace God.CLI.ConsoleFramework {
  public class Startup {
    public async Task Process(string[] args) {
      await Host
          .CreateDefaultBuilder()
          .RunConsoleAppFrameworkAsync(args);
    }
  }
}
