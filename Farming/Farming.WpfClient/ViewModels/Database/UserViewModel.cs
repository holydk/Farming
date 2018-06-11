using System.ComponentModel;

namespace Farming.WpfClient.ViewModels.Database
{
    public class UserViewModel : ValidationViewModelBase
    {
        [Description("Идентификатор")]
        public int Id
        {
            get => Get(() => Id);
            set => Set(() => Id, value);
        }

        [Description("Логин")]
        public string Login
        {
            get => Get(() => Login);
            set => Set(() => Login, value);
        }

        [Description("Пароль")]
        public string Password
        {
            get => Get(() => Password);
            set => Set(() => Password, value);
        }

        [Description("Фамилия")]
        public string LastName
        {
            get => Get(() => LastName);
            set => Set(() => LastName, value);
        }

        [Description("Имя")]
        public string FirstName
        {
            get => Get(() => FirstName);
            set => Set(() => FirstName, value);
        }

        [Description("Отчество")]
        public string MiddleName
        {
            get => Get(() => MiddleName);
            set => Set(() => MiddleName, value);
        }

        [Description("Телефон")]
        public decimal? Phone
        {
            get => Get(() => Phone);
            set => Set(() => Phone, value);
        }

        [Description("Идентификатор типа пользователя")]
        public int UserTypeId
        {
            get => Get(() => UserTypeId);
            set => Set(() => UserTypeId, value);
        }

        public virtual UserTypeViewModel UserType
        {
            get => Get(() => UserType);
            set
            {
                Set(() => UserType, value);

                if (UserType != null)
                    UserTypeId = UserType.Id;
            }
        }

        public UserViewModel()
        {
            IsValidOnClick = true;

            AddRule(() => FirstName, () => !string.IsNullOrWhiteSpace(FirstName) && FirstName.Length < 50, "Поле не должно быть пустым и содержать больше 50 символов.");
            AddRule(() => MiddleName, () => !string.IsNullOrWhiteSpace(MiddleName) && MiddleName.Length < 50, "Поле не должно быть пустым и содержать больше 50 символов.");
            AddRule(() => Login, () => !string.IsNullOrWhiteSpace(Login) && Login.Length < 50, "Поле не должно быть пустым и содержать больше 50 символов.");
            AddRule(() => Password, () => !string.IsNullOrWhiteSpace(Password) && Password.Length < 50, "Поле не должно быть пустым и содержать больше 50 символов.");
            AddRule(() => Phone, () => Phone.HasValue, "Поле не должно быть пустым.");
            AddRule(() => UserType, () => UserType != null && UserTypeId == UserType.Id && UserType.Valid(), "Поле не должно быть пустым.");
        }
    }
}
