using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class BloodTypesViewModel : DatabaseTableViewModel<BloodTypeViewModel>
    {
        public BloodTypesViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user)
            : base("Группы крови", user, snackbarMessageQueue)
        {

        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            var bloodyTypes = await db.BloodTypes.ToArrayAsync();

            foreach (var bType in bloodyTypes)
            {
                Models.Add(new BloodTypeViewModel()
                {
                    Id = bType.Id,
                    Name = bType.Name
                });
            }
        }

        protected async override Task AddAsync(FarmingEntities db, BloodTypeViewModel model)
        {
            var newBType = new BloodType()
            {
                Name = model.Name
            };

            db.BloodTypes.Add(newBType);

            await db.SaveChangesAsync();

            model.Id = newBType.Id;

            Models.Add(model);
        }

        protected async override Task UpdateAsync(FarmingEntities db, BloodTypeViewModel model)
        {
            var bType = await db.BloodTypes.FindAsync(model.Id);

            if (bType != null)
            {
                bType.Name = model.Name;

                await db.SaveChangesAsync();

                var bTypeVm = Models.FirstOrDefault(t => t.Id == bType.Id);

                if (bTypeVm != null)
                {
                    bTypeVm.Name = model.Name;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, BloodTypeViewModel model)
        {
            var bType = await db.BloodTypes.FindAsync(model.Id);

            if (bType != null)
            {
                db.BloodTypes.Remove(bType);

                await db.SaveChangesAsync();
                Models.Remove(model);
            }
        }
    }
}
