using Farming.WpfClient.Models;
using System.Windows.Input;

namespace Farming.WpfClient.ViewModels
{
    public interface IPageViewModel : IUpdatable, ICanSearch
    {
        string Title { get; }

        bool IsBusy { get; set; }

        string SearchText { get; set; }

        ICommand SearchCommand { get; }

        ICommand ClearSearchCommand { get; }
    }
}
