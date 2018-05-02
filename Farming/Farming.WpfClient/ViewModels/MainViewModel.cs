using MaterialDesignThemes.Wpf;
using Farming.WpfClient.Models;
using Farming.WpfClient.Views;

namespace Farming.WpfClient.ViewModels
{
    public class MainViewModel : NavigationPageViewModel
    {
        public ILink[] Links { get; }

        public MainViewModel(INavigation navigation, string title)
            : base(navigation, title)
        {
            var homeVm = new HomeViewModel();
            Navigation.Add(() => new Home(homeVm));

            _pagesViewModels = new IPageViewModel[]
            {
                homeVm
            };

            _navigationItems = new INavigationItem[]
            {
                new NavigationItem(homeVm.Title, typeof(Home), PackIconKind.Home)
            };

            Links = new ILink[]
            {
                new Link("GitHub", "https://github.com/holydk/Farming", PackIconKind.GithubCircle, "Исходный код")
            };
        }
    }
}
