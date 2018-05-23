using System;
using System.ComponentModel;

namespace Farming.WpfClient.ViewModels.Database
{
    public class RetirementViewModel : ValidationViewModelBase
    {
        [Description("Идентификатор")]
        public int Id
        {
            get => Get(() => Id);
            set => Set(() => Id, value);
        }

        [Description("Идентификатор коровы")]
        public int CowId
        {
            get => Get(() => CowId);
            set => Set(() => CowId, value);
        }

        [Description("Дата списки")]
        public DateTime? Date
        {
            get => Get(() => Date);
            set => Set(() => Date, value);
        }

        [Description("Причина списки")]
        public string Reason
        {
            get => Get(() => Reason);
            set => Set(() => Reason, value);
        }

        public virtual CowViewModel Cow
        {
            get => Get(() => Cow);
            set => Set(() => Cow, value);
        }

        public RetirementViewModel()
        {
            IsValidOnClick = true;

            AddRule(() => Date, () => Date.HasValue, "Дата не может быть пустой.");
            AddRule(() => Reason, () => !string.IsNullOrWhiteSpace(Reason) && Reason.Length < 50, "Поле не может быть пустым.");
            AddRule(() => Cow, () => Cow != null && CowId == Cow.Id && Cow.Valid(), "Поле не должно быть пустым.");
        }
    }
}
