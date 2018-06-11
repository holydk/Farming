using Farming.WpfClient.Models;
using System;
using System.Security;

namespace Farming.WpfClient.DatabaseService
{
    public class ConnectionEventArgs : EventArgs
    {
        public User User { get; }
        public SecureString Password { get; }

        public ConnectionEventArgs(User user, SecureString password)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}
