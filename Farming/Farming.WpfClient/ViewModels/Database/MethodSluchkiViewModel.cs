using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Farming.WpfClient.ViewModels.Database
{
    public class MethodSluchkiViewModel : ValidationViewModelBase
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

        public virtual ICollection<ReproductionViewModel> Reproductions
        {
            get => Get(() => Reproductions);
            set => Set(() => Reproductions, value);
        }

        public MethodSluchkiViewModel()
        {
            IsValidOnClick = true;

            AddRule(() => Name, () => !string.IsNullOrWhiteSpace(Name) && Name.Length < 50, "Название не должно быть пустым и содержать больше 50 символов.");

            Reproductions = new ObservableCollection<ReproductionViewModel>();
        }
    }
}
