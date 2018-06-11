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
    /// Логика взаимодействия для DatabaseTableMiniSearchView.xaml
    /// </summary>
    public partial class DatabaseTableMiniSearchView : UserControl, IView<IDatabaseTableViewModel>
    {
        public DatabaseTableMiniSearchView()
        {
            InitializeComponent();
        }

        public IDatabaseTableViewModel ViewModel
        {
            get => DataContext as IDatabaseTableViewModel;
            set => DataContext = value;
        }

        private void SearchTextBox_Search(string obj)
        {
            if (ViewModel != null)
            {
                ViewModel.Search(obj);
            }
        }
    }
}
