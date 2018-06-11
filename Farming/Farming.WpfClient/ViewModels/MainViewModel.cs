using MaterialDesignThemes.Wpf;
using Farming.WpfClient.Models;
using Farming.WpfClient.Views;
using Farming.WpfClient.ViewModels.Database;
using Farming.WpfClient.Views.Database;
using System;
using System.Linq;

namespace Farming.WpfClient.ViewModels
{
    public class MainViewModel : NavigationPageViewModel
    {
        public ILink[] Links { get; }

        private User _user;
        public User User => _user;

        public MainViewModel(User user, INavigation navigation, string title, ISnackbarMessageQueue snackbarMessageQueue)
            : base(navigation, title, snackbarMessageQueue)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));

            var homeVm = new HomeViewModel(snackbarMessageQueue);
            Navigation.Add(() => new Home(homeVm));

            var bloodyVm = new BloodTypesViewModel(snackbarMessageQueue, _user);
            Navigation.Add(() => new BloodTypesView(bloodyVm));

            var breedsVm = new BreedsViewModel(snackbarMessageQueue, _user);
            Navigation.Add(() => new BreedsView(breedsVm));

            var categoriesVm = new CategoriesViewModel(snackbarMessageQueue, _user);
            Navigation.Add(() => new CategoriesView(categoriesVm));

            var linesVm = new LinesViewModel(snackbarMessageQueue, _user);
            Navigation.Add(() => new LinesView(linesVm));

            var familiesVm = new FamiliesViewModel(snackbarMessageQueue, _user);
            Navigation.Add(() => new FamiliesView(familiesVm));

            var gendersVm = new GendersViewModel(snackbarMessageQueue, _user);
            Navigation.Add(() => new GendersView(gendersVm));

            var msVm = new MethodsSluchkiViewModel(snackbarMessageQueue, _user);
            Navigation.Add(() => new MethodsSluchkiView(msVm));

            var bullsVm = new BullsViewModel(snackbarMessageQueue, _user);
            Navigation.Add(() => new BullsView(bullsVm));

            var cowsVm = new CowsViewModel(snackbarMessageQueue, _user, bullsVm, bloodyVm, categoriesVm, linesVm, breedsVm, familiesVm);
            Navigation.Add(() => new CowsView(cowsVm));

            var repsVm = new ReproductionsViewModel(snackbarMessageQueue, _user, cowsVm, msVm);
            Navigation.Add(() => new ReproductionsView(repsVm));

            var usersTypesVm = new UsersTypesViewModel(snackbarMessageQueue, _user);

            var usersVm = new UsersViewModel(snackbarMessageQueue, _user, usersTypesVm);
            Navigation.Add(() => new UsersView(usersVm));

            var prodVm = new ProductivitiesViewModel(snackbarMessageQueue, _user, cowsVm);
            Navigation.Add(() => new ProductivitiesView(prodVm));

            var retVm = new RetirementsViewModel(snackbarMessageQueue, _user, cowsVm);
            Navigation.Add(() => new RetirementsView(retVm));

            var priplodsVm = new PriplodsViewModel(snackbarMessageQueue, _user, cowsVm, gendersVm);
            Navigation.Add(() => new PriplodsView(priplodsVm));

            _pagesViewModels = new IPageViewModel[]
            {
                homeVm, bloodyVm, breedsVm, categoriesVm, linesVm, familiesVm, gendersVm, msVm, bullsVm, prodVm, cowsVm, usersTypesVm, usersVm, retVm, priplodsVm
            };

            Navigation.Navigating += (obj, e) =>
            {
                if (e.Content != null && e.Content.DataContext is IDatabaseTableViewModel dbVm)
                {
                    dbVm.ClearData();
                }
            };

            _navigationItems = new INavigationItem[]
            {                
                new NavigationItem(bloodyVm.Title, typeof(BloodTypesView), PackIconKind.Fire),
                new NavigationItem(breedsVm.Title, typeof(BreedsView), PackIconKind.Eraser),
                new NavigationItem(categoriesVm.Title, typeof(CategoriesView), PackIconKind.CardsOutline),
                new NavigationItem(linesVm.Title, typeof(LinesView), PackIconKind.SourceBranch),
                new NavigationItem(familiesVm.Title, typeof(FamiliesView), PackIconKind.CardsVariant),
                new NavigationItem(gendersVm.Title, typeof(GendersView), PackIconKind.GenderMaleFemale),
                new NavigationItem(msVm.Title, typeof(MethodsSluchkiView), PackIconKind.SourceFork),
                new NavigationItem(bullsVm.Title, typeof(BullsView), PackIconKind.Cow),
                new NavigationItem(prodVm.Title, typeof(ProductivitiesView), PackIconKind.ChartBar),
                new NavigationItem(usersVm.Title, typeof(UsersView), PackIconKind.AccountMultiple),
                new NavigationItem(cowsVm.Title, typeof(CowsView), PackIconKind.Cow),
                new NavigationItem(repsVm.Title, typeof(ReproductionsView), PackIconKind.HeartHalfFull),
                new NavigationItem(retVm.Title, typeof(RetirementsView), PackIconKind.Logout),
                new NavigationItem(priplodsVm.Title, typeof(PriplodsView), PackIconKind.Cow)
            }.OrderBy(i => i.Title).Prepend(new NavigationItem(homeVm.Title, typeof(Home), PackIconKind.Home));

            Links = new ILink[]
            {
                new Link("GitHub", "https://github.com/holydk/Farming", PackIconKind.GithubCircle, "Исходный код")
            };
        }
    }
}
