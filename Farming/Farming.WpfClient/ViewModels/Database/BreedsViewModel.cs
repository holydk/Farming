using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class BreedsViewModel : DatabaseTableViewModel<BreedViewModel>
    {
        public BreedsViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user)
            : base("Породы", user, snackbarMessageQueue)
        {

        }

        protected async override Task UpdateAsync(FarmingEntities db)
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

        protected async override Task AddAsync(FarmingEntities db, BreedViewModel model)
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

        protected async override Task UpdateAsync(FarmingEntities db, BreedViewModel model)
        {
            var breed = await db.Breeds.FindAsync(model.Id);

            if (breed != null)
            {
                breed.Name = model.Name;

                await db.SaveChangesAsync();

                var breedVm = Models.FirstOrDefault(b => b.Id == breed.Id);

                if (breedVm != null)
                {
                    breedVm.Name = model.Name;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, BreedViewModel model)
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
