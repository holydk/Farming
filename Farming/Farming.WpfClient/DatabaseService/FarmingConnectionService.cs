using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Farming.WpfClient.Helpers;
using Farming.WpfClient.Models;

namespace Farming.WpfClient.DatabaseService
{
    public delegate void ConnectionEventHandler(object sender, ConnectionEventArgs e);

    public class FarmingConnectionService : IConnectionService
    {
        public event ConnectionEventHandler SuccessfulSignIn;
        public event ConnectionEventHandler SuccessfulLogIn;

        public Task LogInAsync(User client, SecureString password)
        {
            throw new NotImplementedException();
        }

        public async Task SignInAsync(string login, SecureString password)
        {
            if (string.IsNullOrWhiteSpace(login) || password == null || password.Length == 0)
            {
                throw new ArgumentException("Логин или пароль не должен быть пустым");
            }

            password.MakeReadOnly();

            User user = null;

            using (var db = new FarmingEntities())
            {
                var pwd = password.Unsecure();

                user = await db.Users.SingleOrDefaultAsync(
                    u => u.Login == login && u.Password == pwd);

                if (user != null)
                {
                    user.Password = string.Empty;
                    user.UserType = await db.UsersTypes.FindAsync(user.UserTypeId);
                }
            }

            if (user == null)
            {
                throw new InvalidOperationException("Неверный логин или пароль.");
            }
            else
            {              
                OnSuccessfulSignIn(new ConnectionEventArgs(user, password));
            }
        }

        protected virtual void OnSuccessfulSignIn(ConnectionEventArgs e)
        {
            SuccessfulSignIn?.Invoke(this, e);
        }

        protected virtual void OnSuccessfulLogIn(ConnectionEventArgs e)
        {
            SuccessfulLogIn?.Invoke(this, e);
        }
    }
}
