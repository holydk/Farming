using System.ComponentModel;

namespace Farming.WpfClient.ViewModels.Database
{
    public class PriplodViewModel : ValidationViewModelBase
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

        [Description("Идентификатор пола")]
        public int? GenderId
        {
            get => Get(() => GenderId);
            set => Set(() => GenderId, value);
        }

        [Description("Вес(кг.)")]
        public double? Weight
        {
            get => Get(() => Weight);
            set => Set(() => Weight, value);
        }

        public virtual GenderViewModel Gender
        {
            get => Get(() => Gender);
            set
            {
                Set(() => Gender, value);

                if (Gender != null)
                    GenderId = Gender.Id;
            }
        }

        public virtual CowViewModel Mother
        {
            get => Get(() => Mother);
            set
            {
                Set(() => Mother, value);

                if (Mother != null)
                    CowId = Mother.Id;
            }
        }

        public PriplodViewModel()
        {
            IsValidOnClick = true;

            AddRule(() => Weight, () => Weight > 0, "Вес должен быть быльше нуля.");
            AddRule(() => Gender, () => Gender != null && Gender.Id == GenderId && Gender.Valid(), "Поле не должно быть пустым.");
            //AddRule(() => Mother, () => Mother != null && Mother.Id == CowId && Mother.Valid(), "Поле не должно быть пустым.");
        }
    }
}
