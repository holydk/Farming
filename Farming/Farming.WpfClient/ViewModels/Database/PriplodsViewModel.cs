using Farming.WpfClient.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class PriplodsViewModel : DatabaseTableViewModel<PriplodViewModel>
    {
        protected CowsViewModel _cowsViewModel;
        public CowsViewModel CowsViewModel => _cowsViewModel;

        protected GendersViewModel _gendersViewModel;
        public GendersViewModel GendersViewModel => _gendersViewModel;

        public PriplodsViewModel(CowsViewModel cowsViewModel, GendersViewModel gendersViewModel)
            : base("Приплоды")
        {
            _cowsViewModel = cowsViewModel;
            _gendersViewModel = gendersViewModel;
        }

        public async override Task UpdateAsync()
        {
            if (Models.Any())
                Models.Clear();

            await _cowsViewModel.UpdateAsync();
            await _gendersViewModel.UpdateAsync();

            using (var db = new FarmingEntities())
            {
                var priplods = await db.Priplods.ToArrayAsync();

                PriplodViewModel priplod;

                foreach (var p in priplods)
                {
                    priplod = new PriplodViewModel()
                    {
                        Id = p.Id,
                        CowId = p.CowId,
                        GenderId = p.GenderId,
                        Weight = p.Weight,
                        Gender = _gendersViewModel.Models.First(g => g.Id == p.GenderId),
                        Cow = _cowsViewModel.Models.First(c => c.Id == p.CowId)
                    };

                    Models.Add(priplod);
                }
            }
        }

        protected async override Task AddAsync(PriplodViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var priplod = new Priplod()
                {
                    CowId = model.CowId,
                    GenderId = model.GenderId,
                    Weight = model.Weight.Value
                };

                db.Priplods.Add(priplod);

                await db.SaveChangesAsync();

                model.Id = priplod.Id;

                Models.Add(model);
            }
        }

        protected async override Task UpdateAsync(PriplodViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var priplod = await db.Priplods.FindAsync(model.Id);

                if (priplod != null)
                {
                    priplod.CowId = model.CowId;
                    priplod.GenderId = model.GenderId;
                    priplod.Weight = model.Weight.Value;

                    await db.SaveChangesAsync();
                }
            }
        }

        protected async override Task DeleteAsync(PriplodViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var priplod = await db.Priplods.FindAsync(model.Id);

                if (priplod != null)
                {
                    db.Priplods.Remove(priplod);

                    await db.SaveChangesAsync();
                    Models.Remove(model);
                }
            }
        }
    }
}
