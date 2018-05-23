using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для BreedDialog.xaml
    /// </summary>
    public partial class BreedDialog : UserControl, IView<BreedViewModel>
    {
        public BreedDialog(string action, BreedViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            ActionTb.Text = action.ToUpper();
        }

        public BreedViewModel ViewModel
        {
            get => DataContext as BreedViewModel;
            set => DataContext = value;
        }
    }
}
