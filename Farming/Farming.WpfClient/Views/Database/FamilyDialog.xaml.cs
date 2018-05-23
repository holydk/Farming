using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для FamilyDialog.xaml
    /// </summary>
    public partial class FamilyDialog : UserControl, IView<FamilyViewModel>
    {
        public FamilyDialog(string action, FamilyViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            ActionTb.Text = action.ToUpper();
        }

        public FamilyViewModel ViewModel
        {
            get => DataContext as FamilyViewModel;
            set => DataContext = value;
        }
    }
}
