using System;
using Autofac;
using God.CLI.Framework.Services.FileSystem;
namespace God.CLI.Framework {
  public class Setup {
    public static IContainer Get<T>() {
      var builder = new ContainerBuilder();
      builder.RegisterType<T>();
      builder.RegisterType<FileSystemService>().As<IFileSystemService>();
      return builder.Build();
    }

    public static ContainerBuilder RegisterServices<T>(ContainerBuilder builder) {
      builder.RegisterType<T>();
      builder.RegisterType<FileSystemService>().As<IFileSystemService>();
      return builder;
    }
  }
}
