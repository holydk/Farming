using Farming.WpfClient.Models;

namespace Farming.WpfClient.ViewModels
{
    public interface IPageViewModel : IUpdatable
    {
        string Title { get; }

        bool IsBusy { get; set; }

        void Search(object sender);
    }
}
