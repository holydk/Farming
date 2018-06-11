using Farming.WpfClient.DatabaseService;
using MaterialDesignThemes.Wpf;
using System;
using System.Security;

namespace Farming.WpfClient.ViewModels
{
    public abstract class ConnectionViewModel : PageViewModel
    {
        private readonly IConnectionService _connectionService;

        public SecureString SecurePassword
        {
            get { return Get(() => SecurePassword); }
            set { Set(() => SecurePassword, value); }
        }

        public ConnectionViewModel(string title, IConnectionService cnnService, ISnackbarMessageQueue snackbarMessageQueue)
            : base(title, snackbarMessageQueue)
        {
            IsValidOnClick = true;
            _connectionService = cnnService ?? throw new ArgumentNullException(nameof(cnnService));

            AddRule(() => SecurePassword, () => SecurePassword != null && SecurePassword.Length > 0, "Пароль не должен быть пустым.");
        }
    }
}
