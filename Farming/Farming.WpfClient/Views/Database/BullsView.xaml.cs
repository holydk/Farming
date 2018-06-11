using Farming.WpfClient.Controls;
using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для BullsView.xaml
    /// </summary>
    public partial class BullsView : DialogDatabaseTableView
    {
        public BullsView(BullsViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }

        protected override object GetAddDialogView()
        {
            return new BullDialog("Добавить", new BullViewModel());
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is BullsViewModel viewModel && viewModel.SelectedModel != null)
            {
                var bullVm = new BullViewModel()
                {
                    Id = viewModel.SelectedModel.Id,
                    Location = viewModel.SelectedModel.Location,
                    OtherId = viewModel.SelectedModel.OtherId,
                    Childrens = viewModel.SelectedModel.Childrens,
                    Reproductions = viewModel.SelectedModel.Reproductions
                };

                return new BullDialog("Изменить", bullVm);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is BullsViewModel viewModel && viewModel.SelectedModel != null)
            {
                return new MessageDialog("Вы действительно хотите удалить запись?")
                {
                    DataContext = viewModel.SelectedModel
                };
            }

            return null;
        }

        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem item)
            {
                item.IsSelected = true;

                if (ViewModel is BullsViewModel viewModel)
                {
                    viewModel.SelectedModel = item.DataContext as BullViewModel; 
                }
            }
        }
    }
}
