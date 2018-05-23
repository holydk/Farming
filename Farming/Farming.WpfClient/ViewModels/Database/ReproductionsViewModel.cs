using Farming.WpfClient.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class ReproductionsViewModel : DatabaseTableViewModel<ReproductionViewModel>
    {
        protected CowsViewModel _cowsViewModel;
        public CowsViewModel CowsViewModel => _cowsViewModel;

        protected BullsViewModel _bullsViewModel;
        public BullsViewModel BullsViewModel => _bullsViewModel;

        protected MethodsSluchkiViewModel _methodsSluchkiViewModel;
        public MethodsSluchkiViewModel MethodsSluchkiViewModel => _methodsSluchkiViewModel;

        public ReproductionsViewModel(CowsViewModel cowsViewModel, BullsViewModel bullsViewModel, MethodsSluchkiViewModel methodsSluchkiViewModel)
            : base("Воспроизведение")
        {
            _cowsViewModel = cowsViewModel;
            _bullsViewModel = bullsViewModel;
            _methodsSluchkiViewModel = methodsSluchkiViewModel;
        }

        public async override Task UpdateAsync()
        {
            if (Models.Any())
                Models.Clear();

            await _cowsViewModel.UpdateAsync();
            await _bullsViewModel.UpdateAsync();
            await _methodsSluchkiViewModel.UpdateAsync();

            using (var db = new FarmingEntities())
            {
                var reproductions = await db.Reproductions.ToArrayAsync();

                ReproductionViewModel reproduction;

                foreach (var r in reproductions)
                {
                    reproduction = new ReproductionViewModel()
                    {
                        Id = r.Id,
                        CowId = r.CowId,
                        MethodSluchkiId = r.MethodSluchkiId,
                        BullId = r.BullId,
                        DateOsemeneniya = r.DateOsemeneniya,
                        ChisloSuhihDney = r.ChisloSuhihDney,
                        SorPeriod = r.SorPeriod,
                        DateOtela = r.DateOtela,
                        Bull = _bullsViewModel.Models.First(b => b.Id == r.BullId),
                        Cow = _cowsViewModel.Models.First(c => c.Id == r.CowId),
                        MethodSluchki = _methodsSluchkiViewModel.Models.First(m => m.Id == r.MethodSluchkiId)
                    };
                }
            }
        }

        protected async override Task AddAsync(ReproductionViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var reproduction = new Reproduction()
                {
                    CowId = model.CowId,
                    BullId = model.BullId,
                    MethodSluchkiId = model.MethodSluchkiId,
                    DateOsemeneniya = model.DateOsemeneniya.Value,
                    ChisloSuhihDney = model.ChisloSuhihDney.Value,
                    SorPeriod = model.SorPeriod.Value,
                    DateOtela = model.DateOtela.Value,
                };

                db.Reproductions.Add(reproduction);

                await db.SaveChangesAsync();

                model.Id = reproduction.Id;

                Models.Add(model);
            }
        }

        protected async override Task UpdateAsync(ReproductionViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var reproduction = await db.Reproductions.FindAsync(model.Id);

                if (reproduction != null)
                {
                    reproduction.BullId = model.BullId;
                    reproduction.CowId = model.CowId;
                    reproduction.MethodSluchkiId = model.MethodSluchkiId;
                    reproduction.DateOsemeneniya = model.DateOsemeneniya.Value;
                    reproduction.ChisloSuhihDney = model.ChisloSuhihDney.Value;
                    reproduction.SorPeriod = model.SorPeriod.Value;
                    reproduction.DateOtela = model.DateOtela.Value;

                    await db.SaveChangesAsync();
                }
            }
        }

        protected async override Task DeleteAsync(ReproductionViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var reproduction = await db.Reproductions.FindAsync(model.Id);

                if (reproduction != null)
                {
                    db.Reproductions.Remove(reproduction);

                    await db.SaveChangesAsync();
                    Models.Remove(model);
                }
            }
        }
    }
}
