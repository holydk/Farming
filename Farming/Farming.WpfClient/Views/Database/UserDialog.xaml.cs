using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для UserDialog.xaml
    /// </summary>
    public partial class UserDialog : UserControl, IView<UserViewModel>
    {
        public UserDialog(string action, UserViewModel viewModel, UsersViewModel usersVm)
        {
            ViewModel = viewModel;

            InitializeComponent();

            PART_selectUserType.DataContext = usersVm;
            ActionTb.Text = action.ToUpper();
        }

        public UserViewModel ViewModel
        {
            get => DataContext as UserViewModel;
            set => DataContext = value;
        }
    }
}
