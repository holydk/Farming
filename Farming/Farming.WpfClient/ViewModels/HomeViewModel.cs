using MaterialDesignThemes.Wpf;

namespace Farming.WpfClient.ViewModels
{
    public class HomeViewModel : PageViewModel
    {
        public HomeViewModel(ISnackbarMessageQueue snackbarMessageQueue)
            : base("Главная", snackbarMessageQueue)
        {

        }
    }
}
