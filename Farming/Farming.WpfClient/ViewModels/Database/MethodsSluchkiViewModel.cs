using Farming.WpfClient.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class MethodsSluchkiViewModel : DatabaseTableViewModel<MethodSluchkiViewModel>
    {
        public MethodsSluchkiViewModel()
            : base("Методы случки")
        {

        }

        public async override Task UpdateAsync()
        {
            if (Models.Any())
                Models.Clear();

            using (var db = new FarmingEntities())
            {
                var methodsSluchki = await db.MethodsSluchki.ToArrayAsync();

                foreach (var methodSluchki in methodsSluchki)
                {
                    Models.Add(new MethodSluchkiViewModel()
                    {
                        Id = methodSluchki.Id,
                        Name = methodSluchki.Name
                    });
                }
            }
        }

        protected async override Task AddAsync(MethodSluchkiViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var methodSluchki = new MethodSluchki()
                {
                    Name = model.Name
                };

                db.MethodsSluchki.Add(methodSluchki);

                await db.SaveChangesAsync();

                model.Id = methodSluchki.Id;

                Models.Add(model);
            }
        }

        protected async override Task UpdateAsync(MethodSluchkiViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var methodSluchki = await db.MethodsSluchki.FindAsync(model.Id);

                if (methodSluchki != null)
                {
                    methodSluchki.Name = model.Name;

                    await db.SaveChangesAsync();
                }
            }
        }

        protected async override Task DeleteAsync(MethodSluchkiViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var methodSluchki = await db.MethodsSluchki.FindAsync(model.Id);

                if (methodSluchki != null)
                {
                    db.MethodsSluchki.Remove(methodSluchki);

                    await db.SaveChangesAsync();
                    Models.Remove(model);
                }
            }
        }
    }
}
