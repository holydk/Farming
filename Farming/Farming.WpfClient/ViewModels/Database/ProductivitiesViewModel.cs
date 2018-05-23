using Farming.WpfClient.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class ProductivitiesViewModel : DatabaseTableViewModel<ProductivityViewModel>
    {
        protected CowsViewModel _cowsViewModel;
        public CowsViewModel CowsViewModel => _cowsViewModel;

        public ProductivitiesViewModel(CowsViewModel cowsViewModel)
            : base("Продуктивность")
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
                var productivities = await db.Productivities.ToArrayAsync();

                ProductivityViewModel productivity;

                foreach (var p in productivities)
                {
                    productivity = new ProductivityViewModel()
                    {
                        Id = p.Id,
                        CowId = p.CowId,
                        UchtenoLaktacij = p.UchtenoLaktacij,
                        UdojKg = p.UdojKg,
                        ZhirProz = p.ZhirProz,
                        BelokProz = p.BelokProz,
                        Date = p.Date,
                        Cow = _cowsViewModel.Models.First(c => c.Id == p.CowId)
                    };

                    Models.Add(productivity);
                }
            }
        }

        protected async override Task AddAsync(ProductivityViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var productivity = new Productivity()
                {
                    CowId = model.CowId,
                    UchtenoLaktacij = model.UchtenoLaktacij.Value,
                    UdojKg = model.UdojKg.Value,
                    ZhirProz = model.ZhirProz.Value,
                    BelokProz = model.BelokProz.Value,
                    Date = model.Date.Value
                };

                db.Productivities.Add(productivity);

                await db.SaveChangesAsync();

                model.Id = productivity.Id;

                Models.Add(model);
            }
        }

        protected async override Task UpdateAsync(ProductivityViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var productivity = await db.Productivities.FindAsync(model.Id);

                if (productivity != null)
                {
                    productivity.CowId = model.CowId;
                    productivity.UchtenoLaktacij = model.UchtenoLaktacij.Value;
                    productivity.UdojKg = model.UdojKg.Value;
                    productivity.ZhirProz = model.ZhirProz.Value;
                    productivity.BelokProz = model.BelokProz.Value;
                    productivity.Date = model.Date.Value;

                    await db.SaveChangesAsync();
                }
            }
        }

        protected async override Task DeleteAsync(ProductivityViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var productivity = await db.Productivities.FindAsync(model.Id);

                if (productivity != null)
                {
                    db.Productivities.Remove(productivity);

                    await db.SaveChangesAsync();
                    Models.Remove(model);
                }
            }
        }
    }
}
