using Farming.WpfClient.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class CategoriesViewModel : DatabaseTableViewModel<CategoryViewModel>
    {
        public CategoriesViewModel()
            : base("Категории")
        {

        }

        public async override Task UpdateAsync()
        {
            if (Models.Any())
                Models.Clear();

            using (var db = new FarmingEntities())
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
        }

        protected async override Task AddAsync(CategoryViewModel model)
        {
            using (var db = new FarmingEntities())
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
        }

        protected async override Task UpdateAsync(CategoryViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var category = await db.Categories.FindAsync(model.Id);

                if (category != null)
                {
                    category.Name = model.Name;

                    await db.SaveChangesAsync();
                }
            }
        }

        protected async override Task DeleteAsync(CategoryViewModel model)
        {
            using (var db = new FarmingEntities())
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
}
