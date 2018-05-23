using System.Windows.Controls;
using Farming.WpfClient.Controls;
using Farming.WpfClient.ViewModels.Database;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для BloodTypesView.xaml
    /// </summary>
    public partial class BloodTypesView : DialogDatabaseTableView
    {
        public BloodTypesView(BloodTypesViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }       

        protected override object GetAddDialogView()
        {
            return new BloodTypeDialog("Добавить", new BloodTypeViewModel());
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is BloodTypesViewModel viewModel && viewModel.SelectedModel != null)
            {
                return new BloodTypeDialog("Изменить", viewModel.SelectedModel);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is BloodTypesViewModel viewModel && viewModel.SelectedModel != null)
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
            if (sender is Button btn && ViewModel is BloodTypesViewModel viewModel)
            {
                viewModel.SelectedModel = btn.CommandParameter as BloodTypeViewModel;
            }
        }
    }
}
