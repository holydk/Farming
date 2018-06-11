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
    /// Логика взаимодействия для ReproductionsView.xaml
    /// </summary>
    public partial class ReproductionsView : DialogDatabaseTableView
    {
        public ReproductionsView(ReproductionsViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();

            ActionItems.Add(new ActionItem("Фильтр", PackIconKind.Filter, PackIconKind.FilterOutline, new RelayCommand(sender =>
            {
                if (sender is ActionItem item)
                {
                    UpdateContent(item, new ReproductionsFilterView() { DataContext = ViewModel });
                }
            })));

            ActionItems.Add(new CommandItem("Сохранить", PackIconKind.Download, viewModel.SaveAsCommand));
        }

        protected override object GetAddDialogView()
        {
            if (ViewModel is ReproductionsViewModel viewModel)
            {
                var snackbar = (Application.Current.MainWindow as MainWindow)?.MainSnackbar.MessageQueue;
                var user = (Application.Current.MainWindow as MainWindow)?.ViewModel.User;

                if (snackbar == null || user == null) return null;

                var bullsVm = new BullsViewModel(snackbar, user, viewModel.CowsViewModel.BullsViewModel.Models);
                var cowsVm = new CowsViewModel(snackbar, viewModel.CowsViewModel.Models, user, bullsVm, viewModel.CowsViewModel.BloodTypesViewModel, viewModel.CowsViewModel.CategoriesViewModel, viewModel.CowsViewModel.LinesViewModel, viewModel.CowsViewModel.BreedsViewModel, viewModel.CowsViewModel.FamiliesViewModel);
                var reprsVm = new ReproductionsViewModel(snackbar, user, cowsVm, viewModel.MethodsSluchkiViewModel, viewModel.Models);

                return new ReproductionDialog("Добавить", new ReproductionViewModel(), reprsVm);
            }

            return null;
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is ReproductionsViewModel viewModel && viewModel.SelectedModel != null)
            {
                var snackbar = (Application.Current.MainWindow as MainWindow)?.MainSnackbar.MessageQueue;
                var user = (Application.Current.MainWindow as MainWindow)?.ViewModel.User;

                if (snackbar == null || user == null) return null;

                var bullsVm = new BullsViewModel(snackbar, user, viewModel.CowsViewModel.BullsViewModel.Models);
                var cowsVm = new CowsViewModel(snackbar, viewModel.CowsViewModel.Models, user, bullsVm, viewModel.CowsViewModel.BloodTypesViewModel, viewModel.CowsViewModel.CategoriesViewModel, viewModel.CowsViewModel.LinesViewModel, viewModel.CowsViewModel.BreedsViewModel, viewModel.CowsViewModel.FamiliesViewModel);
                var reprsVm = new ReproductionsViewModel(snackbar, user, cowsVm, viewModel.MethodsSluchkiViewModel, viewModel.Models);

                var repVm = new ReproductionViewModel()
                {
                    Id = viewModel.SelectedModel.Id,
                    CowId = viewModel.SelectedModel.CowId,
                    MethodSluchkiId = viewModel.SelectedModel.MethodSluchkiId,
                    BullId = viewModel.SelectedModel.BullId,
                    DateOsemeneniya = viewModel.SelectedModel.DateOsemeneniya,
                    ChisloSuhihDney = viewModel.SelectedModel.ChisloSuhihDney,
                    SerPeriod = viewModel.SelectedModel.SerPeriod,
                    DateOtela = viewModel.SelectedModel.DateOtela,
                    Bull = viewModel.SelectedModel.Bull,
                    Cow = viewModel.SelectedModel.Cow,
                    MethodSluchki = viewModel.SelectedModel.MethodSluchki
                };

                return new ReproductionDialog("Изменить", repVm, reprsVm);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is ReproductionsViewModel viewModel && viewModel.SelectedModel != null)
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

                if (ViewModel is ReproductionsViewModel viewModel)
                {
                    viewModel.SelectedModel = item.DataContext as ReproductionViewModel;
                }
            }
        }
    }
}
