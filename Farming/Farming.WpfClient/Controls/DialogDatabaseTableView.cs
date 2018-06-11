using Farming.WpfClient.Commands;
using Farming.WpfClient.Helpers;
using Farming.WpfClient.Models;
using Farming.WpfClient.ViewModels;
using Farming.WpfClient.ViewModels.Database;
using Farming.WpfClient.Views;
using Farming.WpfClient.Views.Database;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Farming.WpfClient.Controls
{
    public abstract class DialogDatabaseTableView: PageView, IView<IDatabaseTableViewModel>
    {
        public ICommand AddCommand { get; }

        public ICommand UpdateCommand { get; }

        public ICommand DeleteCommand { get; }

        public IDatabaseTableViewModel ViewModel
        {
            get => DataContext as IDatabaseTableViewModel;
            set => DataContext = value;
        }

        public DialogDatabaseTableView()
        {

        }

        public DialogDatabaseTableView(IDatabaseTableViewModel viewModel)
        {
            ViewModel = viewModel;

            AddCommand = new RelayCommand(
                async sender => await RunDialogAsync(GetAddDialogView(), ViewModel.AddCommand));

            UpdateCommand = new RelayCommand(
                async sender => await RunDialogAsync(GetUpdateDialogView(), ViewModel.UpdateCommand));

            DeleteCommand = new RelayCommand(
                async sender => await RunDialogAsync(GetDeleteDialogView(), ViewModel.DeleteCommand));

            ActionItems.Add(new CommandItem("Обновить", PackIconKind.Refresh, new RelayCommand(
                async sender => await viewModel.UpdateAsync())));

            ActionItems.Add(new ActionItem("Сортировка", PackIconKind.Sort, PackIconKind.Tune, new RelayCommand(
                sender =>
                {
                    if (sender is ActionItem item)
                    {
                        UpdateContent(item, new DatabaseTableSortView() { DataContext = ViewModel });
                    }
                })));

            ActionItems.Add(new ActionItem("Поиск", PackIconKind.Magnify, PackIconKind.Magnify, new RelayCommand(
                sender =>
                {
                    if (sender is ActionItem item)
                    {
                        UpdateContent(item, new DatabaseTableSearchView() { DataContext = ViewModel });
                    }
                })));

            this.ExecuteOnLoaded(async () =>
            {
                await ViewModel?.UpdateAsync();
            });
        }

        private async Task RunDialogAsync(object content, ICommand cmd)
        {
            if (content == null) return;

            var result = await DialogHost.Show(content, "RootDialog", ClosingDialogEventHandler);

            if (result != null)
            {
                cmd.Execute(result);
            }
        }

        private void ClosingDialogEventHandler(object sender, DialogClosingEventArgs e)
        {
            if (e.Parameter is IValidationViewModel viewModel && !viewModel.Valid())
            {
                e.Cancel();
            }
        }

        protected abstract object GetAddDialogView();

        protected abstract object GetUpdateDialogView();

        protected abstract object GetDeleteDialogView();
    }
}
