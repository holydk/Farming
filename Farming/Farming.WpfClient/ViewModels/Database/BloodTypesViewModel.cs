using Farming.WpfClient.Models;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class BloodTypesViewModel : DatabaseTableViewModel<BloodTypeViewModel>
    {
        public BloodTypesViewModel()
            : base("Группы крови")
        {

        }

        public async override Task UpdateAsync()
        {
            if (Models.Any())
                Models.Clear();

            using (var db = new FarmingEntities())
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
        }

        protected async override Task AddAsync(BloodTypeViewModel model)
        {
            using (var db = new FarmingEntities())
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
        }

        protected async override Task UpdateAsync(BloodTypeViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var bType = await db.BloodTypes.FindAsync(model.Id);

                if (bType != null)
                {
                    bType.Name = model.Name;

                    await db.SaveChangesAsync();
                }
            }
        }

        protected async override Task DeleteAsync(BloodTypeViewModel model)
        {
            using (var db = new FarmingEntities())
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
}
