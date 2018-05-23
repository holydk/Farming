using Farming.WpfClient.Controls;
using Farming.WpfClient.ViewModels.Database;
using System.Windows;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для LinesView.xaml
    /// </summary>
    public partial class LinesView : DialogDatabaseTableView
    {
        public LinesView(LinesViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }

        protected override object GetAddDialogView()
        {
            return new LineDialog("Добавить", new LineViewModel());
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is LinesViewModel viewModel && viewModel.SelectedModel != null)
            {
                return new LineDialog("Изменить", viewModel.SelectedModel);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is LinesViewModel viewModel && viewModel.SelectedModel != null)
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
            if (sender is Button btn && ViewModel is LinesViewModel viewModel)
            {
                viewModel.SelectedModel = btn.CommandParameter as LineViewModel;
            }
        }
    }
}
