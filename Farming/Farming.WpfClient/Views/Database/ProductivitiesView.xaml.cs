using Farming.WpfClient.Controls;
using Farming.WpfClient.Models;
using Farming.WpfClient.ViewModels.Database;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для ProductivitiesView.xaml
    /// </summary>
    public partial class ProductivitiesView : DialogDatabaseTableView
    {
        public ProductivitiesView(ProductivitiesViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();

            ActionItems.Add(new CommandItem("Сохранить", PackIconKind.Download, viewModel.SaveAsCommand));
        }

        protected override object GetAddDialogView()
        {
            if (ViewModel is ProductivitiesViewModel viewModel)
            {
                var snackbar = (Application.Current.MainWindow as MainWindow)?.MainSnackbar.MessageQueue;
                var user = (Application.Current.MainWindow as MainWindow)?.ViewModel.User;

                if (snackbar == null || user == null) return null;

                var cowsVm = new CowsViewModel(snackbar, viewModel.CowsViewModel.Models, user, viewModel.CowsViewModel.BullsViewModel, viewModel.CowsViewModel.BloodTypesViewModel, viewModel.CowsViewModel.CategoriesViewModel, viewModel.CowsViewModel.LinesViewModel, viewModel.CowsViewModel.BreedsViewModel, viewModel.CowsViewModel.FamiliesViewModel);
                var prodsVm = new ProductivitiesViewModel(snackbar, user, cowsVm, viewModel.Models);

                return new ProductivityDialog("Добавить", new ProductivityViewModel(), prodsVm);
            }

            return null;
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is ProductivitiesViewModel viewModel && viewModel.SelectedModel != null)
            {
                var snackbar = (Application.Current.MainWindow as MainWindow)?.MainSnackbar.MessageQueue;
                var user = (Application.Current.MainWindow as MainWindow)?.ViewModel.User;

                if (snackbar == null || user == null) return null;

                var cowsVm = new CowsViewModel(snackbar, viewModel.CowsViewModel.Models, user, viewModel.CowsViewModel.BullsViewModel, viewModel.CowsViewModel.BloodTypesViewModel, viewModel.CowsViewModel.CategoriesViewModel, viewModel.CowsViewModel.LinesViewModel, viewModel.CowsViewModel.BreedsViewModel, viewModel.CowsViewModel.FamiliesViewModel);
                var prodsVm = new ProductivitiesViewModel(snackbar, user, cowsVm, viewModel.Models);

                var productivityVm = new ProductivityViewModel()
                {
                    Id = viewModel.SelectedModel.Id,
                    BelokProz = viewModel.SelectedModel.BelokProz,
                    Date = viewModel.SelectedModel.Date,
                    Cow = viewModel.SelectedModel.Cow,
                    CowId = viewModel.SelectedModel.CowId,
                    UchtenoLaktacij = viewModel.SelectedModel.UchtenoLaktacij,
                    UdojKg = viewModel.SelectedModel.UdojKg,
                    ZhirProz = viewModel.SelectedModel.ZhirProz
                };

                return new ProductivityDialog("Изменить", productivityVm, prodsVm);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is ProductivitiesViewModel viewModel && viewModel.SelectedModel != null)
            {
                return new MessageDialog("Вы действительно хотите удалить запись?")
                {
                    DataContext = viewModel.SelectedModel
                };
            }

            return null;
        }

        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem item)
            {
                item.IsSelected = true;

                if (ViewModel is ProductivitiesViewModel viewModel)
                {
                    viewModel.SelectedModel = item.DataContext as ProductivityViewModel;
                }
            }
        }
    }
}
