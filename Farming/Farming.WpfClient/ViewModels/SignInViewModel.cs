using Farming.WpfClient.Commands;
using Farming.WpfClient.DatabaseService;
using MaterialDesignThemes.Wpf;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Farming.WpfClient.ViewModels
{
    public class SignInViewModel : ConnectionViewModel
    {
        public string Login
        {
            get { return Get(() => Login); }
            set { Set(() => Login, value); }
        }

        public ICommand SignInCommand { get; }

        public SignInViewModel(IConnectionService cnnService, ISnackbarMessageQueue snackbarMessageQueue)
            : base("Вход", cnnService, snackbarMessageQueue)
        {
            SignInCommand = new RelayCommand(async sender =>
            {
                if (IsBusy || !Valid()) return;

                IsBusy = true;

                await Task.Delay(500);

                try
                {
                    await cnnService.SignInAsync(Login, SecurePassword);
                }
                catch (ArgumentException argEx)
                {
                    Enqueue(argEx.Message);
                }
                catch (InvalidOperationException invOpEx)
                {
                    Enqueue(invOpEx.Message);
                }
                catch (Exception ex)
                {
                    Enqueue("Обнаружена непредвиденная ошибка.");
                }
                finally
                {
                    IsBusy = false;
                }
            });

            AddRule(() => Login, () => !string.IsNullOrWhiteSpace(Login) && Login.Length < 50,
                "Логин не должен быть пустым и содержать больше 50 символов.");
        }
    }
}
