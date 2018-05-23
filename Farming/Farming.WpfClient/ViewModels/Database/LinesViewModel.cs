using Farming.WpfClient.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class LinesViewModel : DatabaseTableViewModel<LineViewModel>
    {
        public LinesViewModel()
            : base("Линии")
        {

        }

        public async override Task UpdateAsync()
        {
            if (Models.Any())
                Models.Clear();

            using (var db = new FarmingEntities())
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
        }

        protected async override Task AddAsync(LineViewModel model)
        {
            using (var db = new FarmingEntities())
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
        }

        protected async override Task UpdateAsync(LineViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var line = await db.Lines.FindAsync(model.Id);

                if (line != null)
                {
                    line.Name = model.Name;

                    await db.SaveChangesAsync();
                }
            }
        }

        protected async override Task DeleteAsync(LineViewModel model)
        {
            using (var db = new FarmingEntities())
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
}
