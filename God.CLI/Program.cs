using Autofac;
using System.Threading.Tasks;

namespace God.CLI {
  class Program {
    static async Task Main(string[] args) {
      await  new God.CLI.Application().Run(args);     
    }
  }
}
