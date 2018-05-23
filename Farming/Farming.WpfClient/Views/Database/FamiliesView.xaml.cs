using Farming.WpfClient.Controls;
using Farming.WpfClient.ViewModels.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для FamiliesView.xaml
    /// </summary>
    public partial class FamiliesView : DialogDatabaseTableView
    {
        public FamiliesView(FamiliesViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }

        protected override object GetAddDialogView()
        {
            return new FamilyDialog("Добавить", new FamilyViewModel());
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is FamiliesViewModel viewModel && viewModel.SelectedModel != null)
            {
                return new FamilyDialog("Изменить", viewModel.SelectedModel);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is FamiliesViewModel viewModel && viewModel.SelectedModel != null)
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
            if (sender is Button btn && ViewModel is FamiliesViewModel viewModel)
            {
                viewModel.SelectedModel = btn.CommandParameter as FamilyViewModel;
            }
        }
    }
}
