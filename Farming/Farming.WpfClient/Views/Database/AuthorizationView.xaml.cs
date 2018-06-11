using Farming.WpfClient.AttachedProperties;
using Farming.WpfClient.Commands;
using Farming.WpfClient.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Input;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationView.xaml
    /// </summary>
    public partial class AuthorizationView : Window, IView<SignInViewModel>
    {
        public ICommand CloseCommand { get; }

        public SignInViewModel ViewModel
        {
            get => DataContext as SignInViewModel;
            set => DataContext = value;
        }

        public AuthorizationView()
        {
            CloseCommand = new RelayCommand(sender => Close());

            InitializeComponent();
        }

        public AuthorizationView(SignInViewModel viewModel, SnackbarMessageQueue snackbarMessageQueue)
        {
            ViewModel = viewModel;

            CloseCommand = new RelayCommand(sender => Close());

            InitializeComponent();

            PART_authSnackbar.MessageQueue = snackbarMessageQueue ?? throw new ArgumentNullException(nameof(snackbarMessageQueue));
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EncryptedPasswordProperty.SetValue(PwdBox, PwdBox.SecurePassword);
        }
    }
}
