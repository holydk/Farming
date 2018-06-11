using Farming.WpfClient.Controls;
using Farming.WpfClient.ViewModels.Database;
using System.Windows.Controls;
using System.Windows.Input;

namespace Farming.WpfClient.Views.Database
{
    /// <summary>
    /// Логика взаимодействия для UsersView.xaml
    /// </summary>
    public partial class UsersView : DialogDatabaseTableView
    {
        public UsersView(UsersViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }

        protected override object GetAddDialogView()
        {
            if (ViewModel is UsersViewModel viewModel)
            {
                return new UserDialog("Добавить", new UserViewModel(), viewModel);
            }

            return null;
        }

        protected override object GetUpdateDialogView()
        {
            if (ViewModel is UsersViewModel viewModel && viewModel.SelectedModel != null)
            {
                var userVm = new UserViewModel()
                {
                    Id = viewModel.SelectedModel.Id,
                    Login = viewModel.SelectedModel.Login,
                    Password = viewModel.SelectedModel.Password,
                    LastName = viewModel.SelectedModel.LastName,
                    FirstName = viewModel.SelectedModel.FirstName,
                    MiddleName = viewModel.SelectedModel.MiddleName,
                    Phone = viewModel.SelectedModel.Phone,
                    UserTypeId = viewModel.SelectedModel.UserTypeId,
                    UserType = viewModel.SelectedModel.UserType
                };

                return new UserDialog("Изменить", userVm, viewModel);
            }

            return null;
        }

        protected override object GetDeleteDialogView()
        {
            if (ViewModel is UsersViewModel viewModel && viewModel.SelectedModel != null)
            {
                return new MessageDialog("Вы действительно хотите удалить запись?")
                {
                    DataContext = viewModel.SelectedModel
                };
            }

            return null;
        }

        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem item)
            {
                item.IsSelected = true;

                if (ViewModel is UsersViewModel viewModel)
                {
                    viewModel.SelectedModel = item.DataContext as UserViewModel;
                }
            }
        }
    }
}
