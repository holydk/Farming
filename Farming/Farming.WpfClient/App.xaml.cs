using Farming.WpfClient.Models;
using Farming.WpfClient.ViewModels;
using Farming.WpfClient.Views;
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
            var navigation = new Navigation();
            var mainViewModel = new MainViewModel(navigation, "FARMING inc.");

            MainWindow = new MainWindow(mainViewModel);
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
