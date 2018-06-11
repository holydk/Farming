using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;
using System.Windows.Input;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для ProductivityDialog.xaml
    /// </summary>
    public partial class ProductivityDialog : UserControl, IView<ProductivityViewModel>
    {
        public ProductivityDialog(string action, ProductivityViewModel viewModel, ProductivitiesViewModel productivitiesVm)
        {
            ViewModel = viewModel;

            InitializeComponent();

            PART_inputCow.DataContext = productivitiesVm.CowsViewModel;
            ActionTb.Text = action.ToUpper();
        }

        public ProductivityViewModel ViewModel
        {
            get => DataContext as ProductivityViewModel;
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
