using Farming.WpfClient.Commands;
using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Farming.WpfClient.ViewModels
{
    public class NavigationPageViewModel : InformationPageViewModel, INavigationViewModel
    {
        private ICollectionView _navigationItemsView => CollectionViewSource.GetDefaultView(_navigationItems);

        protected IPageViewModel[] _pagesViewModels;

        public INavigation Navigation { get; }

        protected IEnumerable<INavigationItem> _navigationItems;
        public IEnumerable<INavigationItem> NavigationItems => _navigationItems;

        public ICommand GoToCommand { get; }

        public ICommand GoBackToCommand { get; }

        public NavigationPageViewModel(INavigation navigation, string title, ISnackbarMessageQueue snackbarMessageQueue)
            : base(title, snackbarMessageQueue)
        {
            Navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));

            GoToCommand = new RelayCommand(async (sender) =>
            {
                if (sender is Type viewType)
                {
                    await Navigation.GoToAsync(viewType);
                }
            });

            GoBackToCommand = new RelayCommand(async (sender) =>
            {
                if (sender is Type viewType)
                {
                    await Navigation.GoBackAsync(viewType);
                }
            });
        }

        public override void Search(object sender)
        {
            if (sender is string text)
            {
                text = text.ToLower();

                if (string.IsNullOrWhiteSpace(text))
                {
                    _navigationItemsView.Filter = null;
                }
                else
                {
                    _navigationItemsView.Filter = (item) =>
                    {
                        return (item as INavigationItem).Title.ToLower().Contains(text);
                    };
                }
            }           
        }
    }
}
