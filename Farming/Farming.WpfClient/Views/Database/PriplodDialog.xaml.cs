using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;
using System.Windows.Input;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для PriplodDialog.xaml
    /// </summary>
    public partial class PriplodDialog : UserControl, IView<PriplodViewModel>
    {
        public PriplodDialog(string action, PriplodViewModel viewModel, PriplodsViewModel priplodsVm)
        {
            ViewModel = viewModel;

            InitializeComponent();

            PART_selectGender.DataContext = priplodsVm.GendersViewModel;
            PART_inputMother.DataContext = priplodsVm.CowsViewModel;
            ActionTb.Text = action.ToUpper();
        }

        public PriplodViewModel ViewModel
        {
            get => DataContext as PriplodViewModel;
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
