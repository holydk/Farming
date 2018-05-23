using System.Windows.Controls;
using Farming.WpfClient.ViewModels.Database;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для AddBloodTypeDialog.xaml
    /// </summary>
    public partial class BloodTypeDialog : UserControl, IView<BloodTypeViewModel>
    {
        public BloodTypeDialog(string action, BloodTypeViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            ActionTb.Text = action.ToUpper();
        }

        public BloodTypeViewModel ViewModel
        {
            get => DataContext as BloodTypeViewModel;
            set => DataContext = value;
        }
    }
}
