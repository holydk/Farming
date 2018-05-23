using Farming.WpfClient.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class GendersViewModel : DatabaseTableViewModel<GenderViewModel>
    {
        public GendersViewModel()
            : base("Полы")
        {

        }

        public async override Task UpdateAsync()
        {
            if (Models.Any())
                Models.Clear();

            using (var db = new FarmingEntities())
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
        }

        protected async override Task AddAsync(GenderViewModel model)
        {
            using (var db = new FarmingEntities())
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
        }

        protected async override Task UpdateAsync(GenderViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var gender = await db.Genders.FindAsync(model.Id);

                if (gender != null)
                {
                    gender.Name = model.Name;

                    await db.SaveChangesAsync();
                }
            }
        }

        protected async override Task DeleteAsync(GenderViewModel model)
        {
            using (var db = new FarmingEntities())
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
}
