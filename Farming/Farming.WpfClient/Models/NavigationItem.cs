using MaterialDesignThemes.Wpf;
using System;

namespace Farming.WpfClient.Models
{
    public class NavigationItem : ImageItem, INavigationItem
    {
        public Type ViewType { get; }

        public NavigationItem(string title, Type viewType, PackIconKind iconKind)
            : base(title, iconKind)
        {
            ViewType = viewType;
        }
    }
}
