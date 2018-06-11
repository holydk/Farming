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
    public class RetirementsViewModel : DatabaseTableViewModel<RetirementViewModel>
    {
        protected CowsViewModel _cowsViewModel;
        public CowsViewModel CowsViewModel => _cowsViewModel;

        public RetirementsViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user, CowsViewModel cowsViewModel)
            : this(snackbarMessageQueue, user, cowsViewModel, null)
        {
        }

        public RetirementsViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user, CowsViewModel cowsViewModel, IEnumerable<RetirementViewModel> models)
            : base("Выбытие", user, snackbarMessageQueue, models)
        {
            _cowsViewModel = cowsViewModel;
        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            await _cowsViewModel.UpdateAsync();

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
                    Cow = _cowsViewModel.Models.FirstOrDefault(c => c.Id == r.CowId)
                };

                Models.Add(retirement);
            }
        }

        protected async override Task AddAsync(FarmingEntities db, RetirementViewModel model)
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

        protected async override Task UpdateAsync(FarmingEntities db, RetirementViewModel model)
        {
            var retirement = await db.Retirements.FindAsync(model.Id);

            if (retirement != null)
            {
                retirement.CowId = model.CowId;
                retirement.Date = model.Date.Value;
                retirement.Reason = model.Reason;

                await db.SaveChangesAsync();

                var retirementVm = Models.FirstOrDefault(r => r.Id == retirement.Id);

                if (retirementVm != null)
                {
                    retirementVm.CowId = model.CowId;
                    retirementVm.Cow = model.Cow;
                    retirementVm.Date = model.Date;
                    retirementVm.Reason = model.Reason;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, RetirementViewModel model)
        {
            var retirement = await db.Retirements.FindAsync(model.Id);

            if (retirement != null)
            {
                db.Retirements.Remove(retirement);

                await db.SaveChangesAsync();
                Models.Remove(model);
            }
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
                    table.AddTitleTable($"{model.Id} / {model.Cow?.Name} / {model.Reason} / {model.Date}", fontHeader, 2);

                    table.AddCell("Идентификатор", font);
                    table.AddCell(model.Id.ToString(), font);

                    table.AddCell("Причина", font);
                    table.AddCell(model.Reason, font);

                    table.AddCell("Дата", font);
                    table.AddCell(model.Date.ToString(), font);

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
