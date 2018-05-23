using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Farming.WpfClient.ViewModels.Database
{
    public class FamilyViewModel : ValidationViewModelBase
    {
        [Description("Идентификатор")]
        public int Id
        {
            get => Get(() => Id);
            set => Set(() => Id, value);
        }

        [Description("Название")]
        public string Name
        {
            get => Get(() => Name);
            set => Set(() => Name, value);
        }

        public virtual ICollection<CowViewModel> Cows
        {
            get => Get(() => Cows);
            set => Set(() => Cows, value);
        }

        public FamilyViewModel()
        {
            IsValidOnClick = true;

            AddRule(() => Name, () => !string.IsNullOrWhiteSpace(Name) && Name.Length < 50, "Название не должно быть пустым и содержать больше 50 символов.");

            Cows = new ObservableCollection<CowViewModel>();
        }
    }
}
