using Farming.WpfClient.Controls;
using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для GendersView.xaml
    /// </summary>
    public partial class GendersView : DialogDatabaseTableView
    {
        public GendersView(GendersViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }

        protected override object GetAddDialogView()
        {
            return new GenderDialog("Добавить", new GenderViewModel());
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is GendersViewModel viewModel && viewModel.SelectedModel != null)
            {
                return new GenderDialog("Изменить", viewModel.SelectedModel);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is GendersViewModel viewModel && viewModel.SelectedModel != null)
            {
                return new MessageDialog("Вы действительно хотите удалить запись?")
                {
                    DataContext = viewModel.SelectedModel
                };
            }

            return null;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is Button btn && ViewModel is GendersViewModel viewModel)
            {
                viewModel.SelectedModel = btn.CommandParameter as GenderViewModel;
            }
        }
    }
}
