using Farming.WpfClient.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels.Database
{
    public class CowsViewModel : DatabaseTableViewModel<CowViewModel>
    {
        protected BullsViewModel _bullsViewModel;
        public BullsViewModel BullsViewModel => _bullsViewModel;

        protected BloodTypesViewModel _bloodTypesViewModel;
        public BloodTypesViewModel BloodTypesViewModel => _bloodTypesViewModel;

        protected CategoriesViewModel _categoriesViewModel;
        public CategoriesViewModel CategoriesViewModel => _categoriesViewModel;

        protected LinesViewModel _linesViewModel;
        public LinesViewModel LinesViewModel => _linesViewModel;

        protected BreedsViewModel _breedsViewModel;
        public BreedsViewModel BreedsViewModel => _breedsViewModel;

        protected FamiliesViewModel _familiesViewModel;
        public FamiliesViewModel FamiliesViewModel => _familiesViewModel;

        public CowsViewModel(BullsViewModel bullsViewModel, BloodTypesViewModel bloodTypesViewModel, CategoriesViewModel categoriesViewModel, LinesViewModel linesViewModel, BreedsViewModel breedsViewModel, FamiliesViewModel familiesViewModel)
            : base("Коровы")
        {
            _bullsViewModel = bullsViewModel;
            _bloodTypesViewModel = bloodTypesViewModel;
            _categoriesViewModel = categoriesViewModel;
            _linesViewModel = linesViewModel;
            _breedsViewModel = breedsViewModel;
            _familiesViewModel = familiesViewModel;
        }

        public async override Task UpdateAsync()
        {
            if (Models.Any())
                Models.Clear();

            await _bullsViewModel.UpdateAsync();
            await _bloodTypesViewModel.UpdateAsync();
            await _categoriesViewModel.UpdateAsync();
            await _linesViewModel.UpdateAsync();
            await _familiesViewModel.UpdateAsync();

            using (var db = new FarmingEntities())
            {
                var cows = await db.Cows.ToArrayAsync();

                CowViewModel cow;

                foreach (var c in cows)
                {
                    cow = new CowViewModel()
                    {
                        Id = c.Id,
                        BreedId = c.BreedId,
                        LineId = c.LineId,
                        FamilyId = c.FamilyId,
                        CategoryId = c.CategoryId,
                        BloodTypeId = c.BloodTypeId,
                        MotherId = c.MotherId,
                        FatherId = c.FatherId,
                        Porodnost = c.Porodnost,
                        Name = c.Name,
                        BDay = c.BDay,
                        BPlace = c.BPlace,
                        Weight = c.Weight,
                        Age = c.Age,
                        IsInHerd = c.IsInHerd,
                        Father = _bullsViewModel.Models.First(b => b.Id == c.FatherId),
                        BloodType = _bloodTypesViewModel.Models.First(b => b.Id == c.BloodTypeId),
                        Category = _categoriesViewModel.Models.First(_c => _c.Id == c.CategoryId),
                        Line = _linesViewModel.Models.First(l => l.Id == c.LineId),
                        Breed = _breedsViewModel.Models.First(b => b.Id == c.BreedId),
                        Family = _familiesViewModel.Models.First(f => f.Id == c.FamilyId)
                    };

                    Models.Add(cow);
                }

                foreach (var c in Models)
                {
                    c.Mother = Models.First(m => m.Id == c.MotherId);
                }
            }
        }

        protected async override Task AddAsync(CowViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var cow = new Cow()
                {
                    BreedId = model.BreedId,
                    LineId = model.LineId,
                    FamilyId = model.FamilyId,
                    CategoryId = model.CategoryId,
                    BloodTypeId = model.BloodTypeId,
                    MotherId = model.MotherId,
                    FatherId = model.FatherId,
                    Porodnost = model.Porodnost.Value,
                    Name = model.Name,
                    BDay = model.BDay.Value,
                    BPlace = model.BPlace,
                    Weight = model.Weight.Value,
                    Age = model.Age.Value,
                    IsInHerd = model.IsInHerd.Value,
                };

                db.Cows.Add(cow);

                await db.SaveChangesAsync();

                model.Id = cow.Id;

                Models.Add(model);
            }
        }

        protected async override Task UpdateAsync(CowViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var cow = await db.Cows.FindAsync(model.Id);

                if (cow != null)
                {
                    cow.BreedId = model.BreedId;
                    cow.LineId = model.LineId;
                    cow.FamilyId = model.FamilyId;
                    cow.CategoryId = model.CategoryId;
                    cow.BloodTypeId = model.BloodTypeId;
                    cow.MotherId = model.MotherId;
                    cow.FatherId = model.FatherId;
                    cow.Porodnost = model.Porodnost.Value;
                    cow.Name = model.Name;
                    cow.BDay = model.BDay.Value;
                    cow.BPlace = model.BPlace;
                    cow.Weight = model.Weight.Value;
                    cow.Age = model.Age.Value;
                    cow.IsInHerd = model.IsInHerd.Value;

                    await db.SaveChangesAsync();
                }
            }
        }

        protected async override Task DeleteAsync(CowViewModel model)
        {
            using (var db = new FarmingEntities())
            {
                var cow = await db.Cows.FindAsync(model.Id);

                if (cow != null)
                {
                    db.Cows.Remove(cow);

                    await db.SaveChangesAsync();
                    Models.Remove(model);
                }
            }
        }
    }
}
