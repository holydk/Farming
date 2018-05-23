using Farming.WpfClient.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class RetirementsViewModel : DatabaseTableViewModel<RetirementViewModel>
    {
        protected CowsViewModel _cowsViewModel;
        public CowsViewModel CowsViewModel => _cowsViewModel;

        public RetirementsViewModel(CowsViewModel cowsViewModel)
            : base("Выбытие")
        {
            _cowsViewModel = cowsViewModel;
        }

        public async override Task UpdateAsync()
        {
            if (Models.Any())
                Models.Clear();

            await _cowsViewModel.UpdateAsync();

            using (var db = new FarmingEntities())
            {
                var retirements = await db.Retirements.ToArrayAsync();

                RetirementViewModel retirement;

                foreach (var r in retirements)
                {
                    retirement = new RetirementViewModel()
                    {
                        Id = r.Id,
                        CowId = r.CowId,
                        Date = r.Date,
                        Reason = r.Reason,
                        Cow = _cowsViewModel.Models.First(c => c.Id == r.CowId)
                    };

                    Models.Add(retirement);
                }
            }
        }

        protected async override Task AddAsync(RetirementViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var retirement = new Retirement()
                {
                    CowId = model.CowId,
                    Date = model.Date.Value,
                    Reason = model.Reason
                };

                db.Retirements.Add(retirement);

                await db.SaveChangesAsync();

                model.Id = retirement.Id;

                Models.Add(model);
            }
        }

        protected async override Task UpdateAsync(RetirementViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var retirement = await db.Retirements.FindAsync(model.Id);

                if (retirement != null)
                {
                    retirement.CowId = model.CowId;
                    retirement.Date = model.Date.Value;
                    retirement.Reason = model.Reason;

                    await db.SaveChangesAsync();
                }
            }
        }

        protected async override Task DeleteAsync(RetirementViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var retirement = await db.Retirements.FindAsync(model.Id);

                if (retirement != null)
                {
                    db.Retirements.Remove(retirement);

                    await db.SaveChangesAsync();
                    Models.Remove(model);
                }
            }
        }
    }
}
