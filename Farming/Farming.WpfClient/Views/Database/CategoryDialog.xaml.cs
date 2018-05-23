using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для CategoryDialog.xaml
    /// </summary>
    public partial class CategoryDialog : UserControl, IView<CategoryViewModel>
    {
        public CategoryDialog(string action, CategoryViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            ActionTb.Text = action.ToUpper();
        }

        public CategoryViewModel ViewModel
        {
            get => DataContext as CategoryViewModel;
            set => DataContext = value;
        }
    }
}
