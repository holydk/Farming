using System;
using System.ComponentModel;

namespace Farming.WpfClient.ViewModels.Database
{
    public class ReproductionViewModel : ValidationViewModelBase
    {
        [Description("Идентификатор")]
        public int Id
        {
            get => Get(() => Id);
            set => Set(() => Id, value);
        }

        [Description("Идентификатор коровы")]
        public int? CowId
        {
            get => Get(() => CowId);
            set => Set(() => CowId, value);
        }

        [Description("Идентификатор метода случки")]
        public int? MethodSluchkiId
        {
            get => Get(() => MethodSluchkiId);
            set => Set(() => MethodSluchkiId, value);
        }

        [Description("Идентификатор быка")]
        public int? BullId
        {
            get => Get(() => BullId);
            set => Set(() => BullId, value);
        }

        [Description("Дата осеменения")]
        public DateTime? DateOsemeneniya
        {
            get => Get(() => DateOsemeneniya);
            set => Set(() => DateOsemeneniya, value);
        }

        [Description("Число сухих дней")]
        public int? ChisloSuhihDney
        {
            get => Get(() => ChisloSuhihDney);
            set => Set(() => ChisloSuhihDney, value);
        }

        [Description("Сор. период")]
        public int? SorPeriod
        {
            get => Get(() => SorPeriod);
            set => Set(() => SorPeriod, value);
        }

        [Description("Дата отела")]
        public DateTime? DateOtela
        {
            get => Get(() => DateOtela);
            set => Set(() => DateOtela, value);
        }

        public virtual BullViewModel Bull
        {
            get => Get(() => Bull);
            set => Set(() => Bull, value);
        }

        public virtual CowViewModel Cow
        {
            get => Get(() => Cow);
            set => Set(() => Cow, value);
        }

        public virtual MethodSluchkiViewModel MethodSluchki
        {
            get => Get(() => MethodSluchki);
            set => Set(() => MethodSluchki, value);
        }

        public ReproductionViewModel()
        {
            IsValidOnClick = true;

            AddRule(() => DateOsemeneniya, () => DateOsemeneniya.HasValue, "Дата осеменения не может быть пустой.");
            AddRule(() => ChisloSuhihDney, () => ChisloSuhihDney.HasValue && ChisloSuhihDney > -1, "Поле не может быть пустым и отрицательным.");
            AddRule(() => SorPeriod, () => SorPeriod.HasValue && SorPeriod > -1, "Поле не может быть пустым и отрицательным.");
            AddRule(() => DateOtela, () => DateOtela.HasValue, "Дата отела не может быть пустой.");
            AddRule(() => Bull, () => Bull != null && BullId == Bull.Id && Bull.Valid(), "Поле не должно быть пустым.");
            AddRule(() => Cow, () => Cow != null && CowId == Cow.Id && Cow.Valid(), "Поле не должно быть пустым.");
            AddRule(() => MethodSluchki, () => MethodSluchki != null && MethodSluchkiId == MethodSluchki.Id && MethodSluchki.Valid(), "Поле не должно быть пустым.");
        }
    }
}
