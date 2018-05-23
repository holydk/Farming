using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для LineDialog.xaml
    /// </summary>
    public partial class LineDialog : UserControl, IView<LineViewModel>
    {
        public LineDialog(string action, LineViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            ActionTb.Text = action.ToUpper();
        }

        public LineViewModel ViewModel
        {
            get => DataContext as LineViewModel;
            set => DataContext = value;
        }
    }
}
