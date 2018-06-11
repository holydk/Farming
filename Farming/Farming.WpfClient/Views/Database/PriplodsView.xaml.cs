using Farming.WpfClient.Commands;
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
    /// Логика взаимодействия для PriplodsView.xaml
    /// </summary>
    public partial class PriplodsView : DialogDatabaseTableView
    {
        public PriplodsView(PriplodsViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();

            ActionItems.Add(new ActionItem("Фильтр", PackIconKind.Filter, PackIconKind.FilterOutline, new RelayCommand(sender =>
            {
                if (sender is ActionItem item)
                {
                    UpdateContent(item, new PriplodsFilterView() { DataContext = ViewModel });
                }
            })));

            ActionItems.Add(new CommandItem("Сохранить", PackIconKind.Download, viewModel.SaveAsCommand));
        }

        protected override object GetAddDialogView()
        {
            if (ViewModel is PriplodsViewModel viewModel)
            {
                var snackbar = (Application.Current.MainWindow as MainWindow)?.MainSnackbar.MessageQueue;
                var user = (Application.Current.MainWindow as MainWindow)?.ViewModel.User;

                if (snackbar == null || user == null) return null;

                var cowsVm = new CowsViewModel(snackbar, viewModel.CowsViewModel.Models, user, viewModel.CowsViewModel.BullsViewModel, viewModel.CowsViewModel.BloodTypesViewModel, viewModel.CowsViewModel.CategoriesViewModel, viewModel.CowsViewModel.LinesViewModel, viewModel.CowsViewModel.BreedsViewModel, viewModel.CowsViewModel.FamiliesViewModel);
                var priplodsVm = new PriplodsViewModel(snackbar, user, cowsVm, viewModel.GendersViewModel, viewModel.Models);

                return new PriplodDialog("Добавить", new PriplodViewModel(), priplodsVm);
            }

            return null;
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is PriplodsViewModel viewModel && viewModel.SelectedModel != null)
            {
                var snackbar = (Application.Current.MainWindow as MainWindow)?.MainSnackbar.MessageQueue;
                var user = (Application.Current.MainWindow as MainWindow)?.ViewModel.User;

                if (snackbar == null || user == null) return null;

                var cowsVm = new CowsViewModel(snackbar, viewModel.CowsViewModel.Models, user, viewModel.CowsViewModel.BullsViewModel, viewModel.CowsViewModel.BloodTypesViewModel, viewModel.CowsViewModel.CategoriesViewModel, viewModel.CowsViewModel.LinesViewModel, viewModel.CowsViewModel.BreedsViewModel, viewModel.CowsViewModel.FamiliesViewModel);
                var priplodsVm = new PriplodsViewModel(snackbar, user, cowsVm, viewModel.GendersViewModel, viewModel.Models);

                var priplodVm = new PriplodViewModel()
                {
                    Id = viewModel.SelectedModel.Id,
                    CowId = viewModel.SelectedModel.CowId,
                    GenderId = viewModel.SelectedModel.GenderId,
                    Weight = viewModel.SelectedModel.Weight,
                    Gender = viewModel.SelectedModel.Gender,
                    Mother = viewModel.SelectedModel.Mother
                };

                return new PriplodDialog("Изменить", priplodVm, priplodsVm);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is PriplodsViewModel viewModel && viewModel.SelectedModel != null)
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

                if (ViewModel is PriplodsViewModel viewModel)
                {
                    viewModel.SelectedModel = item.DataContext as PriplodViewModel;
                }
            }
        }
    }
}
