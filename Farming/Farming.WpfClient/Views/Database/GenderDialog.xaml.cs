using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для GenderDialog.xaml
    /// </summary>
    public partial class GenderDialog : UserControl, IView<GenderViewModel>
    {
        public GenderDialog(string action, GenderViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            ActionTb.Text = action.ToUpper();
        }

        public GenderViewModel ViewModel
        {
            get => DataContext as GenderViewModel;
            set => DataContext = value;
        }
    }
}
