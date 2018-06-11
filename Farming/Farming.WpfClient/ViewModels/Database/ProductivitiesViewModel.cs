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
    public class ProductivitiesViewModel : DatabaseTableViewModel<ProductivityViewModel>
    {
        protected CowsViewModel _cowsViewModel;
        public CowsViewModel CowsViewModel => _cowsViewModel;

        public ProductivitiesViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user, CowsViewModel cowsViewModel)
            : this(snackbarMessageQueue, user, cowsViewModel, null)
        {

        }

        public ProductivitiesViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user, CowsViewModel cowsViewModel, IEnumerable<ProductivityViewModel> models)
            : base("Продуктивность", user, snackbarMessageQueue, models)
        {
            _cowsViewModel = cowsViewModel;
        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            await _cowsViewModel.UpdateAsync();

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
                    Cow = _cowsViewModel.Models.FirstOrDefault(c => c.Id == p.CowId)
                };

                Models.Add(productivity);
            }
        }

        protected async override Task AddAsync(FarmingEntities db, ProductivityViewModel model)
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

        protected async override Task UpdateAsync(FarmingEntities db, ProductivityViewModel model)
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

                var productivityVm = Models.FirstOrDefault(p => p.Id == productivity.Id);

                if (productivityVm != null)
                {
                    productivityVm.CowId = model.CowId;
                    productivityVm.Cow = model.Cow;
                    productivityVm.UchtenoLaktacij = model.UchtenoLaktacij;
                    productivityVm.UdojKg = model.UdojKg;
                    productivityVm.ZhirProz = model.ZhirProz;
                    productivityVm.BelokProz = model.BelokProz;
                    productivityVm.Date = model.Date;
                }
            }
        }

        protected async override Task DeleteAsync(FarmingEntities db, ProductivityViewModel model)
        {
            var productivity = await db.Productivities.FindAsync(model.Id);

            if (productivity != null)
            {
                db.Productivities.Remove(productivity);

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
                    table.AddTitleTable($"{model.Id} / {model.Cow?.Name} / {model.Date}", fontHeader, 2);

                    table.AddCell("Идентификатор", font);
                    table.AddCell(model.Id.ToString(), font);

                    table.AddCell("Учтено лактаций", font);
                    table.AddCell(model.UchtenoLaktacij.ToString(), font);

                    table.AddCell("Удой(кг.)", font);
                    table.AddCell(model.UdojKg.ToString(), font);

                    table.AddCell("Жир(%)", font);
                    table.AddCell(model.ZhirProz.ToString(), font);

                    table.AddCell("Белок(%)", font);
                    table.AddCell(model.BelokProz.ToString(), font);

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
