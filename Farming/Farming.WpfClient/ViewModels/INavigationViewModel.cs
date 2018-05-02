using Farming.WpfClient.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace Farming.WpfClient.ViewModels
{
    public interface INavigationViewModel : IInformationPageViewModel
    {
        INavigation Navigation { get; }

        IEnumerable<INavigationItem> NavigationItems { get; }

        ICommand GoToCommand { get; }

        ICommand GoBackToCommand { get; }
    }
}
