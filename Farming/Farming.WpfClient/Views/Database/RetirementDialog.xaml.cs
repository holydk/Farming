using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;
using System.Windows.Input;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для RetirementDialog.xaml
    /// </summary>
    public partial class RetirementDialog : UserControl, IView<RetirementViewModel>
    {
        public RetirementDialog(string action, RetirementViewModel viewModel, RetirementsViewModel retirementsVm)
        {
            ViewModel = viewModel;

            InitializeComponent();

            PART_inputCow.DataContext = retirementsVm.CowsViewModel;
            ActionTb.Text = action.ToUpper();
        }

        public RetirementViewModel ViewModel
        {
            get => DataContext as RetirementViewModel;
            set => DataContext = value;
        }

        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem item)
            {
                item.IsSelected = true;
            }
        }
    }
}
