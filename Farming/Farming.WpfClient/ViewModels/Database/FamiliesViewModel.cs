using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class FamiliesViewModel : DatabaseTableViewModel<FamilyViewModel>
    {
        public FamiliesViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user)
            : base("Семейства", user, snackbarMessageQueue)
        {

        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            var families = await db.Families.ToArrayAsync();

            foreach (var family in families)
            {
                Models.Add(new FamilyViewModel()
                {
                    Id = family.Id,
                    Name = family.Name
                });
            }
        }

        protected async override Task AddAsync(FarmingEntities db, FamilyViewModel model)
        {
            var family = new Family()
            {
                Name = model.Name
            };

            db.Families.Add(family);

            await db.SaveChangesAsync();

            model.Id = family.Id;

            Models.Add(model);
        }

        protected async override Task UpdateAsync(FarmingEntities db, FamilyViewModel model)
        {
            var family = await db.Families.FindAsync(model.Id);

            if (family != null)
            {
                family.Name = model.Name;

                await db.SaveChangesAsync();

                var familyVm = Models.FirstOrDefault(f => f.Id == family.Id);

                if (familyVm != null)
                {
                    familyVm.Name = family.Name;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, FamilyViewModel model)
        {
            var family = await db.Families.FindAsync(model.Id);

            if (family != null)
            {
                db.Families.Remove(family);

                await db.SaveChangesAsync();
                Models.Remove(model);
            }
        }
    }
}
