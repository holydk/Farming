using System;
using System.ComponentModel;

namespace Farming.WpfClient.ViewModels.Database
{
    public class ProductivityViewModel : ValidationViewModelBase
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

        [Description("Учтено лактаций")]
        public int? UchtenoLaktacij
        {
            get => Get(() => UchtenoLaktacij);
            set => Set(() => UchtenoLaktacij, value);
        }

        [Description("Удой(кг.)")]
        public double? UdojKg
        {
            get => Get(() => UdojKg);
            set => Set(() => UdojKg, value);
        }

        [Description("Жир(%)")]
        public double? ZhirProz
        {
            get => Get(() => ZhirProz);
            set => Set(() => ZhirProz, value);
        }

        [Description("Белок(%)")]
        public double? BelokProz
        {
            get => Get(() => BelokProz);
            set => Set(() => BelokProz, value);
        }

        [Description("Дата")]
        public DateTime? Date { get; set; }

        public virtual CowViewModel Cow
        {
            get => Get(() => Cow);
            set
            {
                Set(() => Cow, value);
                CowId = Cow.Id;
            }
        }

        public ProductivityViewModel()
        {
            IsValidOnClick = true;

            AddRule(() => UchtenoLaktacij, () => UchtenoLaktacij.HasValue && UchtenoLaktacij > 0, "Поле должно быть больше 0.");
            AddRule(() => UdojKg, () => UdojKg.HasValue && UdojKg > 0, "Поле должно быть больше 0.");
            AddRule(() => ZhirProz, () => ZhirProz.HasValue && ZhirProz > 0, "Поле должно быть больше 0.");
            AddRule(() => BelokProz, () => BelokProz.HasValue && BelokProz > 0, "Поле должно быть больше 0.");
            AddRule(() => Cow, () => Cow != null && CowId == Cow.Id && Cow.Valid(), "Поле не должно быть пустым.");
            AddRule(() => Date, () => Date.HasValue, "Поле должно быть пустым.");
        }
    }
}
