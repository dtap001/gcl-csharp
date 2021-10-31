using God.CLI.Domain;

namespace God.CLI.Common {
  public interface IUserInteractor {
    UserSaid AskForPermissionToContinue(string question);
  }
}