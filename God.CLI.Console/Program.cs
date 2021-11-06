using Autofac;
using System;
using System.Threading.Tasks;

namespace God.CLI.Console {
  class Program {
    static async Task Main(string[] args) {
      try {
        await new God.CLI.Console.Application().Run(args);
      }
      catch (Exception e) {
        System.Console.WriteLine($"Exception. Message: {e.Message} Trace: {e.StackTrace}");
        System.Console.ReadLine();
      }
    }
  }
}
