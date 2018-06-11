using Farming.WpfClient.Helpers;
using Farming.WpfClient.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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

        public CowsViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user, BullsViewModel bullsViewModel, BloodTypesViewModel bloodTypesViewModel, CategoriesViewModel categoriesViewModel, LinesViewModel linesViewModel, BreedsViewModel breedsViewModel, FamiliesViewModel familiesViewModel)
            : this(snackbarMessageQueue, null, user, bullsViewModel, bloodTypesViewModel, categoriesViewModel, linesViewModel, breedsViewModel, familiesViewModel)
        {
            
        }

        public CowsViewModel(ISnackbarMessageQueue snackbarMessageQueue, IEnumerable<CowViewModel> models, User user, BullsViewModel bullsViewModel, BloodTypesViewModel bloodTypesViewModel, CategoriesViewModel categoriesViewModel, LinesViewModel linesViewModel, BreedsViewModel breedsViewModel, FamiliesViewModel familiesViewModel)
            : base("Коровы", user, snackbarMessageQueue, models)
        {
            _bullsViewModel = bullsViewModel;
            _bloodTypesViewModel = bloodTypesViewModel;
            _categoriesViewModel = categoriesViewModel;
            _linesViewModel = linesViewModel;
            _breedsViewModel = breedsViewModel;
            _familiesViewModel = familiesViewModel;
        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            await _bullsViewModel.UpdateAsync();
            await _bloodTypesViewModel.UpdateAsync();
            await _categoriesViewModel.UpdateAsync();
            await _linesViewModel.UpdateAsync();
            await _familiesViewModel.UpdateAsync();
            await _breedsViewModel.UpdateAsync();

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
                    Father = _bullsViewModel.Models.FirstOrDefault(b => b.Id == c.FatherId),
                    BloodType = _bloodTypesViewModel.Models.FirstOrDefault(b => b.Id == c.BloodTypeId),
                    Category = _categoriesViewModel.Models.FirstOrDefault(_c => _c.Id == c.CategoryId),
                    Line = _linesViewModel.Models.FirstOrDefault(l => l.Id == c.LineId),
                    Breed = _breedsViewModel.Models.FirstOrDefault(b => b.Id == c.BreedId),
                    Family = _familiesViewModel.Models.FirstOrDefault(f => f.Id == c.FamilyId)
                };

                Models.Add(cow);
            }

            foreach (var c in Models)
            {
                c.Mother = Models.FirstOrDefault(m => m.Id == c.MotherId);
            }
        }

        protected async override Task AddAsync(FarmingEntities db, CowViewModel model)
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

        protected async override Task UpdateAsync(FarmingEntities db, CowViewModel model)
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

                var cowVm = Models.FirstOrDefault(c => c.Id == cow.Id);

                if (cowVm != null)
                {
                    cowVm.BreedId = model.BreedId;
                    cowVm.LineId = model.LineId;
                    cowVm.FamilyId = model.FamilyId;
                    cowVm.CategoryId = model.CategoryId;
                    cowVm.BloodTypeId = model.BloodTypeId;
                    cowVm.MotherId = model.MotherId;
                    cowVm.FatherId = model.FatherId;
                    cowVm.Porodnost = model.Porodnost.Value;
                    cowVm.Name = model.Name;
                    cowVm.BDay = model.BDay.Value;
                    cowVm.BPlace = model.BPlace;
                    cowVm.Weight = model.Weight.Value;
                    cowVm.Age = model.Age.Value;
                    cowVm.IsInHerd = model.IsInHerd.Value;
                    cowVm.Breed = model.Breed;
                    cowVm.Line = model.Line;
                    cowVm.Family = model.Family;
                    cowVm.Category = model.Category;
                    cowVm.BloodType = model.BloodType;
                    cowVm.Mother = model.Mother;
                    cowVm.Father = model.Father;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, CowViewModel model)
        {
            var cow = await db.Cows.FindAsync(model.Id);

            if (cow != null)
            {
                db.Cows.Remove(cow);

                await db.SaveChangesAsync();
                Models.Remove(model);
            }
        }

        public override void ApplyFilter(params string[] by)
        {
            if (IsBusy) return;

            IsBusy = true;

            _filterPredicate = item =>
            {
                var model = item as CowViewModel;

                return _bloodTypesViewModel.SelectedModel?.Name == model?.BloodType.Name ||
                _categoriesViewModel.SelectedModel?.Name == model?.Category.Name ||
                _linesViewModel.SelectedModel?.Name == model?.Line.Name ||
                _breedsViewModel.SelectedModel?.Name == model?.Breed.Name ||
                _familiesViewModel.SelectedModel?.Name == model?.Family.Name;
            };

            _modelsView.Filter = model => _filterPredicate.Invoke(model) &&
                (_searchPredicate == null ? true : _searchPredicate.Invoke(model));

            IsBusy = false;
        }

        public override void ClearFilter()
        {
            if (IsBusy) return;

            IsBusy = true;

            _filterPredicate = null;

            if (_searchPredicate != null)
            {
                _modelsView.Filter = _searchPredicate;
            }
            else
            {
                _modelsView.Filter = null;
            }

            _bloodTypesViewModel.SelectedModel = null;
            _categoriesViewModel.SelectedModel = null;
            _linesViewModel.SelectedModel = null;
            _breedsViewModel.SelectedModel = null;
            _familiesViewModel.SelectedModel = null;

            IsBusy = false;
        }

        public override void SaveAs(string fileName)
        {
            PdfWriter writer = null;
            var doc = new Document();

            try
            {
                writer = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));

                doc.Open();

                var baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                var font = new Font(baseFont, 12, Font.NORMAL);
                var fontBold = new Font(baseFont, 12, Font.BOLD);
                var fontHeader = new Font(baseFont, 14, Font.BOLD);
                var fontTitle = new Font(baseFont, 18, Font.NORMAL);

                doc.Add(new Paragraph(Title, fontTitle));

                var table = new PdfPTable(2)
                {
                    SpacingBefore = 50
                };

                foreach (var model in Models)
                {
                    table.AddTitleTable($"{model.Id} / {model.Name}", fontHeader, 2);

                    table.AddCell("Идентификатор", font);
                    table.AddCell(model.Id.ToString(), font);

                    table.AddCell("Имя", font);
                    table.AddCell(model.Name, font);

                    table.AddCell("Породность", font);
                    table.AddCell(model.Porodnost.ToString(), font);

                    table.AddCell("Дата рождения", font);
                    table.AddCell(model.BDay.ToString(), font);

                    table.AddCell("Место рождения", font);
                    table.AddCell(model.BPlace, font);

                    table.AddCell("Вес", font);
                    table.AddCell(model.Weight.ToString(), font);

                    table.AddCell("Возраст", font);
                    table.AddCell(model.Age.ToString(), font);

                    table.AddCell("В стаде", font);
                    table.AddCell((model.IsInHerd == true ? "Да" : "Нет"), font);

                    table.AddCell("Порода", font);
                    table.AddCell(model.Breed?.Name, font);

                    table.AddCell("Линия", font);
                    table.AddCell(model.Line?.Name, font);

                    table.AddCell("Семейство", font);
                    table.AddCell(model.Family?.Name, font);

                    table.AddCell("Категория", font);
                    table.AddCell(model.Category?.Name, font);

                    table.AddCell("Группа крови", font);
                    table.AddCell(model.BloodType?.Name, font);

                    table.AddHeaderColumn("Мать", fontBold, 2);

                    table.AddCell("Идентификатор", font, 0, 15);
                    table.AddCell(model.Mother?.Id.ToString(), font, 0, 15);

                    table.AddCell("Имя", font, 0, 15);
                    table.AddCell(model.Mother?.Name, font, 0, 15);

                    table.AddHeaderColumn("Отец", fontBold, 2);

                    table.AddCell("Идентификатор", font, 0, 15);
                    table.AddCell(model.Father?.Id.ToString(), font, 0, 15);

                    table.AddCell("Месторасположение", font, 0, 15);
                    table.AddCell(model.Father?.Location.ToString(), font, 0, 15);

                    table.AddCell("Сторонний идентификатор", font, 0, 15);
                    table.AddCell(model.Father?.OtherId.ToString(), font, 0, 15);
                }

                doc.Add(table);
            }
            finally
            {
                writer?.Flush();
                doc.Close();

                writer = null;
                doc = null;
            }
        }
    }
}
