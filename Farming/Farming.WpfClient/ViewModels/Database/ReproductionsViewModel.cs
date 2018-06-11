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
    public class ReproductionsViewModel : DatabaseTableViewModel<ReproductionViewModel>
    {
        protected CowsViewModel _cowsViewModel;
        public CowsViewModel CowsViewModel => _cowsViewModel;

        protected MethodsSluchkiViewModel _methodsSluchkiViewModel;
        public MethodsSluchkiViewModel MethodsSluchkiViewModel => _methodsSluchkiViewModel;

        public ReproductionsViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user, CowsViewModel cowsViewModel, MethodsSluchkiViewModel methodsSluchkiViewModel)
            : this(snackbarMessageQueue, user, cowsViewModel, methodsSluchkiViewModel, null)
        {
        }

        public ReproductionsViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user, CowsViewModel cowsViewModel, MethodsSluchkiViewModel methodsSluchkiViewModel, IEnumerable<ReproductionViewModel> models)
          : base("Воспроизведение", user, snackbarMessageQueue, models)
        {
            _cowsViewModel = cowsViewModel;
            _methodsSluchkiViewModel = methodsSluchkiViewModel;
        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            await _cowsViewModel.UpdateAsync();
            await _methodsSluchkiViewModel.UpdateAsync();

            var reproductions = await db.Reproductions.ToArrayAsync();

            foreach (var r in reproductions)
            {
                Models.Add(new ReproductionViewModel()
                {
                    Id = r.Id,
                    CowId = r.CowId,
                    MethodSluchkiId = r.MethodSluchkiId,
                    BullId = r.BullId,
                    DateOsemeneniya = r.DateOsemeneniya,
                    ChisloSuhihDney = r.ChisloSuhihDney,
                    SerPeriod = r.SerPeriod,
                    DateOtela = r.DateOtela,
                    Bull = _cowsViewModel.BullsViewModel.Models.FirstOrDefault(b => b.Id == r.BullId),
                    Cow = _cowsViewModel.Models.FirstOrDefault(c => c.Id == r.CowId),
                    MethodSluchki = _methodsSluchkiViewModel.Models.FirstOrDefault(m => m.Id == r.MethodSluchkiId)
                });
            }
        }

        protected async override Task AddAsync(FarmingEntities db, ReproductionViewModel model)
        {
            var reproduction = new Reproduction()
            {
                CowId = model.CowId,
                BullId = model.BullId,
                MethodSluchkiId = model.MethodSluchkiId,
                DateOsemeneniya = model.DateOsemeneniya.Value,
                ChisloSuhihDney = model.ChisloSuhihDney.Value,
                SerPeriod = model.SerPeriod.Value,
                DateOtela = model.DateOtela.Value,
            };

            db.Reproductions.Add(reproduction);

            await db.SaveChangesAsync();

            model.Id = reproduction.Id;

            Models.Add(model);
        }

        protected async override Task UpdateAsync(FarmingEntities db, ReproductionViewModel model)
        {
            var reproduction = await db.Reproductions.FindAsync(model.Id);

            if (reproduction != null)
            {
                reproduction.BullId = model.BullId;
                reproduction.CowId = model.CowId;
                reproduction.MethodSluchkiId = model.MethodSluchkiId;
                reproduction.DateOsemeneniya = model.DateOsemeneniya.Value;
                reproduction.ChisloSuhihDney = model.ChisloSuhihDney.Value;
                reproduction.SerPeriod = model.SerPeriod.Value;
                reproduction.DateOtela = model.DateOtela.Value;

                await db.SaveChangesAsync();

                var reproductionVm = Models.FirstOrDefault(r => r.Id == reproduction.Id);

                if (reproductionVm != null)
                {
                    reproductionVm.BullId = model.BullId;
                    reproductionVm.Bull = model.Bull;
                    reproductionVm.CowId = model.CowId;
                    reproductionVm.Cow = model.Cow;
                    reproductionVm.MethodSluchkiId = model.MethodSluchkiId;
                    reproductionVm.MethodSluchki = model.MethodSluchki;
                    reproductionVm.DateOsemeneniya = model.DateOsemeneniya;
                    reproductionVm.ChisloSuhihDney = model.ChisloSuhihDney;
                    reproductionVm.SerPeriod = model.SerPeriod;
                    reproductionVm.DateOtela = model.DateOtela;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, ReproductionViewModel model)
        {
            var reproduction = await db.Reproductions.FindAsync(model.Id);

            if (reproduction != null)
            {
                db.Reproductions.Remove(reproduction);

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
                var model = item as ReproductionViewModel;

                return _methodsSluchkiViewModel.SelectedModel?.Name == model?.MethodSluchki.Name;
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

            _methodsSluchkiViewModel.SelectedModel = null;

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
                    table.AddTitleTable($"{model.Id} / {model.DateOsemeneniya} - {(model.DateOtela == null ? "?" : model.DateOtela.ToString())}", fontHeader, 2);

                    table.AddCell("Идентификатор", font);
                    table.AddCell(model.Id.ToString(), font);

                    table.AddCell("Метод случки", font);
                    table.AddCell(model.MethodSluchki?.Name, font);

                    table.AddCell("Число сухих дней", font);
                    table.AddCell(model.ChisloSuhihDney.ToString(), font);

                    table.AddCell("Сер. период", font);
                    table.AddCell(model.SerPeriod.ToString(), font);

                    table.AddCell("Дата осеменения", font);
                    table.AddCell(model.DateOsemeneniya.ToString(), font);

                    table.AddCell("Дата отела", font);
                    table.AddCell((model.DateOtela == null ? "?" : model.DateOtela.ToString()), font);

                    table.AddHeaderColumn("Соответствующий бык", fontBold, 2);

                    table.AddCell("Идентификатор", font, 0, 15);
                    table.AddCell(model.Bull?.Id.ToString(), font, 0, 15);

                    table.AddCell("Месторасположение", font, 0, 15);
                    table.AddCell(model.Bull?.Location.ToString(), font, 0, 15);

                    table.AddCell("Сторонний идентификатор", font, 0, 15);
                    table.AddCell(model.Bull?.OtherId.ToString(), font, 0, 15);

                    table.AddHeaderColumn("Соответствующая корова", fontBold, 2);

                    table.AddCell("Идентификатор", font, 0, 15);
                    table.AddCell(model.Cow?.Id.ToString(), font, 0, 15);

                    table.AddCell("Имя", font, 0, 15);
                    table.AddCell(model.Cow?.Name, font, 0, 15);
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
