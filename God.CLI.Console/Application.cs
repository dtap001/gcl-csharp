using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using God.CLI.Console.Commands;

namespace God.CLI.Console {
  public class Application {
    public async Task Run(string[] args) {
      await Host
      .CreateDefaultBuilder()
       .UseServiceProviderFactory(new AutofacServiceProviderFactory())
      .ConfigureContainer<ContainerBuilder>(builder => {
        God.CLI.Framework.Setup.RegisterServices<Application>(builder);
        builder.RegisterType<FleSystemCommand>();
        builder.RegisterType<SetupCommand>();
      })
      .RunConsoleAppFrameworkAsync<RootCommand>(args, new ConsoleAppOptions() {
        ShowDefaultCommand = true
      });
    }
  }
}