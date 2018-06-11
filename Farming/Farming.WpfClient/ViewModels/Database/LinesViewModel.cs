using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class LinesViewModel : DatabaseTableViewModel<LineViewModel>
    {
        public LinesViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user)
            : base("Линии", user, snackbarMessageQueue)
        {

        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            var lines = await db.Lines.ToArrayAsync();

            foreach (var line in lines)
            {
                Models.Add(new LineViewModel()
                {
                    Id = line.Id,
                    Name = line.Name
                });
            }
        }

        protected async override Task AddAsync(FarmingEntities db, LineViewModel model)
        {
            var line = new Line()
            {
                Name = model.Name
            };

            db.Lines.Add(line);

            await db.SaveChangesAsync();

            model.Id = line.Id;

            Models.Add(model);
        }

        protected async override Task UpdateAsync(FarmingEntities db, LineViewModel model)
        {
            var line = await db.Lines.FindAsync(model.Id);

            if (line != null)
            {
                line.Name = model.Name;

                await db.SaveChangesAsync();

                var lineVm = Models.FirstOrDefault(l => l.Id == line.Id);

                if (lineVm != null)
                {
                    lineVm.Name = model.Name;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, LineViewModel model)
        {
            var line = await db.Lines.FindAsync(model.Id);

            if (line != null)
            {
                db.Lines.Remove(line);

                await db.SaveChangesAsync();
                Models.Remove(model);
            }
        }
    }
}
