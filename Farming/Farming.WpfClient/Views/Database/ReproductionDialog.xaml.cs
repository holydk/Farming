using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;
using System.Windows.Input;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для ReproductionDialog.xaml
    /// </summary>
    public partial class ReproductionDialog : UserControl, IView<ReproductionViewModel>
    {
        public ReproductionDialog(string action, ReproductionViewModel viewModel, ReproductionsViewModel repsVm)
        {
            ViewModel = viewModel;

            InitializeComponent();

            PART_inputCow.DataContext = repsVm.CowsViewModel;
            PART_inputBull.DataContext = repsVm.CowsViewModel.BullsViewModel;
            PART_selectMethodSluchki.DataContext = repsVm.MethodsSluchkiViewModel;

            ActionTb.Text = action.ToUpper();
        }

        public ReproductionViewModel ViewModel
        {
            get => DataContext as ReproductionViewModel;
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
