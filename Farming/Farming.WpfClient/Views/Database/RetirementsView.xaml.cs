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
    /// Логика взаимодействия для RetirementsView.xaml
    /// </summary>
    public partial class RetirementsView : DialogDatabaseTableView
    {
        public RetirementsView(RetirementsViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();

            ActionItems.Add(new CommandItem("Сохранить", PackIconKind.Download, viewModel.SaveAsCommand));
        }

        protected override object GetAddDialogView()
        {
            if (ViewModel is RetirementsViewModel viewModel)
            {
                var snackbar = (Application.Current.MainWindow as MainWindow)?.MainSnackbar.MessageQueue;
                var user = (Application.Current.MainWindow as MainWindow)?.ViewModel.User;

                if (snackbar == null || user == null) return null;

                var cowsVm = new CowsViewModel(snackbar, viewModel.CowsViewModel.Models, user, viewModel.CowsViewModel.BullsViewModel, viewModel.CowsViewModel.BloodTypesViewModel, viewModel.CowsViewModel.CategoriesViewModel, viewModel.CowsViewModel.LinesViewModel, viewModel.CowsViewModel.BreedsViewModel, viewModel.CowsViewModel.FamiliesViewModel);
                var retVm = new RetirementsViewModel(snackbar, user, cowsVm, viewModel.Models);

                return new RetirementDialog("Добавить", new RetirementViewModel(), retVm);
            }

            return null;
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is RetirementsViewModel viewModel && viewModel.SelectedModel != null)
            {
                var snackbar = (Application.Current.MainWindow as MainWindow)?.MainSnackbar.MessageQueue;
                var user = (Application.Current.MainWindow as MainWindow)?.ViewModel.User;

                if (snackbar == null || user == null) return null;

                var cowsVm = new CowsViewModel(snackbar, viewModel.CowsViewModel.Models, user, viewModel.CowsViewModel.BullsViewModel, viewModel.CowsViewModel.BloodTypesViewModel, viewModel.CowsViewModel.CategoriesViewModel, viewModel.CowsViewModel.LinesViewModel, viewModel.CowsViewModel.BreedsViewModel, viewModel.CowsViewModel.FamiliesViewModel);
                var retVm = new RetirementsViewModel(snackbar, user, cowsVm, viewModel.Models);

                var retirementVm = new RetirementViewModel()
                {
                    Id = viewModel.SelectedModel.Id,
                    CowId = viewModel.SelectedModel.CowId,
                    Date = viewModel.SelectedModel.Date,
                    Reason = viewModel.SelectedModel.Reason,
                    Cow = viewModel.SelectedModel.Cow
                };

                return new RetirementDialog("Изменить", retirementVm, retVm);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is RetirementsViewModel viewModel && viewModel.SelectedModel != null)
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

                if (ViewModel is RetirementsViewModel viewModel)
                {
                    viewModel.SelectedModel = item.DataContext as RetirementViewModel;
                }
            }
        }
    }
}
