using Farming.WpfClient.Models;
using System.Threading.Tasks;

namespace Farming.WpfClient.ViewModels
{
    public class PageViewModel : ValidationViewModelBase, IPageViewModel, ICanSearch
    {
        public string Title { get; }

        public bool IsBusy
        {
            get => Get(() => IsBusy);
            set => Set(() => IsBusy, value);
        }

        public bool IsCommonSearchActivated
        {
            get => Get(() => IsCommonSearchActivated);
            set => Set(() => IsCommonSearchActivated, value);
        }

        public PageViewModel(string title)
        {
            Title = title;
        }

        public virtual void Search(object sender)
        {
            
        }

        public virtual Task UpdateAsync()
        {
            return Task.CompletedTask;
        }
    }
}
