using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class CategoriesViewModel : DatabaseTableViewModel<CategoryViewModel>
    {
        public CategoriesViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user)
            : base("Категории", user, snackbarMessageQueue)
        {

        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            var categories = await db.Categories.ToArrayAsync();

            foreach (var category in categories)
            {
                Models.Add(new CategoryViewModel()
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }
        }

        protected async override Task AddAsync(FarmingEntities db, CategoryViewModel model)
        {
            var newBType = new Category()
            {
                Name = model.Name
            };

            db.Categories.Add(newBType);

            await db.SaveChangesAsync();

            model.Id = newBType.Id;

            Models.Add(model);
        }

        protected async override Task UpdateAsync(FarmingEntities db, CategoryViewModel model)
        {
            var category = await db.Categories.FindAsync(model.Id);

            if (category != null)
            {
                category.Name = model.Name;

                await db.SaveChangesAsync();

                var categoryVm = Models.FirstOrDefault(c => c.Id == category.Id);

                if (categoryVm != null)
                {
                    categoryVm.Name = model.Name;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, CategoryViewModel model)
        {
            var category = await db.Categories.FindAsync(model.Id);

            if (category != null)
            {
                db.Categories.Remove(category);

                await db.SaveChangesAsync();
                Models.Remove(model);
            }
        }
    }
}
