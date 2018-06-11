using Farming.WpfClient.Controls;
using Farming.WpfClient.ViewModels.Database;
using System.Windows;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для CategoriesView.xaml
    /// </summary>
    public partial class CategoriesView : DialogDatabaseTableView
    {
        public CategoriesView(CategoriesViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }

        protected override object GetAddDialogView()
        {
            return new CategoryDialog("Добавить", new CategoryViewModel());
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is CategoriesViewModel viewModel && viewModel.SelectedModel != null)
            {
                var categoryVm = new CategoryViewModel()
                {
                    Id = viewModel.SelectedModel.Id,
                    Name = viewModel.SelectedModel.Name
                };

                return new CategoryDialog("Изменить", categoryVm);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is CategoriesViewModel viewModel && viewModel.SelectedModel != null)
            {
                return new MessageDialog("Вы действительно хотите удалить запись?")
                {
                    DataContext = viewModel.SelectedModel
                };
            }

            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && ViewModel is CategoriesViewModel viewModel)
            {
                viewModel.SelectedModel = btn.CommandParameter as CategoryViewModel;
            }
        }
    }
}
