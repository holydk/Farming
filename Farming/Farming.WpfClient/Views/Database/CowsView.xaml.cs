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
    /// Логика взаимодействия для CowsView.xaml
    /// </summary>
    public partial class CowsView : DialogDatabaseTableView
    {
        public CowsView(CowsViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();

            ActionItems.Add(new ActionItem("Фильтр", PackIconKind.Filter, PackIconKind.FilterOutline, new RelayCommand(sender =>
            {
                if (sender is ActionItem item)
                {
                    UpdateContent(item, new CowsFilterView() { DataContext = ViewModel });
                }
            })));

            ActionItems.Add(new CommandItem("Сохранить", PackIconKind.Download, viewModel.SaveAsCommand));
        }

        protected override object GetAddDialogView()
        {
            if (ViewModel is CowsViewModel viewModel)
            {
                var snackbar = (Application.Current.MainWindow as MainWindow)?.MainSnackbar.MessageQueue;
                var user = (Application.Current.MainWindow as MainWindow)?.ViewModel.User;

                if (snackbar == null || user == null) return null;

                var bullsVm = new BullsViewModel(snackbar, user, viewModel.BullsViewModel.Models);
                var cowsVm = new CowsViewModel(snackbar, viewModel.Models, user, bullsVm, viewModel.BloodTypesViewModel, viewModel.CategoriesViewModel, viewModel.LinesViewModel, viewModel.BreedsViewModel, viewModel.FamiliesViewModel);

                return new CowDialog("Добавить", new CowViewModel(), cowsVm);
            }

            return null;
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is CowsViewModel viewModel && viewModel.SelectedModel != null)
            {
                var cowVm = new CowViewModel()
                {
                    Id = viewModel.SelectedModel.Id,
                    BreedId = viewModel.SelectedModel.BreedId,
                    LineId = viewModel.SelectedModel.LineId,
                    FamilyId = viewModel.SelectedModel.FamilyId,
                    CategoryId = viewModel.SelectedModel.CategoryId,
                    BloodTypeId = viewModel.SelectedModel.BloodTypeId,
                    MotherId = viewModel.SelectedModel.MotherId,
                    FatherId = viewModel.SelectedModel.FatherId,
                    Porodnost = viewModel.SelectedModel.Porodnost.Value,
                    Name = viewModel.SelectedModel.Name,
                    BDay = viewModel.SelectedModel.BDay.Value,
                    BPlace = viewModel.SelectedModel.BPlace,
                    Weight = viewModel.SelectedModel.Weight.Value,
                    Age = viewModel.SelectedModel.Age.Value,
                    IsInHerd = viewModel.SelectedModel.IsInHerd.Value,
                    Breed = viewModel.SelectedModel.Breed,
                    Line = viewModel.SelectedModel.Line,
                    Family = viewModel.SelectedModel.Family,
                    Category = viewModel.SelectedModel.Category,
                    BloodType = viewModel.SelectedModel.BloodType,
                    Mother = viewModel.SelectedModel.Mother,
                    Father = viewModel.SelectedModel.Father
                };

                var snackbar = (Application.Current.MainWindow as MainWindow)?.MainSnackbar.MessageQueue;
                var user = (Application.Current.MainWindow as MainWindow)?.ViewModel.User;

                if (snackbar == null || user == null) return null;

                var bullsVm = new BullsViewModel(snackbar, user, viewModel.BullsViewModel.Models);
                var cowsVm = new CowsViewModel(snackbar, viewModel.Models, user, bullsVm, viewModel.BloodTypesViewModel, viewModel.CategoriesViewModel, viewModel.LinesViewModel, viewModel.BreedsViewModel, viewModel.FamiliesViewModel);

                return new CowDialog("Изменить", cowVm, cowsVm);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is CowsViewModel viewModel && viewModel.SelectedModel != null)
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

                if (ViewModel is CowsViewModel viewModel)
                {
                    viewModel.SelectedModel = item.DataContext as CowViewModel;
                }
            }
        }
    }
}
