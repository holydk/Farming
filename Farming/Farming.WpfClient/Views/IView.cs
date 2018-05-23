using Farming.WpfClient.ViewModels;

namespace Farming.WpfClient.Views
{
    public interface IView<T> where T : class
    {
        T ViewModel { get; set; }
    }

    public interface IView
    {
        IPageViewModel ViewModel { get; set; }
    }
}
