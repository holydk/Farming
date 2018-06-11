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
    public class PriplodsViewModel : DatabaseTableViewModel<PriplodViewModel>
    {
        protected CowsViewModel _cowsViewModel;
        public CowsViewModel CowsViewModel => _cowsViewModel;

        protected GendersViewModel _gendersViewModel;
        public GendersViewModel GendersViewModel => _gendersViewModel;

        public PriplodsViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user, CowsViewModel cowsViewModel, GendersViewModel gendersViewModel)
            : this(snackbarMessageQueue, user, cowsViewModel, gendersViewModel, null)
        {
        }

        public PriplodsViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user, CowsViewModel cowsViewModel, GendersViewModel gendersViewModel, IEnumerable<PriplodViewModel> models)
            : base("Приплоды", user, snackbarMessageQueue, models)
        {
            _cowsViewModel = cowsViewModel;
            _gendersViewModel = gendersViewModel;
        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            await _cowsViewModel.UpdateAsync();
            await _gendersViewModel.UpdateAsync();

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
                    Gender = _gendersViewModel.Models.FirstOrDefault(g => g.Id == p.GenderId),
                    Mother = _cowsViewModel.Models.FirstOrDefault(c => c.Id == p.CowId)
                };

                Models.Add(priplod);
            }
        }

        protected async override Task AddAsync(FarmingEntities db, PriplodViewModel model)
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

        protected async override Task UpdateAsync(FarmingEntities db, PriplodViewModel model)
        {
            var priplod = await db.Priplods.FindAsync(model.Id);

            if (priplod != null)
            {
                priplod.CowId = model.CowId;
                priplod.GenderId = model.GenderId;
                priplod.Weight = model.Weight.Value;

                await db.SaveChangesAsync();

                var priplodVm = Models.FirstOrDefault(p => p.Id == priplod.Id);

                if (priplodVm != null)
                {
                    priplodVm.CowId = model.CowId;
                    priplodVm.Mother = model.Mother;
                    priplodVm.GenderId = model.GenderId;
                    priplodVm.Gender = model.Gender;
                    priplodVm.Weight = model.Weight;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, PriplodViewModel model)
        {
            var priplod = await db.Priplods.FindAsync(model.Id);

            if (priplod != null)
            {
                db.Priplods.Remove(priplod);

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
                var model = item as PriplodViewModel;

                return _gendersViewModel.SelectedModel?.Name == model?.Gender.Name;
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

            _gendersViewModel.SelectedModel = null;

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
                    table.AddTitleTable($"{model.Id} / {model.Gender?.Name} / {model.Weight}", fontHeader, 2);

                    table.AddCell("Идентификатор", font);
                    table.AddCell(model.Id.ToString(), font);

                    table.AddCell("Пол", font);
                    table.AddCell(model.Gender?.Name, font);

                    table.AddCell("Вес", font);
                    table.AddCell(model.Weight.ToString(), font);

                    table.AddHeaderColumn("Мать", fontBold, 2);

                    table.AddCell("Идентификатор", font, 0, 15);
                    table.AddCell(model.Mother?.Id.ToString(), font, 0, 15);

                    table.AddCell("Имя", font, 0, 15);
                    table.AddCell(model.Mother?.Name, font, 0, 15);
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
