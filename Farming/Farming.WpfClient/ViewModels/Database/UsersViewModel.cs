using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;

namespace Farming.WpfClient.ViewModels.Database
{
    public class UsersViewModel : DatabaseTableViewModel<UserViewModel>
    {
        public UsersTypesViewModel UsersTypesViewModel { get; }

        public UsersViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user, UsersTypesViewModel usersTypesViewModel)
            : base("Пользователи", user, snackbarMessageQueue)
        {
            UsersTypesViewModel = usersTypesViewModel ?? throw new ArgumentNullException(nameof(usersTypesViewModel));
        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            await UsersTypesViewModel.UpdateAsync();

            var users = await db.Users.ToArrayAsync();

            foreach (var user in users)
            {
                Models.Add(new UserViewModel()
                {
                    Id = user.Id,
                    Login = user.Login,
                    Password = user.Password,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    Phone = user.Phone,
                    UserTypeId = user.UserTypeId,
                    UserType = UsersTypesViewModel.Models.FirstOrDefault(t => t.Id == user.UserTypeId)
                });
            }
        }

        protected async override Task AddAsync(FarmingEntities db, UserViewModel model)
        {
            var user = new User()
            {
                Login = model.Login,
                Password = model.Password,
                LastName = model.LastName,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                Phone = model.Phone.Value,
                UserTypeId = model.UserTypeId
            };

            db.Users.Add(user);

            await db.SaveChangesAsync();

            model.Id = user.Id;

            Models.Add(model);
        }

        protected async override Task UpdateAsync(FarmingEntities db, UserViewModel model)
        {
            var user = await db.Users.FindAsync(model.Id);

            if (user != null)
            {
                user.Login = model.Login;
                user.Password = model.Password;
                user.LastName = model.LastName;
                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.Phone = model.Phone.Value;
                user.UserTypeId = model.UserTypeId;

                await db.SaveChangesAsync();

                var userVm = Models.FirstOrDefault(u => u.Id == user.Id);

                if (userVm != null)
                {
                    userVm.Login = model.Login;
                    userVm.Password = model.Password;
                    userVm.LastName = model.LastName;
                    userVm.FirstName = model.FirstName;
                    userVm.MiddleName = model.MiddleName;
                    userVm.Phone = model.Phone.Value;
                    userVm.UserTypeId = model.UserTypeId;
                    userVm.UserType = model.UserType;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, UserViewModel model)
        {
            var user = await db.Users.FindAsync(model.Login);

            if (user != null)
            {
                db.Users.Remove(user);

                await db.SaveChangesAsync();
                Models.Remove(model);
            }
        }
    }
}
