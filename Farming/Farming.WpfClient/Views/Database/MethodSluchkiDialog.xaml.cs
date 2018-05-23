using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для MethodSluchkiDialog.xaml
    /// </summary>
    public partial class MethodSluchkiDialog : UserControl, IView<MethodSluchkiViewModel>
    {
        public MethodSluchkiDialog(string action, MethodSluchkiViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            ActionTb.Text = action.ToUpper();
        }

        public MethodSluchkiViewModel ViewModel
        {
            get => DataContext as MethodSluchkiViewModel;
            set => DataContext = value;
        }
    }
}
