using Farming.WpfClient.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class BullsViewModel : DatabaseTableViewModel<BullViewModel>
    {
        public BullsViewModel()
            : base("Быки")
        {

        }

        public async override Task UpdateAsync()
        {
            if (Models.Any())
                Models.Clear();

            using (var db = new FarmingEntities())
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
                            SorPeriod = rep.SorPeriod,
                            DateOtela = rep.DateOtela,
                            Bull = _bull,
                            BullId = _bull.Id
                        });
                    }

                    Models.Add(_bull);
                }
            }
        }

        protected async override Task AddAsync(BullViewModel model)
        {
            using (var db = new FarmingEntities())
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
        }

        protected async override Task UpdateAsync(BullViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var bull = await db.Bulls.FindAsync(model.Id);

                if (bull != null)
                {
                    bull.Location = model.Location;
                    bull.OtherId = model.OtherId;

                    await db.SaveChangesAsync();
                }
            }
        }

        protected async override Task DeleteAsync(BullViewModel model)
        {
            using (var db = new FarmingEntities())
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
}
