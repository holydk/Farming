using MaterialDesignThemes.Wpf;
using Farming.WpfClient.Models;
using Farming.WpfClient.Views;
using Farming.WpfClient.ViewModels.Database;
using Farming.WpfClient.Views.Database;

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

            var bloodyVm = new BloodTypesViewModel();
            Navigation.Add(() => new BloodTypesView(bloodyVm));

            var breedsVm = new BreedsViewModel();
            Navigation.Add(() => new BreedsView(breedsVm));

            var categoriesVm = new CategoriesViewModel();
            Navigation.Add(() => new CategoriesView(categoriesVm));

            var linesVm = new LinesViewModel();
            Navigation.Add(() => new LinesView(linesVm));

            var familiesVm = new FamiliesViewModel();
            Navigation.Add(() => new FamiliesView(familiesVm));

            var gendersVm = new GendersViewModel();
            Navigation.Add(() => new GendersView(gendersVm));

            var msVm = new MethodsSluchkiViewModel();
            Navigation.Add(() => new MethodsSluchkiView(msVm));

            var bullsVm = new BullsViewModel();
            Navigation.Add(() => new BullsView(bullsVm));

            _pagesViewModels = new IPageViewModel[]
            {
                homeVm, bloodyVm, breedsVm, categoriesVm, linesVm, familiesVm, gendersVm, msVm, bullsVm
            };

            _navigationItems = new INavigationItem[]
            {
                new NavigationItem(homeVm.Title, typeof(Home), PackIconKind.Home),
                new NavigationItem(bloodyVm.Title, typeof(BloodTypesView), PackIconKind.Fire),
                new NavigationItem(breedsVm.Title, typeof(BreedsView), PackIconKind.Eraser),
                new NavigationItem(categoriesVm.Title, typeof(CategoriesView), PackIconKind.CardsOutline),
                new NavigationItem(linesVm.Title, typeof(LinesView), PackIconKind.SourceBranch),
                new NavigationItem(familiesVm.Title, typeof(FamiliesView), PackIconKind.CardsVariant),
                new NavigationItem(gendersVm.Title, typeof(GendersView), PackIconKind.GenderMaleFemale),
                new NavigationItem(msVm.Title, typeof(MethodsSluchkiView), PackIconKind.SourceFork),
                new NavigationItem(bullsVm.Title, typeof(BullsView), PackIconKind.Cow)
            };

            Links = new ILink[]
            {
                new Link("GitHub", "https://github.com/holydk/Farming", PackIconKind.GithubCircle, "Исходный код")
            };
        }
    }
}
