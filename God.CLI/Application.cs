using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace God.CLI {
  public class Application {
    public async Task Run(string[] args) {
      await Host
      .CreateDefaultBuilder()
       .UseServiceProviderFactory(new AutofacServiceProviderFactory())
      .ConfigureContainer<ContainerBuilder>(builder => {
        God.CLI.Framework.Setup.RegisterServices<Application>(builder);
      })
      .RunConsoleAppFrameworkAsync(args);
    }
  }
}