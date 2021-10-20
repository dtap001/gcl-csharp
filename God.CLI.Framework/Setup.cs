using System;
using Autofac;
using God.CLI.Common.Services.Console;
using God.CLI.Framework.Services.FileSystem;
namespace God.CLI.Framework {
  public class Setup {
    public static ContainerBuilder RegisterServices<T>(ContainerBuilder builder) {
      builder.RegisterType<T>();
      builder.RegisterType<FileSystemService>().As<IFileSystemService>();
      builder.RegisterType<DefaultConsoleIO>().As<IConsoleIO>();
      return builder;
    }
  }
}
