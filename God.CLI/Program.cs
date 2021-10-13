using System;
using System.Threading.Tasks;

namespace God.CLI {
  class Program {
    static async Task Main(string[] args) {
      await God.CLI.ConsoleFramework.Factory.GetInstance().Process(args);
    }
  }
}
