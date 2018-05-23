using Farming.WpfClient.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class BreedsViewModel : DatabaseTableViewModel<BreedViewModel>
    {
        public BreedsViewModel()
            : base("Породы")
        {

        }

        public async override Task UpdateAsync()
        {
            if (Models.Any())
                Models.Clear();

            using (var db = new FarmingEntities())
            {
                var breeds = await db.Breeds.ToArrayAsync();

                foreach (var breed in breeds)
                {
                    Models.Add(new BreedViewModel()
                    {
                        Id = breed.Id,
                        Name = breed.Name
                    });
                }
            }
        }

        protected async override Task AddAsync(BreedViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var newBType = new Breed()
                {
                    Name = model.Name
                };

                db.Breeds.Add(newBType);

                await db.SaveChangesAsync();

                model.Id = newBType.Id;

                Models.Add(model);
            }
        }

        protected async override Task UpdateAsync(BreedViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var breed = await db.Breeds.FindAsync(model.Id);

                if (breed != null)
                {
                    breed.Name = model.Name;

                    await db.SaveChangesAsync();
                }
            }
        }

        protected async override Task DeleteAsync(BreedViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var breed = await db.Breeds.FindAsync(model.Id);

                if (breed != null)
                {
                    db.Breeds.Remove(breed);

                    await db.SaveChangesAsync();
                    Models.Remove(model);
                }
            }
        }
    }
}
