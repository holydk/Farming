using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Farming.WpfClient.ViewModels.Database
{
    public class BullViewModel : ValidationViewModelBase
    {
        [Description("Идентификатор")]
        public int Id
        {
            get => Get(() => Id);
            set => Set(() => Id, value);
        }

        [Description("Месторасположение")]
        public string Location
        {
            get => Get(() => Location);
            set => Set(() => Location, value);
        }

        [Description("Сторонний идентификатор")]
        public string OtherId
        {
            get => Get(() => OtherId);
            set => Set(() => OtherId, value);
        }

        public virtual ICollection<CowViewModel> Childrens
        {
            get => Get(() => Childrens);
            set => Set(() => Childrens, value);
        }

        public virtual ICollection<ReproductionViewModel> Reproductions
        {
            get => Get(() => Reproductions);
            set => Set(() => Reproductions, value);
        }

        public BullViewModel()
        {
            IsValidOnClick = true;

            //AddRule(() => Location, () => !string.IsNullOrWhiteSpace(Location) && Location.Length < 50, "Поле не должно быть пустым и содержать больше 50 символов.");
            AddRule(() => OtherId, () => !string.IsNullOrWhiteSpace(OtherId) && OtherId.Length < 50, "Поле не должно быть пустым и содержать больше 50 символов.");

            Childrens = new ObservableCollection<CowViewModel>();
            Reproductions = new ObservableCollection<ReproductionViewModel>();
        }
    }
}
