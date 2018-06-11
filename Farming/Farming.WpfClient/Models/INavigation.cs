using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Farming.WpfClient.Models
{
    public interface INavigation : INotifyPropertyChanged
    {
        bool CanGoBack { get; }
        bool CanGoForward { get; }
        UserControl Content { get; }

        event EventHandler<NavigationEventArgs> Navigating;
        event EventHandler<NavigationEventArgs> Navigated;

        void Add<T>(Func<T> func) where T : UserControl;

        void GoTo<T>(Func<T> func) where T : UserControl;
        Task GoToAsync<T>(Func<T> func) where T : UserControl;
        Task GoToAsync(Type type);
        Task GoBackAsync(Type type);
        Task GoBackAsync();
        void GoBack();
    }
}
