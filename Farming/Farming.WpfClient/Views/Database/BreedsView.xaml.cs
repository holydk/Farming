using Farming.WpfClient.Controls;
using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для BreedsView.xaml
    /// </summary>
    public partial class BreedsView : DialogDatabaseTableView
    {
        public BreedsView(BreedsViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }

        protected override object GetAddDialogView()
        {
            return new BreedDialog("Добавить", new BreedViewModel());
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is BreedsViewModel viewModel && viewModel.SelectedModel != null)
            {
                var breedVm = new BreedViewModel()
                {
                    Id = viewModel.SelectedModel.Id,
                    Name = viewModel.SelectedModel.Name
                };

                return new BreedDialog("Изменить", breedVm);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is BreedsViewModel viewModel && viewModel.SelectedModel != null)
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
            if (sender is Button btn && ViewModel is BreedsViewModel viewModel)
            {
                viewModel.SelectedModel = btn.CommandParameter as BreedViewModel;
            }
        }
    }
}
