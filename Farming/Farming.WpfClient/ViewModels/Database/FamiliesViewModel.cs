using Farming.WpfClient.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class FamiliesViewModel : DatabaseTableViewModel<FamilyViewModel>
    {
        public FamiliesViewModel()
            : base("Семейства")
        {

        }

        public async override Task UpdateAsync()
        {
            if (Models.Any())
                Models.Clear();

            using (var db = new FarmingEntities())
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
        }

        protected async override Task AddAsync(FamilyViewModel model)
        {
            using (var db = new FarmingEntities())
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
        }

        protected async override Task UpdateAsync(FamilyViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var family = await db.Families.FindAsync(model.Id);

                if (family != null)
                {
                    family.Name = model.Name;

                    await db.SaveChangesAsync();
                }
            }
        }

        protected async override Task DeleteAsync(FamilyViewModel model)
        {
            using (var db = new FarmingEntities())
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
}
