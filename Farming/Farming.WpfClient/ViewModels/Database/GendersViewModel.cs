using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class GendersViewModel : DatabaseTableViewModel<GenderViewModel>
    {
        public GendersViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user)
            : base("Полы", user, snackbarMessageQueue)
        {

        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            var genders = await db.Genders.ToArrayAsync();

            foreach (var gender in genders)
            {
                Models.Add(new GenderViewModel()
                {
                    Id = gender.Id,
                    Name = gender.Name
                });
            }
        }

        protected async override Task AddAsync(FarmingEntities db, GenderViewModel model)
        {
            var gender = new Gender()
            {
                Name = model.Name
            };

            db.Genders.Add(gender);

            await db.SaveChangesAsync();

            model.Id = gender.Id;

            Models.Add(model);
        }

        protected async override Task UpdateAsync(FarmingEntities db, GenderViewModel model)
        {
            var gender = await db.Genders.FindAsync(model.Id);

            if (gender != null)
            {
                gender.Name = model.Name;

                await db.SaveChangesAsync();

                var genderVm = Models.FirstOrDefault(g => g.Id == gender.Id);

                if (genderVm != null)
                {
                    genderVm.Name = model.Name;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, GenderViewModel model)
        {
            var gender = await db.Genders.FindAsync(model.Id);

            if (gender != null)
            {
                db.Genders.Remove(gender);

                await db.SaveChangesAsync();
                Models.Remove(model);
            }
        }
    }
}
