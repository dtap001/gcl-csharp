using Autofac;
using System.Threading.Tasks;

namespace God.CLI {
  class Program {
    static async Task Main(string[] args) {
      await God.CLI.Framework.Setup.Get<Application>().Resolve<Application>().Run(args);     
    }
  }
}
