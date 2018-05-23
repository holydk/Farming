using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для BullDialog.xaml
    /// </summary>
    public partial class BullDialog : UserControl, IView<BullViewModel>
    {
        public BullDialog(string action, BullViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            ActionTb.Text = action.ToUpper();
        }

        public BullViewModel ViewModel
        {
            get => DataContext as BullViewModel;
            set => DataContext = value;
        }
    }
}
