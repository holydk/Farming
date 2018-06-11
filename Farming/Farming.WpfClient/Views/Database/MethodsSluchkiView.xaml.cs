using Farming.WpfClient.Controls;
using Farming.WpfClient.ViewModels.Database;
using System.Windows;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для MethodsSluchkiView.xaml
    /// </summary>
    public partial class MethodsSluchkiView : DialogDatabaseTableView
    {
        public MethodsSluchkiView(MethodsSluchkiViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }

        protected override object GetAddDialogView()
        {
            return new MethodSluchkiDialog("Добавить", new MethodSluchkiViewModel());
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is MethodsSluchkiViewModel viewModel && viewModel.SelectedModel != null)
            {
                var methodSluchkiVm = new MethodSluchkiViewModel()
                {
                    Id = viewModel.SelectedModel.Id,
                    Name = viewModel.SelectedModel.Name
                };

                return new MethodSluchkiDialog("Изменить", methodSluchkiVm);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is MethodsSluchkiViewModel viewModel && viewModel.SelectedModel != null)
            {
                return new MessageDialog("Вы действительно хотите удалить запись?")
                {
                    DataContext = viewModel.SelectedModel
                };
            }

            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && ViewModel is MethodsSluchkiViewModel viewModel)
            {
                viewModel.SelectedModel = btn.CommandParameter as MethodSluchkiViewModel;
            }
        }
    }
}
