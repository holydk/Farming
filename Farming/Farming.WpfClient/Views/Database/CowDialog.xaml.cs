using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;
using System.Windows.Input;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для CowDialog.xaml
    /// </summary>
    public partial class CowDialog : UserControl, IView<CowViewModel>
    {
        public CowDialog(string action, CowViewModel viewModel, CowsViewModel cowsVm)
        {
            ViewModel = viewModel;

            InitializeComponent();

            PART_inputMother.DataContext = cowsVm;
            PART_inputFather.DataContext = cowsVm.BullsViewModel;
            PART_selectBloodType.DataContext = cowsVm.BloodTypesViewModel;
            PART_selectBreed.DataContext = cowsVm.BreedsViewModel;
            PART_selectCategory.DataContext = cowsVm.CategoriesViewModel;
            PART_selectFamily.DataContext = cowsVm.FamiliesViewModel;
            PART_selectLine.DataContext = cowsVm.LinesViewModel;

            ActionTb.Text = action.ToUpper();
        }

        public CowViewModel ViewModel
        {
            get => DataContext as CowViewModel;
            set => DataContext = value;
        }

        private void SelectedMother_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem item)
            {
                item.IsSelected = true;
                //ViewModel.Father = item.DataContext as BullViewModel;
            }
        }

        private void SelectedFather_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem item)
            {
                item.IsSelected = true;
                //ViewModel.Mother = item.DataContext as CowViewModel;
            }
        }
    }
}
