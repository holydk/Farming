using Farming.WpfClient.DatabaseService;
using Farming.WpfClient.Models;
using Farming.WpfClient.ViewModels;
using Farming.WpfClient.Views;
using Farming.WpfClient.Views.Database;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace Farming.WpfClient
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var cnnService = new FarmingConnectionService();
            var authSnackbarMessageQueue = new SnackbarMessageQueue();
            var cnnViewModel = new SignInViewModel(cnnService, authSnackbarMessageQueue);
            var authView = new AuthorizationView(cnnViewModel, authSnackbarMessageQueue);

            ConnectionEventHandler onSuccessfulSignIn = null;

            onSuccessfulSignIn = (obj, _e) =>
            {
                cnnService.SuccessfulSignIn -= onSuccessfulSignIn;

                var navigation = new Navigation();
                var snackbarMessageQueue = new SnackbarMessageQueue();
                var mainViewModel = new MainViewModel(_e.User, navigation, "FARMING inc.", snackbarMessageQueue);

                MainWindow = new MainWindow(mainViewModel, snackbarMessageQueue);
                authView.Close();
                MainWindow.Show();
            };

            cnnService.SuccessfulSignIn += onSuccessfulSignIn;

            authView.Show();

            base.OnStartup(e);
        }
    }
}
