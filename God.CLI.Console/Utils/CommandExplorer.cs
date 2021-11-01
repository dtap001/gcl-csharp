using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using God.CLI.Domain;

namespace God.CLI.Console.Utils {
  public class CommandExplorer {
    public static List<GCLCommandInfo> GetAll() {
      var result = new List<GCLCommandInfo>();
      var assembly = Assembly.GetExecutingAssembly();

      var methods = assembly.GetTypes().SelectMany(m => m.GetMethods())
          .Where(x => x.GetCustomAttributes(typeof(ConsoleAppFramework.CommandAttribute), false).Length > 0);

      foreach (var method in methods) {
        var classCommandAttribute = method.DeclaringType.GetCustomAttribute<ConsoleAppFramework.CommandAttribute>();
        var parentComandInfo = string.Empty;
        if (classCommandAttribute?.CommandNames != null && classCommandAttribute.CommandNames.Length > 0) {
          parentComandInfo = classCommandAttribute.CommandNames[0];
        }
        var attributeInfo = method.GetCustomAttributes<ConsoleAppFramework.CommandAttribute>().Select(x => new GCLCommandInfo() {
          Command = string.Join(',', x.CommandNames),
          Description = x.Description,
          ParentCommand = parentComandInfo,
          ClassType = method.DeclaringType,
          MethodName = method.Name
        }).First(); //for now we are currently using one command attribute per command

        result.Add(attributeInfo);
      }
      return result;
    }
  }
}