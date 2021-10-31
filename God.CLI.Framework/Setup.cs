using System;
using Autofac;
using God.CLI.Common;
using God.CLI.Common.Services.Console;
using God.CLI.Framework.Services.FileSystem;
using God.CLI.Framework.Services.Setup;

namespace God.CLI.Framework {
  public class Setup {
    public static ContainerBuilder RegisterServices<T>(ContainerBuilder builder) {
      builder.RegisterType<T>();
      builder.RegisterType<RunningContext>();
      builder.RegisterType<UserInteractor>();
      builder.RegisterType<CliWrapSystemCommandExecutor>().As<ISystemCommandExecutor>();
      builder.RegisterType<DefaultConsoleIO>().As<IConsoleIO>();

      builder.RegisterType<FileSystemService>().As<IFileSystemService>();
      builder.RegisterType<WinSetupService>().As<ISetupService>();
      return builder;
    }
  }
}
