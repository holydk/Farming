using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;

namespace Farming.WpfClient.ViewModels.Database
{
    public class UsersTypesViewModel : DatabaseTableViewModel<UserTypeViewModel>
    {
        public UsersTypesViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user)
            : base("Типы пользователей", user, snackbarMessageQueue)
        {

        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            var usersTypes = await db.UsersTypes.ToArrayAsync();

            foreach (var userType in usersTypes)
            {
                Models.Add(new UserTypeViewModel()
                {
                    Id = userType.Id,
                    Name = userType.Name
                });
            }
        }

        protected override Task AddAsync(FarmingEntities db, UserTypeViewModel model)
        {
            throw new NotImplementedException();
        }

        protected override Task UpdateAsync(FarmingEntities db, UserTypeViewModel model)
        {
            throw new NotImplementedException();
        }

        protected override Task DeleteAsync(FarmingEntities db, UserTypeViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
