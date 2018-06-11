using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class BullsViewModel : DatabaseTableViewModel<BullViewModel>
    {
        public BullsViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user)
            : base("Быки", user, snackbarMessageQueue)
        {

        }

        public BullsViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user, IEnumerable<BullViewModel> models)
            : base("Быки", user, snackbarMessageQueue, models)
        {

        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            var bulls = await db.Bulls.ToArrayAsync();
            var cows = await db.Cows.ToArrayAsync();
            var rpds = await db.Reproductions.ToArrayAsync();

            BullViewModel _bull;

            foreach (var bull in bulls)
            {
                _bull = new BullViewModel()
                {
                    Id = bull.Id,
                    Location = bull.Location,
                    OtherId = bull.OtherId
                };

                foreach (var children in cows.Where(c => c.FatherId == bull.Id))
                {
                    _bull.Childrens.Add(new CowViewModel()
                    {
                        Id = children.Id,
                        Name = children.Name,
                        Porodnost = children.Porodnost,
                        Weight = children.Weight,
                        Age = children.Age,
                        BPlace = children.BPlace,
                        BDay = children.BDay,
                        FatherId = _bull.Id,
                        Father = _bull
                    });
                }

                foreach (var rep in rpds.Where(r => r.BullId == bull.Id))
                {
                    _bull.Reproductions.Add(new ReproductionViewModel()
                    {
                        Id = rep.Id,
                        DateOsemeneniya = rep.DateOsemeneniya,
                        ChisloSuhihDney = rep.ChisloSuhihDney,
                        SerPeriod = rep.SerPeriod,
                        DateOtela = rep.DateOtela,
                        Bull = _bull,
                        BullId = _bull.Id
                    });
                }

                Models.Add(_bull);
            }
        }

        protected async override Task AddAsync(FarmingEntities db, BullViewModel model)
        {
            var bull = new Bull()
            {
                Location = model.Location,
                OtherId = model.OtherId
            };

            db.Bulls.Add(bull);

            await db.SaveChangesAsync();

            model.Id = bull.Id;

            Models.Add(model);
        }

        protected async override Task UpdateAsync(FarmingEntities db, BullViewModel model)
        {
            var bull = await db.Bulls.FindAsync(model.Id);

            if (bull != null)
            {
                bull.Location = model.Location;
                bull.OtherId = model.OtherId;

                await db.SaveChangesAsync();

                var bullVm = Models.FirstOrDefault(b => b.Id == bull.Id);

                if (bullVm != null)
                {
                    bullVm.Location = model.Location;
                    bullVm.OtherId = model.OtherId;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, BullViewModel model)
        {
            var bull = await db.Bulls.FindAsync(model.Id);

            if (bull != null)
            {
                db.Bulls.Remove(bull);

                await db.SaveChangesAsync();
                Models.Remove(model);
            }
        }
    }
}
