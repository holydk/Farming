using Farming.WpfClient.Models;
using System.Security;
using System.Threading.Tasks;

namespace Farming.WpfClient.DatabaseService
{
    public interface IConnectionService
    {
        event ConnectionEventHandler SuccessfulSignIn;
        event ConnectionEventHandler SuccessfulLogIn;

        Task SignInAsync(string login, SecureString password);
        Task LogInAsync(User client, SecureString password);
    }
}
