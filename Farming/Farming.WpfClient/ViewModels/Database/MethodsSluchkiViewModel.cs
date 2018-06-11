using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class MethodsSluchkiViewModel : DatabaseTableViewModel<MethodSluchkiViewModel>
    {
        public MethodsSluchkiViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user)
            : base("Методы случки", user, snackbarMessageQueue)
        {

        }

        protected async override Task UpdateAsync(FarmingEntities db)
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

        protected async override Task AddAsync(FarmingEntities db, MethodSluchkiViewModel model)
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

        protected async override Task UpdateAsync(FarmingEntities db, MethodSluchkiViewModel model)
        {
            var methodSluchki = await db.MethodsSluchki.FindAsync(model.Id);

            if (methodSluchki != null)
            {
                methodSluchki.Name = model.Name;

                await db.SaveChangesAsync();

                var methodSluchkiVm = Models.FirstOrDefault(m => m.Id == methodSluchki.Id);

                if (methodSluchkiVm != null)
                {
                    methodSluchkiVm.Name = model.Name;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, MethodSluchkiViewModel model)
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
